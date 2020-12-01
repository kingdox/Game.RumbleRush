using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private bool isDead = false;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    /*
     * TODO
     * 
     * Este script contempla:
     * 
     *  - Que se mueva hacia adelante en un rango con velocidad aleatoria
     *  - Que dependiendo del tipo de monstruo aplique o no ciertas cosas
     *  - Que se destruya si hace contacto con player
     *  - Que se destruya si sale de cierto rango o si alcanza x posicion
     *  - que pueda tener colision con el floor, pero no con las plataformas
     *  
     *  
     *  Propuesta: 
     *  
     *  *Que tenga fisicas, pero en el momento que alcanze la pos y inicial
     *  deje de bajar
     *  
     *  *Que tenga un detector del suelo como player para activar su colision?
     
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && !isDead){
            isDead = true;
            PlayerManager.player.energyActual -= Random.Range(5, 10);
            //rigidbody2D.simulated = false;
            //Desactivale el trigger, no el simulated
            //Destroy(gameObject);

        }
    }


}
