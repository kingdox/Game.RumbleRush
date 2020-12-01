#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class PlayerManager : MonoBehaviour
{
    //Aquí manejaremos los datos del jugador y estos mismos serán
    //los que pueden afectarse por efectos externos, etc...
    //Aquí se contará la vida entre otros, si se muere el jugador se llama
    //a GameManager desde aquí para llamar gameOver

    /*
     * Qué poseerá este manejador?
     * 
     * - Los parametros del jugador combinado con los buffs
     * - El manejo y contacto con power en caso de su activaciónd esactivación
     * - El checkeo de si sigue jugando
     * 
     */

    #region VAR
    public static PlayerManager player;
   
    [Header("ActualStats")]
    public float speedActual;

    public float jumpActual;

    public float energyMax;
    public float energyActual;


    public float cooldownMax;
    public float cooldownActual;

    //private Character player;

    //Escudos que le permiten evitar golpes de monstruos por cierto tiempo
    public int shields = 0;

    [Header("Info")]
    public float mettersActual;
    public int killsActual;
    

    [Header("Settings")]
    public GameObject obj_player;
    public Rigidbody2D rigi2D_player;


    public float inmuneTimeCount ;

    #endregion
    #region EVENT
    private void Awake()
    {
        if (player == null) player = this;
        else if (player != this) Destroy(gameObject);

        //GameSetup ya posee datos antes de que PlayerManager exista
        LoadPlayer();
    }
   
    private void Update()
    {
        if (GameManager.status == GameStatus.InGame )
        {
            UpdateRun();
            CheckPlayerStatus();

            
        }

    }
    #endregion
    #region Methods

    /// <summary>
    /// Colocamos los datos del character del GameSetup y
    /// los colocamos en el player
    /// </summary>
    public static void LoadPlayer()
    {
        player.speedActual = GameSetup.character.speed;
        player.jumpActual = GameSetup.character.jump;
        player.energyMax= GameSetup.character.energy;
        player.energyActual = player.energyMax;
        player.cooldownMax = GameSetup.character.cooldown;


        //Aprovechamos de colocar las adiciones con los buff...
        for (int x = 0; x < GameSetup.buffs.Length; x++)
        {
            switch (GameSetup.buffs[x].type)
            {
                case BuffType.Energy:
                    player.energyActual += GameSetup.buffs[x].counts;
                    break;
                case BuffType.Speed:
                    player.speedActual += GameSetup.buffs[x].counts;
                    break;
                case BuffType.Shield:
                    player.shields += GameSetup.buffs[x].counts;
                    break;
                default:
                    break;
            }
            
        }

    }



    /// <summary>
    /// Actualiza el estado de la barra de energía
    /// para que esta se reduzca poco a poco a medida que el jugador juega
    /// también aumenta los metros recorridos...
    /// </summary>
    private void UpdateRun()
    {
        inmuneTimeCount += Time.deltaTime;
        energyActual -= Time.deltaTime / Data.data.lifeReductor;
        
        mettersActual = (float)System.Math.Round(obj_player.transform.position.x / Data.data.metter ,1);
        cooldownActual = Mathf.Clamp(cooldownActual + Time.deltaTime , 0, cooldownMax);

        //Si recorres los metros faciles entonces es modo hard
        GameSetup.hardMode = GameSetup.easyMetters < mettersActual;

    }

    /// <summary>
    /// Se resetea el cooldown para que pierda el estado del poder en caso de que este
    /// haya sido cargado
    /// </summary>
    public void ResetPowerBar()
    {
        cooldownActual = 0;
    }

    /// <summary>
    /// Reduces en cierta medida la vida del jugador
    /// </summary>
    /// <param name="type"></param>
    public void RemoveLife(MonsterType type){
        if (inmuneTimeCount < Data.data.playerInmuneTimeCountLimit) return;
        inmuneTimeCount = 0;

        if (shields-- > 0) return;


        int[] range = {};

        switch (type)
        {
            case MonsterType.Monster_Floor:
                range = Data.data.monsterDamageRange_floor;
                break;
            case MonsterType.Monster_Aero:
                range = Data.data.monsterDamageRange_aero;
                break;
        }

        player.energyActual -= Random.Range(range[0], range[1]);
    }


    /// <summary>
    /// Revisa el estado del jugador, en caso de no tener energía
    /// se llama al gameOver
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (energyActual < 0 || obj_player.transform.position.y < -2)
        {
            GameManager.GameOver();
        }
    }
    #endregion
}
