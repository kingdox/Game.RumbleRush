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
        BuffType.Energy,
        BuffType.Speed,
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
///
/// Añade tambien la cantidad que se tiene de esta
/// </summary>
[System.Serializable]
public struct Buff
{
    public BuffType type;
    public TKey keyName;

    //- Aqui sabemos cuantos posee el buff
    public int counts;
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

    /// <summary>
    /// Añades o quitas en el contador la cantidad que quieras
    /// limitado entre el 0 y el limite de buffs
    /// </summary>
    public void ModifyCount(int c) => counts = Mathf.Clamp(counts + c, 0, Data.data.maxBuffCount);
}

public enum BuffType
{
    Energy,
    Speed,
    Shield
}

