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

    public readonly string savedPath = "saved8.txt";
    public readonly string version = "v0.1.1";
    
    //Datos especificos

    // -> Valor de cada monstruo eliminado
    public readonly int monsterValue = 5;
    // -> Cantidad maxima de un tipod e buff
    public readonly int maxBuffCount = 20;

    [Header("Player")]
    public readonly float lifeReductor = 10;

    [Header("Platform info")]
    public readonly int limitPlatformsInGame = 7;

    //-> Usado para saber el rango separador entre las plataformas para no generarse pegado.
    public readonly float platformRangeY = 3.0f;
    //-> Usado para conocer el rango se
    public readonly float platformRangeX = 10.0f;

    [Header("Floor info")]
    public readonly int limitBaseFloorInGame = 3;

    public readonly float floorRangeX = 5.0f;

    public enum Scenes
    {
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
    /// sacas el alto aprox de una camara o la activa por defecto
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
            ? (_index == indexLength ? 0 : _index + 1)
            : (_index == 0 ? indexLength : _index - 1)
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

}