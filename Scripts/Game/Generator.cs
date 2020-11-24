#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class Generator : MonoBehaviour
{
    #region Var

    /// <summary>
    /// Con esto sabremos la posición y el tamó de esta plataforma,
    /// evitandonos porblemas...
    /// Con el Indicator podremos saber el inicio de la plataforma
    ///
    /// Poseeremos todas las creadas en esta tanda
    /// </summary>
    public GameObject[] lastPlatforms;

    /// <summary>
    /// Con esta determinaremos cuando es conveniente borrar las plataformas de la tanda
    /// Cuando traspasan sus "Indicator" significa que pueden ser eliminados :)
    /// </summary>
    public float clearOnX = 0.0f;


    [Header("BaseFloor Settings")]

    ///Este servirá para tener el piso principal, este es unico y tiende a ser largo ?
    public GameObject pref_InitialPlatform;

    #endregion
    #region Events
    void Start()
    {
        
    }
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        
    }
    #endregion
    #region Methods
    #endregion

}



#region DEBUG TALK

/*
 * TODO
 * 
 * Entre las plataformas a generar en un cierta pos.X
 * se determinará si puede hacer más de una (1,3)
 * de manera que se pueda crear más de una pero se comprobará si ninguna influye entre ellas 
 * habrá que apilar todas las creadas en esa ronda
 */
#endregion
