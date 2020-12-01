using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    #region VARIABLES
    private bool isDead = false;
    private Rigidbody2D rigidbody2D;
    public BoxCollider2D boxCollider2D;

    public MonsterType type;

    private float cooldownMovement_Y;

    #endregion
    #region Events
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (GameManager.status == GameStatus.InGame)
        {
            Movement();
            DestroyChecker();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollisions(collision.transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollisions(collision.transform);
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisamos la etiqueta de lo que hemos colisionado, si es un player
    /// entonces efectuamos los cambios correspondientes
    /// </summary>
    /// <param name="tr"></param>
    private void CheckCollisions(Transform tr)
    {
        if (tr.CompareTag("Player") && !isDead && GameManager.status == GameStatus.InGame)
        {   
            isDead = true;
            PlayerManager.player.RemoveLife(type);
            PlayerManager.player.ResetPowerBar();
            PlayerManager.player.rigi2D_player.velocity = new Vector2(0, PlayerManager.player.rigi2D_player.velocity.y);
            boxCollider2D.enabled = false;
            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

    }


    /// <summary>
    /// Asigna el movimiento del monstruo en dirección contraria a la del jugador
    /// en caso de ser volador trata de acercarse al eje y del jugador
    /// </summary>
    public void Movement()
    {
        Vector2 _tempVelocity = rigidbody2D.velocity;
        _tempVelocity.x -= (Time.deltaTime * Data.data.monsterSpeed) ;


        //En caso de no haber pasado al player todavía lo intentará seguir

        if (type == MonsterType.Monster_Aero )
        {

            if (cooldownMovement_Y > 0.25f){
                //tenemos el player fisico
                Vector2 _playerPos = PlayerManager.player.obj_player.transform.position;

                if (transform.position.x > _playerPos.x){

                    bool isUpper = transform.position.y > _playerPos.y;
                    // acerca su eje Y a el player
                    // si el jugador está encima entonces será negativa, sino positiva,
                    //esta luego impulsará un poco pero no al completo...
                    float direction = isUpper ? -1 : 0;
                    _tempVelocity.y += (Time.deltaTime * Data.data.monsterSpeed / 2) * direction;

                }
                else
                {
                    _tempVelocity.y/= 2;
                }
            }
            else
            {
                cooldownMovement_Y += Time.deltaTime;
            }

        }


        rigidbody2D.velocity = _tempVelocity;
    }


    /// <summary>
    /// revisa si se destruye:
    /// Basado con la posición de la camara en eje X, si sale de pantalla por la izquierda
    /// si la Y es inferior a un area safe de Y
    /// </summary>
    private void DestroyChecker(){
        //buscamos la camara
        float _cam_X = GameManager.GetCamera().transform.position.x;
        //conocemos su ancho
        float w = GameManager.GetCameraWidth();
        float h = GameManager.GetCameraHeight();

        bool passBoundX = transform.position.x < (_cam_X - w);

        bool passBoundY = transform.position.y < (h * -1);

        if (passBoundX || passBoundY)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}