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
    public readonly string version = "v0.1.0";
    
    //Datos especificos

    // -> Valor de cada monstruo eliminado
    public readonly int monsterValue = 5;
    // -> Cantidad maxima de un tipod e buff
    public readonly int maxBuffCount = 99;

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
/// </summary>
public struct DataFunc
{

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
    /// Cambiamos a la escena indicada
    /// </summary>
    /// <param name="index"></param>
    public static void ChangeSceneTo(int index) => SceneManager.LoadScene(index);

}