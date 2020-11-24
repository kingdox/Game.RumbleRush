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
    public bool canJumpAgain = false;

    [Header("Ground Settings")]

    //referecia al transform de groundCheck
    public Transform transform_ground;

    //layers en los que considerará estar grounded
    //public LayerMask ground_Layer;

    //public Vector2 ground_SizeGroundCheck = new Vector2(0.64f, 0.9f);

    //indica si estamos tocando el suelo...
    //public bool ground_grounded = false;

    /*
    [Header("Front Check")]
    public Transform front_Check; //referecia al transform de frontCheck
    public Vector2 front_SizeFrontCheck = new Vector2(0.64f, 0.9f);
    public bool front_isContact = false; //indica si estamos tocando el muro de en frente
    */

    //[Header("Movement Settings")]
    //public bool autoMove = true;
    //public float maxSpeed = 5f;// velocidad permitida
    //public float acceleration = 20f; // aceleración aplicada

    #endregion

    #region ################################### Eventos

    private void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        //BoxCollider2D o = GetComponent<BoxCollider2D>();
        //o.isTrigger
    }

    private void Update()
    {
        //EvaluePhysics();
        Controls();
        //DeadZoneCheck();
    }

    private void LateUpdate()
    {
        //Movement();
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
       // Gizmos.DrawWireCube(ground_Check.position, ground_SizeGroundCheck);
        /*  Gizmos.color = Color.blue;
          Gizmos.DrawWireCube(front_Check.position, front_SizeFrontCheck);
          */
    }

    #endregion

    #region ################################### Methods

    /// <summary>
    /// Evaluamos si está tocando o no el suelo
    /// </summary>
    private void EvaluePhysics()
    {
        /*

        ground_grounded = Physics2D.OverlapBox(ground_Check.position, ground_SizeGroundCheck, 0f, ground_Layer);
        if (ground_grounded)
        {
            jumpsInAirCurrent = 0;
            //canJumpAgain = true;
        }
        //  front_isContact = Physics2D.OverlapBox(front_Check.position, front_SizeFrontCheck, 0f, ground_Layer);
        */
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
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        /*
            //en caso de poder saltar de nuevo
            if (ground_grounded || jumpsInAirCurrent < jumpsIntAllowed)
            {
                // limpiamos el impulso de caída
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                //canJumpAgain = false;
            }

            if (!ground_grounded)
            {
                jumpsInAirCurrent++;
            }
        */
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