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
    private Animator animator;
    private Rigidbody2D rigidBody;
    public BoxCollider2D boxCollider2D;
    //La velocidad mas alta que has conseguido antes de chocar con algún monstruo
    public float higherVelocity_X;

    [Header("Jump Settings")]
    public bool canJump = false;
    public bool falling = false;
    //Se pone true cuando fuerzas a caer
    public bool forceFall = false;
    private bool grounded = false;
    [Space]
    public ParticleSystem part_jump;
    public ParticleSystem part_ground;


    [Header("Weapon Settings")]
    public Animator anim_weapon;
    public float weaponCooldownCount = 0;
    public bool weaponReady = false;
    private float weaponCdLimit;

    [Space]
    public GameObject obj_extraHunter;
    public GameObject pref_hunterBullet;



    [Header("Power Class Efffects")]
    public TrailRenderer power_monk;
    public ParticleSystem power_paladin;
    public ParticleSystem power_hunter;
    public ParticleSystem power_brutus;


    #endregion
    #region ################################### Eventos
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        anim_weapon.SetInteger("type", (int)GameSetup.character.type);

        weaponCdLimit = CharacterData.cD.weaponCooldown[(int)GameSetup.character.type];
        weaponCooldownCount = weaponCdLimit;
        weaponReady = true;

        //Pausamos los efectos de los poderes de cada clase
        power_monk.emitting = false;
        power_paladin.Stop();
        power_hunter.Stop();
        power_brutus.Stop();
    }
    private void Update()
    {
        CheckPlayerJump();

        if (GameManager.status == GameStatus.InGame )
        {
            Controls();
            CheckWeaponStatus();
        }
    }
    private void LateUpdate()
    {
        
        Movement();
        CheckPowerEffects();

    }
    #endregion

    #region ################################### Methods


    private void CheckPowerEffects(){

        switch (GameSetup.character.type)
        {
            case CharacterType.Monk:
                power_monk.emitting = PowerManager.IsPoweOn();
                break;
            case CharacterType.Paladin:
                DataFunc.ParticlePlayStop(power_paladin, PowerManager.IsPoweOn());
                break;
            case CharacterType.Hunter:
                DataFunc.ParticlePlayStop(power_hunter, PowerManager.IsPoweOn());
                break;
            case CharacterType.Brutus:
                DataFunc.ParticlePlayStop(power_brutus, PowerManager.IsPoweOn());
            break;
        }

    }

    /// <summary>
    /// Revisamos cuando el estado del arma esta lista para su uso.
    /// </summary>
    private void CheckWeaponStatus()
    {
        //weaponCooldownCount += Time.deltaTime;
        weaponCooldownCount = Mathf.Clamp(weaponCooldownCount + Time.deltaTime, 0, weaponCdLimit);

        if (weaponCooldownCount == weaponCdLimit && !weaponReady)
        {
            weaponReady = true;
            UIManager.WeaponReady();
            MusicSystem.ReproduceSound(MusicSystem.SfxType.WeaponReady);
            // animación de UI MANAGER
        }

        if (weaponReady && weaponCooldownCount != weaponCdLimit){

            weaponReady = false;
        }
        
    }

    /// <summary>
    /// Se revisa el estado del player
    /// así se sabrá si puede saltar o no.
    /// </summary>
    private void CheckPlayerJump()
    {
        //Revisamos si estaba cayendo y, ahora ya está en el suelo
        grounded = rigidBody.velocity.y == 0;

        animator.SetBool("isGrounded", grounded && falling);

        if (grounded) {

            if (GameManager.status.Equals(GameStatus.InGame)){
                //dar un time para la siguiente...
                MusicSystem.ReproduceSound(MusicSystem.SfxType.FootSteps);
            }

            if (falling)
            {
                canJump = PowerManager.PlayerJumpUpdate(canJump,true);
                //Reestableces la opción de forzar caida
                forceFall = false;
                part_ground.Play();
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Grounded);
            }

        }

        //Actualizamos el estado de la caida
        falling = rigidBody.velocity.y < -0f;

    }
    /// <summary>
    /// vemos los controles que presiona
    /// </summary>
    private void Controls()
    {

        //detecta la tecla asignada
        if (Input.GetButtonDown("Jump"))
        {
            UIManager.AnimateButton(false, forceFall || falling && !canJump);


            if (!forceFall)
            {
                Jump();
            }
            else
            {
                MusicSystem.ReproduceSound(MusicSystem.SfxType.ButtonOff);
            }

            //Animamos el boton para cuando ya exedió su uso..
        }


        //si tocan el boton del arma
        if (Input.GetButtonDown("Weapon"))
        {

            bool canUseWeapon = weaponCooldownCount >= CharacterData.cD.weaponCooldown[(int)GameSetup.character.type];
            //Si ha pasado el tiempo para que el cooldown haya terminado
            if (canUseWeapon)
            {
                //reseteamos el conteo
                weaponCooldownCount = 0;

                WeaponAction();
            }
            else
            {
                MusicSystem.ReproduceSound(MusicSystem.SfxType.ButtonOff);
            }


            //Usamos el boton y si esta habilitado o no haremos una animaciónd e los botones del HUD
            UIManager.AnimateButton(true, !canUseWeapon);
        }

        //Tecla para irse a pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MusicSystem.ReproduceSound(MusicSystem.SfxType.ButtonOn);
            GameManager.OnOffPause(true);
        }


        //DEBUG

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    PlayerManager.player.energyActual = 0;
        //}
    }


    /// <summary>
    /// Usamos el arma del jugador, dependiendo del heroe usaremos uno o toro 
    /// </summary>
    public void WeaponAction()
    {
        //Debug.Log("Weapon Pressed");
        //Animamos el arma
        anim_weapon.SetTrigger("attack");
        //Animamos al personaje
        animator.SetTrigger("attack");
        MusicSystem.ReproduceSound(MusicSystem.SfxType.Weapon);


        //SOLO en caso de ser Hunter lanzas una bala
        if (GameSetup.character.type == CharacterType.Hunter)
        {
            Vector3 bulletPos = transform.position + (Vector3.right * 5);

            //obj_extraHunter
            Instantiate(
                pref_hunterBullet,
                bulletPos,
                Quaternion.identity,
                obj_extraHunter.transform
            );
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
                forceFall = true;
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Fall);
            }
            else
            {
                MusicSystem.ReproduceSound(MusicSystem.SfxType.ButtonOff);
            }
            //rigidBody.AddForce(Vector2.up * GameSetup.character.jump, ForceMode2D.Impulse);
        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * GameSetup.character.jump, ForceMode2D.Impulse);
            canJump = PowerManager.PlayerJumpUpdate(canJump);
            part_jump.Play();
            MusicSystem.ReproduceSound(MusicSystem.SfxType.Jump);
        }

    }
    public void Movement()
    {
        Vector2 _tempVelocity = rigidBody.velocity;

        if (GameManager.status == GameStatus.InGame)
        {
            _tempVelocity.x = Mathf.MoveTowards(_tempVelocity.x, PlayerManager.player.speedActual, Time.deltaTime);
        }
        else if(GameManager.status == GameStatus.GameOver)
        {
            _tempVelocity.x /= 2; 
        }
            
        //Asigna la velocidad 
        rigidBody.velocity = _tempVelocity;

        //Aumento en caso de que sea mayor
        higherVelocity_X = _tempVelocity.x > higherVelocity_X ? _tempVelocity.x : higherVelocity_X;
    }


    #endregion
}
#endregion
