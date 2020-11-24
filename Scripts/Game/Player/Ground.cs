using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    #region Var

    private bool isGround = false;
    public PlayerController playerController;
    #endregion
    #region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Platform") && isGround)
        {

            Debug.Log("ÑÑÑÑ + " + collision.name);

        }
    }
    #endregion
    #region Methods

    #endregion
}

#region DEBUG TALK
/*
 * TODO
 * Debería calcular rigidbody.velocity de player y ver si es menor 
 * a 0 significa que esta cayendo, por lo qeu podríamos saber si debería permitirse 
 * saber si va a grounded o no
 */
#endregion