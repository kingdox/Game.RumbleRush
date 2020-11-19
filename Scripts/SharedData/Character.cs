using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aquí se posee toda la información
/// referente a los Characters de cada uno.
/// </summary>
public class CharacterData
{
    [HideInInspector]
    public static CharacterData cD = new CharacterData();

    public readonly CharacterType[] characters =
    {
        CharacterType.Monk,
        CharacterType.Paladin,
        CharacterType.Hunter,
        CharacterType.Brutus,
    };

    public readonly TKey[] charKeys =
    {
        TKey.CHAR_Monk,
        TKey.CHAR_Paladin,
        TKey.CHAR_Hunter,
        TKey.CHAR_Brutus
    };
    public readonly TKey[] powKeys =
    {

        TKey.POW_Monk,
        TKey.POW_Paladin,
        TKey.POW_Hunter,
        TKey.POW_Brutus
    };
    //-> velocidad de los objetos que vendrán
    public readonly float[] speed = { 11, 1, 1, 1 };
    //-> vida del personaje
    public readonly float[] energy = { 1, 11, 1, 1 };
    //-> fuerza del salto
    public readonly float[] jump = { 1, 1, 11, 1 };
    //-> Cooldown de la habilidad
    public readonly float[] cooldown = { 1, 1, 11, 1 };
    //-> Costo para usarlo
    public readonly int[] cost = { 0, 22, 33, 44 };

}

/// <summary>
/// Modelo con los parametros del personaje y  adición de sus datos.
/// </summary>
[System.Serializable]
public struct Character
{
    public CharacterType type;
    public TKey keyName;
    public TKey keyPower;


    public float speed;
    public float energy;
    public float jump;
    public float cooldown;
    public int cost;

    /// <summary>
    /// Establece el tipo que posee el personaje
    /// basado en los tipos de personajes que hay
    /// </summary>
    public void SetType(int index = -1)
    {
        int i = index == -1 ? (int)type : index;
        type = CharacterData.cD.characters[i];
        keyName = CharacterData.cD.charKeys[i];
        speed = CharacterData.cD.speed[i];
        energy = CharacterData.cD.energy[i];
        jump = CharacterData.cD.jump[i];
        cooldown = CharacterData.cD.cooldown[i];
        cost = CharacterData.cD.cost[i];
        keyPower = CharacterData.cD.powKeys[i];
    }
}

/// <summary>
/// Enumeramos los tipos de personajes
/// Con el indice tomaremos la información correspondiente
/// </summary>
public enum CharacterType
{
    Monk,
    Paladin,
    Hunter,
    Brutus
}