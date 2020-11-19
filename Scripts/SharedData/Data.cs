using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Representa los datos basicos del enviroment/app
/// </summary>
public class Data 
{
    [HideInInspector]
    public static Data data = new Data();

    public readonly string savedPath = "saved7.txt";
    public readonly string version = "v0.0.4";


    #region ### METHOD
    /// <summary>
    /// Cambiamos a la escena indicada
    /// </summary>
    /// <param name="index"></param>
    public void ChangeSceneTo(int index) => SceneManager.LoadScene(index);
    #endregion
}



/// <summary>
/// Aquí se poseerán las funcionalidades
/// que pueden usarse en otros sitios
/// </summary>
public struct DataFunc
{
    public static DataFunc _ = new DataFunc();

    /// <summary>
    /// Te permite ir hacia adelante o hacia atrás en un arreglo sin salirte de los limites
    /// donde indexLength es el rango posible del index (arr.length - 1);
    /// _index es la pos actual en el arreglo, en caso de no haber es 0
    /// </summary>
    /// <returns>la nueva posición en el arreglo</returns>
    public int TravelArr(bool goNext, int indexLength, int _index = 0 )
    {
        int i = goNext
            ? (_index == indexLength ? 0 : _index + 1)
            : (_index == 0 ? indexLength : _index - 1)
        ;
        return i;
    }
}