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
public struct Character
{
    public CharacterType type;

    public float speed;
    public float energy;
    public float jump;
    public float cooldown;

    /// <summary>
    /// Establece el tipo que posee el personaje
    /// basado en los tipos de personajes que hay
    /// </summary>
    public void SetType()
    {
        int i = (int)type;
        speed = CharacterStats.characterStats.speed[i];
        energy = CharacterStats.characterStats.energy[i];
        jump = CharacterStats.characterStats.jump[i];
        cooldown = CharacterStats.characterStats.cooldown[i];
    }
}
public struct Buff
{
    public BuffType type;
    public int count;
}