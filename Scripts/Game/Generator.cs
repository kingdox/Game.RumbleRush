#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Generator : MonoBehaviour
{
    #region Var

    private Camera camera;

    [Header("Platform settings")]
    public GameObject obj_platform;
    
    public GameObject lastPlatform;

    [Header("BaseFloor Settings")]
    ///Este servirá para tener el piso principal, este es unico y tiende a ser largo ?
    public GameObject prefab_FieldPlatform;
    public GameObject[] prefab_EasyPlatform;
    public GameObject[] prefab_HardPlatform;


    /// <summary>
    /// Con esto sabremos el rango que tiene que pasar para ser destruido sin mostrarse.
    /// </summary>
    public int destroyMargin;

    #endregion
    #region Events
    void Start()
    {
        camera = Camera.main;
    }
    void Update()
    {
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(
    //        transform.position,
    //        transform.position + Vector3.up * Data.data.platformMaxY
    //    );
    //}
    #endregion
    #region Methods




    private void Spawn()
    {

        
    }

    private void DestroyPlatform()
    {
        // solicito que genere nueva plataforma
        //Manager.instance.Spawn();

        //Destroy(gameObject);
    }


    #endregion

}

/*
 * 
 * 
 * /// <summary>
    /// Genera una nueva plataforma en la posicion aleatoria dentro de los margenes definidos
    /// </summary>
    public void Spawn()
    {

        
        Vector2 _randomPos = nextPosition + new Vector2(
            Random.Range(Xmin, Xmax), 
            Random.Range(Ymin, Ymax)
        );
        if (_randomPos.y < GameManager._.lowerLimit) _randomPos.y = GameManager._.lowerLimit;
        
        //_randomPos.y = Mathf.Clamp(_randomPos.y, GameManager._.lowerLimit, Ymax )

        int randomPlatform = Random.Range(0, prefabPlatforms.Length);

        //Instanciamos el prefab de la plataforma en una pos random basado en los rangos como hijo del contenedor
        GameObject _obj = Instantiate(
            prefabPlatforms[randomPlatform],
            _randomPos,
            Quaternion.identity,
            platformContainer
        );


        // tamaño de la ulima plataforma generada
        float _temPlatformSize = _obj.GetComponent<PlatformController>().platformSize; 

        //Separo al pos random tanto como tamaño tenga la nueva generada
        _randomPos.x += _temPlatformSize;

        // actualizo la pos de la siguiente plataforma
        nextPosition = _randomPos;



    }
 * 
 */

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    #region Variables

    //Tamaño de la plataforma
    public float platformSize = 10f;

    // margen respecto a la camara, donde la plataforma sera destruida
    public float destroyMargin = 30f;
    private Camera cam;

    #endregion
    #region Event

    void Start()
    {
        cam = Camera.main;    
    }

    void Update()
    {
        CheckDestroyMargin();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Destruye la plataforma actual y solicita crear una nueva
    /// </summary>
    private void DestroyPlatform(){
        // solicito que genere nueva plataforma
        Manager.instance.Spawn();

        Destroy(gameObject);
    }

    /// <summary>
    /// Verifica si se ha sobrepasado el limite de destruccion
    /// </summary>
    private void CheckDestroyMargin(){
        if (transform.position.x < cam.transform.position.x - destroyMargin){
            DestroyPlatform();
        }


        Vector2 lineA = new Vector3(
            cam.transform.position.x - destroyMargin,
            cam.transform.position.y + 10f
        );
        Vector2 lineB = new Vector3(
            cam.transform.position.x - destroyMargin,
            cam.transform.position.y - 10f
        );

        //Gizmos.color = Color.green;
        Debug.DrawLine(lineA, lineB, Color.green);
    }

    #endregion
    #region Debug
    
    #endregion
}
 
 
 */
