using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Representa los datos basicos del enviroment/app
/// </summary>
public class Data 
{
    [HideInInspector]
    public static Data data = new Data();
    public readonly string savedPath = "saved7.txt";
    public readonly string version = "v0.0.2";


    
}


/// <summary>
/// Este es el modelo de datos que vamos a guardar y manejar
/// para los archivos que se crean...
/// </summary>
[System.Serializable]
public class SavedData 
{
    public  int actualmoney = 0;
    public  float recordMetersReached = 0;
    public  int recordMonstersKilled = 0;
    public  int lastMoneySpent = 0;
    public  float lastMetersReached = 0;
    public  int lastMonstersKilled = 0;

    //Extra
    [Space(10)]
    [Header("Debug Area")]
    public int debug_savedTimes = 0;
};


/// <summary>
/// Hacemos una clase con las propiedades
/// Estas propiedades sirven para ser tomadas
/// </summary>
public class DataCharacter
{
    public readonly CharacterType type; // los tipos de character

    //-> velocidad de los objetos que vendrán
    public readonly float[] speed = { 1, 1, 1, 1 };
    //-> vida del personaje
    public readonly float[] energy = { 1, 1, 1, 1 };
    //-> fuerza del salto
    public readonly float[] jump = { 1, 1, 1, 1 };
    //-> Cooldown de la habilidad
    public readonly float[] cooldown = { 1, 1, 1, 1 };

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


/// <summary>
/// Aquí manejaremos con el index los cambios de la escena,
/// sin tener que HARDCODEAR
/// </summary>
//[System.Serializable]
//[SerializeField]
public enum SceneIndex
{
    Menu = 0,
    Instruction = 1,
    Preparation = 2,
    Game = 3,
}