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


    #endregion
    #region EVENT
    private void Awake()
    {
        if (player == null) player = this;
        else if (player != this) Destroy(gameObject);
    }
   
    private void Update()
    {
        if (GameManager.status == GameStatus.InGame)
        {
            UpdateEnergy();
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
    }



    /// <summary>
    /// Actualiza el estado de la barra de energía
    /// para que esta se reduzca poco a poco a medida que el jugador juega
    /// </summary>
    private void UpdateEnergy()
    {
        energyActual -= Time.deltaTime / Data.data.lifeReductor;

    }

    /// <summary>
    /// Revisa el estado del jugador, en caso de no tener energía
    /// se llama al gameOver
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (energyActual < 0)
        {
            GameManager.GameOver();
        }
    }
    #endregion
}
