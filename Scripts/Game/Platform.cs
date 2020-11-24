using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Var

    private BoxCollider2D boxCollider;
    public GameObject platform;


    #endregion
    #region Events
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        //Ajusta el collider con el tamaño de la caja
        boxCollider.size = platform.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && boxCollider.isTrigger)
        {
            //Se desactiva el trigger para poseer fisicas
            //con el player y así permitir permanecer en las plataformas
            boxCollider.isTrigger = false;
        }
    }

    #endregion
    #region Methods



    #endregion
}