using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Poseedor de las traducciones en español
/// de ciertas palabras usadas recurrentemente
///
/// </summary>
public class ES
{
    [HideInInspector]
    public static ES es = new ES();
    private readonly string[] value = {
        "Metros: ",
        "Monstruos: ",
        "Monedas: ",
        "M",
        "$"
    };
    /// <summary>
    /// Te devuelve el resultado de los datos para
    /// la traducción especificadaa
    /// </summary>
    /// <param name="enumKey"></param>
    /// <returns></returns>
    public string Trns(TKey enumKey) => value[(int)enumKey];
}

/// <summary>
/// Contenedor de las llaves de los translate comunes
/// </summary>
public enum TKey
{
    Metters,
    Monsters,
    Money,
    SIGN_Metters,
    SIGN_Money

}