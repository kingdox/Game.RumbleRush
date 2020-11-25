using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContact : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject platform;
    public bool isField = false;

    private void Awake()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = !isField;
        //Ajusta el collider con el tamaño de la caja
        boxCollider.size = platform.transform.localScale;
        boxCollider.offset = platform.transform.localPosition;
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
}