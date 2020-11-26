#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Generator : MonoBehaviour
{
    #region Var
    [Header("Platform info")]
    public float Xmin = 1f;
    public float Xmax = 5f;
    public float Ymin = -3f;
    public float Ymax = 3f;

    /*
        QUé nesecitamos saber?

        - 
        - PosY de la ultima aerea generada
        - DistanciaX de la ultima aerea generada
        - Contado de Plataformas aereas creadas
        - Limite de plataformas aereas en juego

        - DistanciaX de la ultima Terrestre generada
        - Contado de plataformas Terrestre creadas
        - Limite de plataformas


     */

    /*
     * La idea es que el primer aereo se genera en la linea verde de camara(Gizmos)
     * y que luego con esa posición podemos crear la siguiente y así hasta 
     * poseer X en pantalla
     * para que tenga un toque random estas pueden estar a la mitad de la anterior plataforma como
     * minimo, y como maximo es el espacio del personaje más el rango que tiene en los saltos (basado en su speed)?
     * sino hardcodear eso y variarlo hasta que cuele
     */



    /*
     *   nextPosition = platformContainer.position;
        for (int x = 0; x < initialPlatforms; x++)
        {
            Spawn();
        }

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
     *  
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


    [Header("Platform settings")]
    public GameObject obj_platform;

    // -> con esto sabremos donde deberíamos generar la siguiente plataforma  para que sea coherente
    public float lastPlatformPosY;

    [Header("BaseFloor Settings")]
    ///Este servirá para tener el piso principal, este es unico y tiende a ser largo ?
    public GameObject prefab_FieldPlatform;
    public GameObject[] prefab_EasyPlatform;
    public GameObject[] prefab_HardPlatform;


    /// <summary>
    /// Con esto sabremos el rango que tiene que pasar para ser destruido sin mostrarse.
    /// </summary>
    public int destroyMargin;




    //HACK

    //Donde guardaremos las posiciones aereas, cambiar name luego TODO
    Vector2 nextPosition;
    Vector2 lastSizePlatform;
    //Cantidad de plataformas que puedes colocar
    public int limitPlatforms = 10;

    #endregion
    #region Events
    private void Start()
    {
        //platformContainer.position;
        //Asignamos el primer spawn
        nextPosition = GameManager.GetCameraWidth() * Vector2.right;

        for (int x = 0; x < limitPlatforms; x++)
        {
            Spawn();
        }
    }
    #endregion
    #region Methods



    /// <summary>TODO
    /// Crea una plataforma
    /// </summary>
    private void Spawn()
    {
        //Temporal
        int @rangeAdrede = 17;

        float RandomX_min = lastSizePlatform.x / 2;
        float RandomX_max = nextPosition.x + @rangeAdrede;
        float RandomX = Random.Range(RandomX_min, RandomX_max);

        int @verticalMinAdrede = 1;
        float RandomY_min = @verticalMinAdrede;
        float RandomY_max = GameManager.GetCameraHeight();

        float RandomY = Random.Range(RandomY_min, RandomY_max);

        Vector2 _newPos = new Vector2(RandomX, RandomY);

        //Instanciamos el prefab de la plataforma en una pos random basado en los rangos como hijo del contenedor
        //GameObject _obj = Instantiate(
        //    prefabPlatforms[randomPlatform],
        //    _randomPos,
        //    Quaternion.identity,
        //    platformContainer
        //);


        Debug.Log(nextPosition +" | " + _newPos);


        nextPosition = _newPos;

        //PASOS

        //1.Conocer la posición donde será creado:
        //- Calcular -> randomX entre (nextPosition - tamañoDeAnteriorPlatforma/2), XCantidad

        //2.Instanciarlo y colocarlo en PlatformGroup como hijo con la pos encontrada

        //3. Guardar su pos y el tamaño que posee



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