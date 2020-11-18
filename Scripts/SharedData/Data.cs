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
    public readonly string version = "v0.0.2";

    
    #region ### METHOD
    /// <summary>
    /// Cambiamos a la escena indicada
    /// </summary>
    /// <param name="index"></param>
    public void ChangeSceneTo(int index) => SceneManager.LoadScene(index);
    #endregion
}

//TODO
/// <summary>
/// Hacemos una clase con las propiedades
/// Estas propiedades sirven para ser tomadas
/// </summary>
public class CharacterStats
{
    [HideInInspector]
    public static CharacterStats characterStats = new CharacterStats();

    //-> velocidad de los objetos que vendrán
    public readonly float[] speed = { 1, 1, 1, 1 };
    //-> vida del personaje
    public readonly float[] energy = { 1, 1, 1, 1 };
    //-> fuerza del salto
    public readonly float[] jump = { 1, 1, 1, 1 };
    //-> Cooldown de la habilidad
    public readonly float[] cooldown = { 1, 1, 1, 1 };
}
