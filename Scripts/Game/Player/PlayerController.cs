#region ############### IMPORTS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

#region ########### CLASS
public class PlayerController : MonoBehaviour
{
    #region ################################################# VARIABLES
    // referencia del rigidBody
    private Rigidbody2D rigidBody;

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
        CheckPlayer();

        Controls();
        //DeadZoneCheck();
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
    private void CheckPlayer()
    {
        if (rigidBody.velocity.y == 0 && falling) canJump = true;
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
    }
    /// <summary>
    /// Realiza el salto
    /// </summary>
    public void Jump()
    {
        if (!canJump) return;

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(Vector2.up * GameSetup.character.jump, ForceMode2D.Impulse);

        canJump = false;
    }
    public void Movement()
    {
        //revisamos si no esta moviendose automaticamente, de ser así se va
        if (GameManager.paused) return;

        Vector2 _tempVelocity = rigidBody.velocity;
        //TODO
        _tempVelocity.x = Mathf.MoveTowards(_tempVelocity.x, GameSetup.character.speed + 5, (Time.deltaTime * GameSetup.character.speed));

        //Asigna la velocidad 
        rigidBody.velocity = _tempVelocity;

    }
    #endregion
}
#endregion

#region ##### DEBUG TEXT

#endregion