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
    public float jumpForce = 7.0f;
    public bool canJump = false;
    public bool falling = false;

    [Header("Ground Settings")]

    //referecia al transform de groundCheck
    public Transform transform_ground;

    //[Header("Movement Settings")]
    //public bool autoMove = true;
    //public float maxSpeed = 5f;// velocidad permitida
    //public float acceleration = 20f; // aceleración aplicada

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
        //Movement();
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

       Gizmos.DrawWireCube(transform.position, transform.localScale);
        /*  Gizmos.color = Color.blue;
          Gizmos.DrawWireCube(front_Check.position, front_SizeFrontCheck);
          */
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
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        canJump = false;
    }
    /*
    public void Movement()
    {
        //revisamos si no esta moviendose automaticamente, de ser así se va
        if (!autoMove || (!ground_grounded && rigidBody.velocity.y < 0)) return;

        Vector2 _tempVelocity = rigidBody.velocity;

        _tempVelocity.x = Mathf.MoveTowards(_tempVelocity.x, maxSpeed, (Time.deltaTime * acceleration));


        //Asigna la velocidad 
        rigidBody.velocity = _tempVelocity;

    }
    */
    #endregion
}
#endregion

#region ##### DEBUG TEXT

#endregion