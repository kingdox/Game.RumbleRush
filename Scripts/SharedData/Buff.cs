using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Aquí poseemos toda la información referente a los buff
/// </summary>
public class BuffData
{
    [HideInInspector]
    public static BuffData _ = new BuffData();


    // -> Los tipos de buff
    public readonly BuffType[] buffTypes =
    {
        BuffType.Speed,
        BuffType.Energy,
        BuffType.Shield
    };

    // -> Las llaves para los textos de cada buff 
    public readonly TKey[] nameKeys =
    {
        TKey.BUFF_Energy,
        TKey.BUFF_Speed,
        TKey.BUFF_Shield
    };

    // -> Donde sabremos los precios de los buff
    public readonly int[] cost = { 25, 10, 35 };

}


/// <summary>
/// Modelo del buff con el tipo, nombre y su coste
/// Basado en el tipo este se puede rellenar
/// </summary>
[System.Serializable]
public struct Buff
{
    public BuffType type;
    public TKey keyName;


    public int cost;

    /// <summary>
    /// Establece el tipod e buff que es basado en el index otorgado
    /// ó por defecto en el tipo qeu posea
    /// </summary>
    /// <param name="index"></param>
    public void SetBuff(int index = -1)
    {
        int i = index == -1 ? (int)type : index;
        type = BuffData._.buffTypes[i];
        cost = BuffData._.cost[i];
        keyName = BuffData._.nameKeys[i];

    }
}

public enum BuffType
{
    Speed,
    Energy,
    Shield
}

