using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public struct Buff
{
    public BuffType type;
    public int count;
}