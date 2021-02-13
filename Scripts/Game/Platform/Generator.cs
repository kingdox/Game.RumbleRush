#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Generator : MonoBehaviour
{
    private static Generator _;
    #region Var

    /*
     * Generator se encarga de:
     * 
     *  1- Generar las plataformas del suelo y a veces dejar espacio entre estas para crear fosas,
     *  estos espacios deben ser comprensibles para no bloquear al jugador...
     *  
     *  2- Generar las plataformas aereas y, dependiendo del rango interno 
     *  conocer el clamping para no entrar
     *
     *
     * Si el personaje esta debajo de la mitad de la camara entonces 
     * las plataformas procurarán generarse para abajo para que pueda empezar a conseguir alcanzarlo
     *
     *
     */

    [Header("Platform Settings")]
    private Vector2 lastPlatform_position;
    private Vector2 lastPlatform_size;
    public GameObject prefab_platform;
    public GameObject obj_platformGroup;


    #endregion
    #region Events
    private void Awake()
    {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
    }
    private void Start()
    {
        //platformContainer.position;
        //Asignamos el primer spawn
        lastPlatform_position = (GameManager.GetCameraWidth() + Data.data.platformRangeX) * Vector2.right;
        for (int x = 0; x < Data.data.limitPlatformsInGame; x++)
        {
            SpawnPlatform();
        }

    }

    #endregion
    #region Methods

    /// <summary>
    /// revisa si generar una plataforma
    /// </summary>
    /// <param name="isFloor"></param>
    public static void CheckSpawnTo()
    {
        _.SpawnPlatform();
    }

    /// <summary>
    /// Crea una plataforma con unos margenes de tamaño y en una posición aleatoria
    /// dentro de otros parametros con respecto a la camara y la ultima plataforma
    /// </summary>
    private void SpawnPlatform()
    {

        Vector2 _newScale = GetNewScale();

        float _newPosX = GetNewPos_X(_newScale.x);

        float _newPosY = GetNewPos_Y(_newScale.y);


        Vector3 _newPos = new Vector3(_newPosX, _newPosY,5);

        //Instanciamos el prefab de la plataforma en una pos random basado en los rangos como hijo del contenedor
        GameObject _obj = Instantiate(
            prefab_platform,
            _newPos,
            Quaternion.identity,
            obj_platformGroup.transform
        );

        _obj.transform.localScale = _newScale;

        //Les asignamos los valores a los last...
        lastPlatform_size = _obj.transform.localScale;
        lastPlatform_position = _newPos;


        //Generamos algo
        ItemGenerator.Generate(lastPlatform_size, lastPlatform_position);
    }


    /// <summary>
    /// Asignamos la escala nueva y recolocamos el objeto
    /// </summary>
    private Vector2 GetNewScale()
    {

        Vector2 pref_scale = prefab_platform.transform.localScale;

        float size_X = Random.Range(pref_scale.x , pref_scale.x * 3);

        Vector2 _newScale = new Vector3(size_X, pref_scale.y);

        //_obj.transform.localScale = new Vector3(size_X, _obj.transform.localScale.y, 1);
        //_newPos += Vector2.right * size_X / 2;
        //_obj.transform.position = new Vector3(_newPos.x, _newPos.y, 1);


        return _newScale;
    }

    /// <summary>
    /// Obtenemos la posicion de la nueva plataforma en X
    //en modo dificil están mas espaciadas las plataformas
    /// </summary>
    /// <returns>el valor de la posición en el mundo</returns>
    private float GetNewPos_X(float scale_x = 0)
    {
        //minimo la pos de la ultima plataforma, y el max es la ultima pos de la plataforma
        float min_X = (lastPlatform_position.x + lastPlatform_size.x) + scale_x / 2;
        float max_X = min_X
            + Data.data.platformRangeX
            + (GameSetup.hardMode ? Data.data.hardMode_platformRangeXPlus : 0)
        ;

        // si la plataforma que se generó estaba cerca del
        // top entonces se ponen mas pegadas
        if (lastPlatform_position.y > GameManager.GetCamera().transform.position.y  + GameManager.GetCameraHeight() /  2){
            float val = max_X / 2;
            Debug.Log($"Past {max_X}, probably new : {val}");
            max_X = Mathf.Clamp(val, min_X, max_X);
        }

        return Random.Range(min_X, max_X);
    }


    /// <summary>
    /// Obtenemos la posición Y de la nueva plataformax
    /// </summary>
    /// <returns>La Pos Y de la nueva plataforma :)</returns>
    private float GetNewPos_Y( float scale_y = 0)
    {
        //Alto de la camara 
        float cam_h = GameManager.GetCameraHeight();
        Vector3 cam_pos = GameManager.GetCamera().transform.position;

        //Rango limite para generar, que tan lejos puede generarse....
        float range = Data.data.platformRangeY;

        // aproximación entre ambos rangos
        float _newPosY = Random.Range(lastPlatform_position.y - range, lastPlatform_position.y + range - (GameSetup.hardMode ? Data.data.hardMode_platformRangeYPlus : 0) );

        //Revisamos si player esta debajo de la mitad de la camara, de ser así lo obtenido lo bajamos
        //si el plater esta cerca del suelo
        if (PlayerManager.player.obj_player.transform.position.y < cam_pos.y - (cam_h / 3)){

            Debug.Log("Tendencia a subir");
            // 5% de reducirlo si el player está debajo de la mitad de las camara
            if (Random.Range(0f, 1f) < 0.25f)
            {
                _newPosY /= 2;
            }
        }

        //Tamaño del player
        float player_height = PlayerManager.player.playerController.boxCollider2D.size.y;
        
        //Calculamos el tope de la vision
        float top = cam_pos.y + (cam_h / 2);
        //confirmación de que no sobrepasa los limites,
        //se le resta también el tamaño del player para evitar plataformas imposibles
        

        _newPosY = Mathf.Clamp(_newPosY, 1.0f, (top - player_height - scale_y) - 1 );

        return _newPosY;
    }
    #endregion



    private void OnDrawGizmos()
    {
        float cam_h = DataFunc.GetScreenHeightUnit();
        Vector3 cam_pos = Camera.main.transform.position;
        float player_height = 2;// PlayerManager.player.playerController.boxCollider2D.size.y;

        float top = cam_pos.y + (cam_h / 2);

        Gizmos.DrawLine(new Vector3(0, top - player_height - prefab_platform.transform.localScale.y, -5), new Vector3(-10, top - player_height - cam_h / 6, -5));

    }
}