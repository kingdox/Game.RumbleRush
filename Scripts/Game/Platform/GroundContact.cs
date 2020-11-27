using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundContact : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject platform;
    public GameObject indicatorPlayer;

    private void Awake()
    {

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        //Ajusta el collider con el tamaño de la caja
        boxCollider.size = platform.transform.localScale;
        boxCollider.offset = platform.transform.localPosition;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && boxCollider.isTrigger)
        {
            //Se desactiva el trigger para poseer fisicas
            //con el player y así permitir permanecer en las plataformas
            //boxCollider.isTrigger = false;


            //Posición que hay que superar en alto
            float indicatorPlayer_Y = indicatorPlayer.transform.position.y;
            float indicatorPlayer_X = indicatorPlayer.transform.position.x;

            //
            float player_Y = collision.transform.position.y;
            float player_X = collision.transform.position.x;

            if (
                indicatorPlayer_Y < player_Y
                && indicatorPlayer_X < player_X
                )
            {
                boxCollider.isTrigger = false;
            }

        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Ground") && boxCollider.isTrigger)
    //    {
    //        //Se desactiva el trigger para poseer fisicas
    //        //con el player y así permitir permanecer en las plataformas
    //        //boxCollider.isTrigger = false;


    //        //Posición que hay que superar en alto
    //        float indicatorPlayer_Y = indicatorPlayer.transform.position.y ;
    //        float indicatorPlayer_X = indicatorPlayer.transform.position.x;

    //        //
    //        float player_Y = collision.transform.position.y;
    //        float player_X = collision.transform.position.x;

    //        if (
    //            indicatorPlayer_Y < player_Y
    //            && indicatorPlayer_X < player_X
    //            )
    //        {
    //            boxCollider.isTrigger = false;
    //        }

    //    }
    //}
}