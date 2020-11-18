using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO
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
//TODO ver como usar esto....
/// <summary>
/// Aquí manejaremos con el index los cambios de la escena,
/// sin tener que HARDCODEAR
/// </summary>
public enum SceneIndex
{
    Menu = 0,
    Instruction = 1,
    Preparation = 2,
    Game = 3,
}
public enum BuffType
{
    Speed,
    Energy,
    Shield
}


#region ##### MSG
/*
    Aqui en este script se coloca todo lo que de momento no se ha encontrado
un sitio correcto donde colocarlo, de todas formas este sería el
segundo mejor sitio para percibirlo...

 */
#endregion