#region ############### IMPORTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

#region ########### CLASS
public class PlayerController : MonoBehaviour
{
    /*
     * Aqui manejamos el sistema de ataque y salto del player
     * que son los ocntroles del jugador
     */


    #region ################################################# VARIABLES
    // referencia del rigidBody
    private Rigidbody2D rigidBody;

    //La velocidad mas alta que has conseguido antes de chocar con algún monstruo
    public float higherVelocity_X;

    [Header("Jump Settings")]
    public bool canJump = false;
    public bool falling = false;

    #endregion
    #region ################################### Eventos
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameManager.status == GameStatus.InGame)
        {
            CheckPlayerJump();
            Controls();
        }
    }
    private void LateUpdate()
    {
        Movement();
    }
    #endregion

    #region ################################### Methods


    /// <summary>
    /// Se revisa el estado del player
    /// así se sabrá si puede saltar o no.
    /// </summary>
    private void CheckPlayerJump()
    {
        if (rigidBody.velocity.y == 0 && falling) canJump = PowerManager.PlayerJumpUpdate(canJump,true);
        falling = rigidBody.velocity.y < 0;
    }
    /// <summary>
    /// vemos los controles que presiona
    /// </summary>
    private void Controls()
    {
        //detecta la tecla asignada
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonDown("Weapon"))
        {
            Debug.Log("Weapon Pressed");
        }

    }
    /// <summary>
    /// Realiza el salto
    /// </summary>
    public void Jump()
    {
        if (!canJump)
        {
            if (!falling)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -0.1f);
            }
            //rigidBody.AddForce(Vector2.up * GameSetup.character.jump, ForceMode2D.Impulse);
        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * GameSetup.character.jump, ForceMode2D.Impulse);

            canJump = PowerManager.PlayerJumpUpdate(canJump); ;
        }

    }
    public void Movement()
    {
        Vector2 _tempVelocity = rigidBody.velocity;
        _tempVelocity.x = Mathf.MoveTowards(_tempVelocity.x, PlayerManager.player.speedActual , Time.deltaTime);
        //Asigna la velocidad 
        rigidBody.velocity = _tempVelocity;

        //Aumento en caso de que sea mayor
        higherVelocity_X = _tempVelocity.x > higherVelocity_X ? _tempVelocity.x : higherVelocity_X;
    }
    #endregion
}
#endregion
