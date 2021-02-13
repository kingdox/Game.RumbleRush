using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private Rigidbody2D rigi2D;

    private Camera cam;
    private float cam_h;
    private float cam_w;

    private bool isDestroying = false;
    private Rigidbody2D rigid_player;

    private readonly float bulletSpeed = 75;
    private float summumSpeed = 1;

    private void Awake()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        rigid_player = PlayerManager.player.rigi2D_player;
        cam = GameManager.GetCamera();
        cam_h = DataFunc.GetScreenHeightUnit(cam);
        cam_w = DataFunc.GetScreenWidthUnit(cam_h);
    }
    //Se moverá hacía la izquierda hasta que se destruya por colisionar con un enemigo o si se sale de los limites de la pantalla

    private void Update()
    {
        if (GameManager.status.Equals(GameStatus.InGame))
        {   
            DestroyChecker();
            Movement();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") && !isDestroying){
            isDestroying = true;
            Debug.Log("Collhunter");
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Mueve la bala vasado en la velocidad del player y un extra
    /// </summary>
    private void Movement()
    {
        Vector2 speed = Vector2.right * rigid_player.velocity.x;

        summumSpeed = (Time.deltaTime * speed.x) + bulletSpeed ;

        speed.x += summumSpeed;
        rigi2D.velocity = speed;

    }

    /// <summary>
    /// revisa si se destruye por los limites de la camara
    /// </summary>
    private void DestroyChecker()
    {
            float _cam_X = cam.transform.position.x;
            //Si pasa adelante de los limites
            bool passBoundX = transform.position.x > (_cam_X + cam_w);

            if (passBoundX)
            {
                Destroy(gameObject);
                isDestroying = true;

            }
    }
}
