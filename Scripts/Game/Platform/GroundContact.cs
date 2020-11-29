#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
public class GroundContact : MonoBehaviour
{
    #region Var
    private BoxCollider2D boxCollider;
    public GameObject platform;
    public GameObject indicatorPlayer;
    #endregion
    #region Events
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
        CheckTriggerWithPlayer(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckTriggerWithPlayer(collision);
    }
    #endregion


    #region Methods

    private void CheckTriggerWithPlayer(Collider2D collision)
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
    #endregion
}