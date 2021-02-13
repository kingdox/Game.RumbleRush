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
     * - manejo de animación del player
     */

    #region VAR
    public static PlayerManager player;
   
    [Header("ActualStats")]
    //Aquí poseeremos la speed base sin añadidos
    public float speedBase;
    public float speedActual;


    public float jumpActual;

    public float energyMax;
    public float energyActual;


    public float cooldownMax;
    public float cooldownActual;

    //Escudos que le permiten evitar golpes de monstruos por cierto tiempo
    public int shieldsActual = 0;

    [Header("Info")]
    public float mettersActual;
    public int killsActual;

    public int collectedMoney;

    [Header("Settings")]
    public GameObject obj_player;
    public Rigidbody2D rigi2D_player;
    public PlayerController playerController;
    public Animator anim_player;
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

    private void Start()
    {
        anim_player.SetInteger("indexCharacter", (int)GameSetup.character.type);

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
        //player.cooldownActual = player.cooldownMax;

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
                    player.shieldsActual += GameSetup.buffs[x].counts;
                    break;
            }
            
        }

        //Establecemos la base de velocidad
        player.speedBase = player.speedActual;

    }



    /// <summary>
    /// Actualiza el estado de la barra de energía
    /// para que esta se reduzca poco a poco a medida que el jugador juega
    /// también aumenta los metros recorridos...
    /// revisa ciertos estados que pueden actualizarse
    /// </summary>
    private void UpdateRun()
    {

        anim_player.SetFloat("velocityX", player.rigi2D_player.velocity.x);
        anim_player.SetFloat("velocityY", player.rigi2D_player.velocity.y);



        inmuneTimeCount += Time.deltaTime;
        energyActual = Mathf.Clamp(PowerManager.PlayerEnergyUpdate(),-1,energyMax);
        //energyActual -= Time.deltaTime / Data.data.lifeReductor;
        speedActual = PowerManager.PlayerSpeedUpdate();

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
        //if (shieldsActual <= 0)
        //{
            cooldownActual = 0;
        //}
    }

    /// <summary>
    /// Reduces en cierta medida la vida del jugador
    /// </summary>
    /// <param name="type"></param>
    public void RemoveLife(MonsterType type){
        if (inmuneTimeCount < Data.data.playerInmuneTimeCountLimit) return;
        inmuneTimeCount = 0;

        if (shieldsActual > 0) {

            shieldsActual -= 1;
            UIManager.SetVisualStat((int)BuffType.Shield, shieldsActual);
            UIManager.VisualEff(VisualEffType.Special);
            return;
        };


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

        player.energyActual -= DataFunc.Range(range);

        //Animamos la pantalla para dar el efecto de atacados
        UIManager.VisualEff(VisualEffType.Damaged);
    }


    /// <summary>
    /// Si coges botiquín te curas
    /// Aleatoriamente entre unos parametros
    /// </summary>
    public void HealLife(){
        float heal = DataFunc.Range(Data.data.healRangeValue);
        //obtienes el porcentaje de curacion basado en tu max
        float healPercent = DataFunc.KnowQtyOfPercent(heal, energyMax);

        energyActual = Mathf.Clamp(energyActual + healPercent, 0, energyMax);
    }
    /// <summary>
    /// Si coges una moneda en mapa, aleatoriamente generará
    /// entre ciertos rangos una cantidad de valor y se añadirá a
    /// el dinero recogido de esta partida
    /// </summary>
    public void CollectCoin(){
        int count = DataFunc.Range(Data.data.coinRangeValue);
        collectedMoney += count;
    }
    /// <summary>
    /// Dependiendo del mosntruo que hayamos eliminado,
    /// añadiremos en el collectedMoney el valor correspondiente
    /// </summary>
    /// <param name="type"></param>
    public void CollectMonsterValue(MonsterType type){
        int count = 0;

        switch (type)
        {
            case MonsterType.Monster_Floor:
                count = Data.data.monsterValue_floor;
                break;
            case MonsterType.Monster_Aero:
                count = Data.data.monsterValue_aero;
                break;
        }

        collectedMoney += count;
    }


    /// <summary>
    /// Revisa el estado del jugador, en caso de no tener energía
    /// se llama al gameOver
    /// </summary>
    private void CheckPlayerStatus()
    {
        if (energyActual <= 0 || obj_player.transform.position.y < -2 && GameManager.status == GameStatus.InGame){

            //playerController.DisablePhysics();
            //Mostramos la animación de muerte del jugador
            player.anim_player.SetBool("IsDead", true);
            MusicSystem.ReproduceSound(MusicSystem.SfxType.Dead);
            GameManager.GameOver();
        }
    }


    /// <summary>
    /// Obtenemos el estado actual de la stat correspondiente al indice especificado
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static float GetActualStat(int index)
    {
        float stat = 0;
        switch (index)
        {
            case (int)BuffType.Energy:
                stat = player.energyMax;
                break;
            case (int)BuffType.Speed:
                stat = player.speedActual;
                break;
            case (int)BuffType.Shield:
                stat = player.shieldsActual;
                break;
        }

        return stat;
    }
    #endregion
}
