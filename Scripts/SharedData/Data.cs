using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



/// <summary>
/// Representa los datos basicos del enviroment/app/exe
/// </summary>
public class Data 
{
    [HideInInspector]
    public static Data data = new Data();

    public readonly string savedPath = "saved9.txt";
    public readonly string version = "v0.9.8";

    //Datos especificos

    /// <summary>Valor de 1 unidad de metro</summary>
    public readonly float metter = 10;


    [Header("Items inGame")]
    //valores improtantes de los objetos en juego
    public readonly int[] coinRangeValue = { 1, 15 };
    public readonly int[] healRangeValue = { 15, 30 };


    [Header("Monster")]
    public readonly int monsterEasyMettersValue = 5;
    // -> Valor de cada monstruo eliminado
    public readonly int monsterValue_floor = 15;
    public readonly int monsterValue_aero = 30;

    public readonly int monsterSpeed = 3;
    public readonly int[] spawnTimeRange_floor = { 3, 6 };
    public readonly int[] spawnTimeRange_aero = { 5, 10 };

    public readonly int[] monsterDamageRange_floor = { 4, 8 };
    public readonly int[] monsterDamageRange_aero = { 3, 6 };

    // -> Cantidad maxima de un tipod e buff
    public readonly int maxBuffCount = 10;

    [Header("Player")]
    // -> cantidad para reducir la vida por el tiempo
    public readonly float lifeReductor = 10f;

    // -> tiempo para volver a ser atacado
    public readonly float playerInmuneTimeCountLimit = 0.25f;

    [Header("Platform info")]
    //Todo tendré problemas con el collector de basura? hay muchas cosas eliminandose'?'???
    public readonly int limitPlatformsInGame = 4;

    //-> Usado para saber el rango separador entre las plataformas para no generarse pegado.
    public readonly float platformRangeY = 3.0f;


    //-> Usado para conocer el rango de espacio entre ambas plataformas
    public readonly float platformRangeX = 10.0f;

    [Header("Modo dificil")]
    // --> la suma extra aplicada a la pos de plataformas
    public readonly float hardMode_platformRangeXPlus = 5f;
    public readonly float hardMode_platformRangeYPlus = 3f;

    public readonly int[] hardMode_spawnTimeRange = { 2, 6 };

    [Header("Floor info")]
    public readonly float floorRangeX = 5.0f;

    public enum Scenes
    {
        CutScene,
        MenuScene,
        InstructionScene,
        PreparationScene,
        GameScene
    }
}



/// <summary>
/// Aquí se poseerán las funcionalidades
/// que pueden usarse en otros sitios
/// los que puede que se usen mas de una vez
/// Puede que sean utiles para futuros proyectos
/// </summary>
public struct DataFunc
{

    /// <summary>
    /// sacas el alto aprox, de una camara o la activa por defecto
    /// </summary>
    /// <param name="camera"></param>
    /// <returns>el alto de la camara en unidades de Unity</returns>
    public static float GetScreenHeightUnit(Camera camera = null) => camera ? camera.orthographicSize * 2f : Camera.main.orthographicSize * 2f;

    /// <summary>
    /// Sacas el ancho de la pantalla basado en el alto de la camara 
    /// </summary>
    /// <param name="camHeight"></param>
    /// <returns>regresa el Ancho de la camara en unidades Unity</returns>
    public static float GetScreenWidthUnit(float camHeight) => camHeight * (Screen.width / Screen.height);

    /// <summary>
    /// Activa o desactiva el objeto basado en una condición recibida
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="condition"></param>
    public static void ObjOnOff(GameObject obj, bool condition) => obj.SetActive(condition);

    /// <summary>
    /// Te permite ir hacia adelante o hacia atrás en un arreglo sin salirte de los limites
    /// donde indexLength es el rango posible del index (arr.length - 1);
    /// _index es la pos actual en el arreglo, en caso de no haber es 0
    /// </summary>
    /// <returns>la nueva posición en el arreglo</returns>
    public static int TravelArr(bool goNext, int indexLength, int _index = 0 )
    {
        int i = goNext
            ? (_index == indexLength - 1 ? 0 : _index + 1)
            : (_index == 0 ? indexLength - 1 : _index - 1)
        ;
        return i;
    }

    /// <summary>
    /// Cambiamos a la escena indicada en numerico
    /// </summary>
    /// <param name="index"></param>
    public static void ChangeSceneTo(int index) => SceneManager.LoadScene(index);
    /// <summary>
    /// Cambiamos a la escena indicada con el nombre
    /// </summary>
    /// <param name="name"></param>
    public static void ChangeSceneTo(string name) => SceneManager.LoadScene(name);

    /// <summary>
    /// Devuelve el nombre de la escena activa
    /// </summary>
    public static Data.Scenes ActiveScene() => (Data.Scenes)SceneManager.GetActiveScene().buildIndex;

    /// <summary>
    ///  Saca el porcentaje de la cantidad y el maximo en caso de tener
    /// </summary>
    /// <param name="count"></param>
    /// <param name="Max"></param>
    /// <returns>El porcentaje de count sobre el max</returns>
    public static float KnowPercentOfMax(float count, float max) => count / max * 100;

    /// <summary>
    /// Basado en el porcentaje
    /// //obtienes el porcentaje de curacion basado en tu max
    /// 
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float KnowQtyOfPercent(float percent, float max) => (max / 100) * percent;

    /// <summary>
    /// Detecta si el indice está dentro del arreglo
    /// </summary>
    /// <param name="i"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsOnBoundsArr(int i, int length) => i == Mathf.Clamp(i, 0, length -1);

    /// <summary>
    /// Buscamos el parametro del color que va a ser cambiado.
    /// el parametro debe estar entre los rangos de los parametros de color y en su orden:
    /// [R == 0,G == 1,B == 2,A == 3] --> iniciando en 0.  
    ///
    /// Xav Update 5 diciembre 2020 _>
    ///  Si i es == -1 entonces aplica a (rgb)
    /// </summary>
    /// <param name="c"></param>
    /// <param name="param"></param>
    /// <param name="val"></param>
    /// <returns>Devuelve el color con el cambio en el parametro</returns>
    public static Color SetColorParam(Color c, int i, float val = 1)
    {
        float[] _c = { c.r, c.g, c.b, c.a };

        //Si esta fuera de los limites del arreglo
        if (!IsOnBoundsArr(i, _c.Length))
        {
            if (i == -1)
            {
                for (int x  = 0; x < 3; x++)
                {
                    _c[x] = val;
                }

            }
            else {
                Debug.LogError($"Indice errado ?, favor usar un enum de parametros de color o usarlo bien :(");
            }
        }
        else
        {
            _c[i] = val;
        }

        Color newColor = new Color(_c[0], _c[1], _c[2], _c[3]);
        return newColor;
    }


    /// <summary>
    /// Dependiendo de la condición determinamos si iniciar o apagar la animación
    /// </summary>
    public static void ParticlePlayStop(ParticleSystem particle, bool condition){

        if (condition && particle.isStopped)
        {
            particle.Play();
        }
        //si no estas con poder y esta corriendo las particulas las frenas
        else if (!condition && particle.isPlaying)
        {
            particle.Stop();
        }
    }

    /// <summary>
    /// Obtienes el valor del rango dado 
    /// </summary>
    /// <param name="range"></param>
    public static float Range(float[] range) => Random.Range(range[0], range[1]);

    /// <summary>
    /// Obtienes el valor del rango dado 
    /// </summary>
    /// <param name="range"></param>
    public static int Range(int[] range) => Random.Range(range[0], range[1]);

    /// <summary>
    /// Revisa el valor y en caso de poseer -1 que, en este caso
    /// significa "Por defecto" entonces mantiene el valor anterior
    /// [TODO ver si remover o si es util....]
    /// </summary>
    /// <param name="val"></param>
    /// <param name="lastVal"></param>
    /// <returns>Devuelve un nuevo valor o el anterior del contexto</returns>
    private static float SetOrLet(float val, float lastVal) => val == -1 ? lastVal : val;
}

public enum ColorType{r,g,b,a}

