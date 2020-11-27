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
     * TODO:
     * Generator se encarga de:
     * 
     *  1- Generar las plataformas del suelo y a veces dejar espacio entre estas para crear fosas,
     *  estos espacios deben ser comprensibles para no bloquear al jugador...
     *  
     *  2- Generar las plataformas aereas y, dependiendo del rango interno 
     *  conocer el clamping para no entrar
     *
     *  3- Dependiendo de la distancia que este recorra, 
     *  cambiar las plataformas "faciles por las dificiles"
     *  
     *  //Notas
     *  Los monstruos, monedas y botiquines se generan con las plataformas, 
     *  de manera que no hay que preocuparse ya que estas estarán ahí
     *
     *
     *  //Extra
     *   Si hay tiempo, deberías de hacer que las plataformas sean dinamicas en su escala, 
     *   de manera que permita varíar dentro de ciertos parametros, así hay mayor diferencia
     */

    [Header("Platform Settings")]
    private Vector2 lastPlatform_position;
    private Vector2 lastPlatform_size;
    public GameObject prefab_platform;
    public GameObject obj_platformGroup;


    [Header("Floor Settings")]
    private Vector2 lastFloor_position;
    private Vector2 lastFloor_size;
    public GameObject prefab_floor;
    public GameObject obj_floorGroup;

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
        lastPlatform_position = GameManager.GetCameraWidth() * Vector2.right;
        lastFloor_position = GameManager.GetCameraWidth() * Vector2.right;

        lastFloor_size = prefab_floor.transform.localScale;

        for (int x = 0; x < Data.data.limitPlatformsInGame; x++)
        {
            SpawnPlatform();
        }
        for (int j = 0; j < Data.data.limitBaseFloorInGame; j++)
        {
            SpawnFloor();
        }

    }

    #endregion
    #region Methods

    /// <summary>
    /// Dependiendo de lo que se le solicita a generar
    /// hará un suelo o una plataforma
    /// </summary>
    /// <param name="isFloor"></param>
    public static void CheckSpawnTo(bool isFloor)
    {
        if (isFloor) _.SpawnFloor();
        else _.SpawnPlatform();
        Debug.Log($"Generando, es suelo? {isFloor}");
    }

    /// <summary>
    /// Generas más suelo, este debe estar pegado del ultimo
    /// </summary>
    private void SpawnFloor()
    {
        float X = lastFloor_size.x + lastFloor_position.x + Random.Range(0,Data.data.floorRangeX);
        Vector2 _newPos = new Vector2(X, 0);

        GameObject _obj = Instantiate(
            prefab_floor,
            _newPos,
            Quaternion.identity,
            obj_floorGroup.transform
        );

        lastFloor_position = _obj.transform.position;
        lastFloor_size = _obj.transform.localScale;
    }

    


    /// <summary>TODO
    /// Crea una plataforma
    /// </summary>
    private void SpawnPlatform()
    {
        //Temporal

        float RandomX_min = lastPlatform_size.x / 2;
        float RandomX_max = Mathf.Clamp(Data.data.platformRangeX, RandomX_min, RandomX_min + Data.data.platformRangeX);

        //Sacamos el rango de distancia entre la ultima plataforma y la que se va a generar
        float RandomX = Random.Range(RandomX_min, RandomX_max) + lastPlatform_position.x; 

         
        float RandomY_min = 1;
        float RandomY_max = GameManager.GetCameraHeight();

        float RandomY = Random.Range(RandomY_min, RandomY_max);

        //Con esto sabemos con aproximación hacia qué lado está orientado
        float minYRange = Mathf.Clamp(lastPlatform_position.y - Data.data.platformRangeY, RandomY_min, RandomY_max);
        float maxYRange = Mathf.Clamp(lastPlatform_position.y + Data.data.platformRangeY, RandomY_min, RandomY_max);

        RandomY = Mathf.Clamp(RandomY, minYRange, maxYRange);

        //Distancia aprox entre las plataformas
        float range = lastPlatform_position.y + 2;


        Vector2 _newPos = new Vector2(RandomX, RandomY);

        //Instanciamos el prefab de la plataforma en una pos random basado en los rangos como hijo del contenedor
        GameObject _obj = Instantiate(
            prefab_platform,
            _newPos,
            Quaternion.identity,
            obj_platformGroup.transform
        );

        // test
        float size_X = _obj.transform.localScale.x;
            size_X  = Random.Range(size_X, size_X + size_X * 2);
        _obj.transform.localScale = new Vector3(size_X, _obj.transform.localScale.y,1);

        _newPos += Vector2.right * size_X / 2;

        _obj.transform.position = new Vector3(_newPos.x, _newPos.y,1);

        //TODO hay que sacarle el size de esto
        lastPlatform_size = _obj.transform.localScale;


        lastPlatform_position = _newPos;
    }









    #endregion
    #region DEBBUG

    /// <summary>
    /// Calculos shidos xdxdxd
    /// </summary>
    private void OnDrawGizmos()
    {
        //calculo para saber aprox el tamaño de la camara en unidades unity eso
        float camHeightAprox = DataFunc.GetScreenHeightUnit();
        float camWidthAprox = DataFunc.GetScreenWidthUnit(camHeightAprox);

        Vector3 range = Vector3.up * (camHeightAprox / 2);

        Vector3 safeElimination = Camera.main.transform.position + (Vector3.left * camWidthAprox);
        Vector3 safeGeneration = Camera.main.transform.position + (Vector3.right * camWidthAprox);

        ///Area segura para eliminar una plataforma por parte de la camara
        Gizmos.color = Color.red;
        Gizmos.DrawLine(safeElimination + range, safeElimination - range);

        //Area segura para la generación de la plataforma 
        Gizmos.color = Color.green;
        Gizmos.DrawLine(safeGeneration + range, safeGeneration - range);
    }
    #endregion
}