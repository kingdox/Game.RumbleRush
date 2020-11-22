using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataStorage
{
    //aquí se vuelve a colocar los datos puestos debajo...
    public SavedData savedData = new SavedData();
    //Con esto podremos guardar los datos de datapass a DataStorage
    public DataStorage(SavedData saved)
    {
        savedData = saved;
    }
}

/// <summary>
/// Este es el modelo de datos que vamos a guardar y manejar
/// para los archivos que se crean...
/// </summary>
[System.Serializable]
public struct SavedData
{
    public int actualmoney;
    public float recordMetersReached;
    public int recordMonstersKilled;
    public int lastMoneySpent;
    public float lastMetersReached;
    public int lastMonstersKilled;

    //Extra
    [Space(10)]
    [Header("Debug Area")]
    public int debug_savedTimes;
};
