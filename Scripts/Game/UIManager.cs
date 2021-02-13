#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion


public class UIManager : MonoBehaviour
{
    #region Variables
    private static UIManager _;

    [Header("Settings and Player data")]
    public PlayerController playerController;
    public SpriteRenderer spr_player;
    // Arma del jugador
    public SpriteRenderer spr_playerWeapon;

    public Sprite[] spr_profile = new Sprite[4];
    public Sprite[] spr_character = new Sprite[4];
    public Sprite[] spr_weapons = new Sprite[4];


    [Space]

    [Header("Game Screen")]

    //Top
    // - Stats
    public Image[] img_stats;
    public Text[] text_stats;
    [Space]

    // - Scores
    public Text text_scoreKills;
    public Text text_scoreMetters;
    [Space]

    //Sides
    // - Left
    public Image img_sideAction_L;
    public Animator anim_side_L;

    // - Right
    public Image img_sideAction_R;
    public Animator anim_side_R;

    [Space]

    // Bottom
    public Image img_energyBar;
    public Image img_energyContainer;
    public Sprite spr_energyContainer;
    public Sprite spr_energyContainer_low;

    public Animator anim_energyBar;

    public RectTransform rect_energyBar;
    [Space]
    public Image img_cooldownBar;
    public Image img_cooldownContainer;
    public Sprite spr_cooldownContainer;
    public Sprite spr_cooldownContainer_low;

    public ParticleSystem part_particle_FirePower;

    public GameObject obj_particle_FirePower;

    public RectTransform rect_cooldownBar;
    [Space]

    public Image img_profileIcon;
    public Image img_weaponIcon;
    public Text text_weaponCD;

    public RectTransform rect_weaponBar;
    public ParticleSystem part_weapon_ready;


    [Header("Etc")]

    public Animator visualEffects;


    [Header("Pause Screen")]
    public Text text_pauseScoreMetters;
    public Text text_pauseScoreKills;

    [Header("End Screen")]
    public Text text_endScoreMetters;
    public Text text_endScoreKills;
    public Text text_endMoney;
    #endregion
    #region EVENTS
    private void Awake()
    {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
    }
    private void Start()
    {
        InitLoad();
    }
    private void FixedUpdate()
    {
        ScreenUpdate();

        
    }
    #endregion
    #region Methods

    /// <summary>
    /// Cargamos la imagen de:
    /// - player
    /// - Arma del player
    /// - Buffs y su cantidad (los que no se les baja el alfa)
    /// - El icono del player
    /// </summary>
    private void InitLoad()
    {
        int _charIndex = (int)GameSetup.character.type;
        //Tomamos el sprite en el orden de los CharacterType
        spr_player.sprite = spr_character[_charIndex];
        spr_playerWeapon.sprite = spr_weapons[_charIndex];
        img_profileIcon.sprite = spr_profile[_charIndex];

        img_weaponIcon.sprite = spr_weapons[_charIndex];

        part_particle_FirePower.Stop();

        //Actualizamos las stats actuales del player
        RefreshActualStats();
    }



    /// <summary>
    /// Actualizamos las estadisticas y el escudo con lo que tenemos actualmente
    /// -No hacer update de esto -
    /// </summary>
    private void RefreshActualStats( int index = -1){

        int _actualcount = 0;


        if (index == -1)
        {
            //Recorres entre las stats visuales del player y sus buffs...
            for (int i = 0; i < 3; i++)
            {
                _actualcount = (int)PlayerManager.GetActualStat(i);
                SetVisualStat(i, _actualcount);
            }
        }
        else
        {
            _actualcount = (int)PlayerManager.GetActualStat(index);
            SetVisualStat(index, _actualcount);
        }
    }

    /// <summary>
    /// Coloca los datos correspondientes a la stat que se muestra, dependiendo
    /// de si hay o no lo muestra o esconde, (convenciones para el caso del escudo...)
    /// </summary>
    /// <param name="index"></param>
    /// <param name="qty"></param>
    public static void SetVisualStat(int index, int qty )
    {
        if (qty != 0)
        {
            _.text_stats[index].text = "+" + qty.ToString();
        }
        //Si no hay se desactiva visualmente
        else
        {
            //lo pone mas invisible y esconde el texto
            _.text_stats[index].text = "";
        }

            _.img_stats[index].color = DataFunc.SetColorParam(_.img_stats[index].color, (int)ColorType.a, qty != 0 ? 1 : 0.25f);
    }


    /// <summary>
    /// Actualiza la pantalla correspondiente dependiendo de la escena
    /// </summary>
    private  void ScreenUpdate()
    {
        switch (GameManager.status)
        {
            case GameStatus.InGame:
                InGameUpdate();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Cargamos las pantallas que solo tienen que cargarse 1 vez,
    /// </summary>
    public static void LoadScreen(){
        switch (GameManager.status)
        {
            case GameStatus.Paused:
                _.LoadPauseScreen();
                break;
            case GameStatus.GameOver:
                _.LoadEndScreen();
                break;
            default:
                //Nada
                break;
        }
    }

    
    /// <summary>
    /// Actualiza los valores del juego
    /// </summary>
    public static void InGameUpdate(){
        _.text_scoreMetters.text = PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters);
        _.text_scoreKills.text = PlayerManager.player.killsActual.ToString();

        _.Refresh_Energy();
        _.Refresh_Spell();
        _.Refresh_Sides();
        _.Refresh_Weapon();
    }

    /// <summary>
    /// Actualizamos la barra de energía
    /// </summary>
    private void Refresh_Energy()
    {
        float lifeUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.energyActual, PlayerManager.player.energyMax) / 100;
        bool lowLife = lifeUnit < 0.5f;
        rect_energyBar.anchorMax = new Vector2(lifeUnit, 1);
        img_energyContainer.sprite = !lowLife ? spr_energyContainer : spr_energyContainer_low;
        img_energyBar.color = DataFunc.SetColorParam(img_energyBar.color, (int)ColorType.a, Mathf.Clamp(lifeUnit, 0.5f, 1));

        anim_energyBar.SetBool("lowLife", lowLife);
    }

    /// <summary>
    /// Actualizamos la barra de habilidad y vemos si esta activa o no
    /// </summary>
    private void Refresh_Spell()
    {
        float cdUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.cooldownActual, PlayerManager.player.cooldownMax) / 100;

        rect_cooldownBar.anchorMax = new Vector2(cdUnit, 1);

        img_cooldownContainer.sprite = PowerManager.IsPoweOn() ? spr_cooldownContainer : spr_cooldownContainer_low;
        img_cooldownBar.color = DataFunc.SetColorParam(img_cooldownBar.color, (int)ColorType.a, cdUnit);


        DataFunc.ParticlePlayStop(part_particle_FirePower, PowerManager.IsPoweOn());
    }


    /// <summary>
    ///  refrescamos los botones de los lados del inGame
    /// </summary>
    private void Refresh_Sides()
    {
        //Imagen derecha del salto
        float _sideRotation = playerController.canJump ? 0 : 180;
        img_sideAction_L.color = DataFunc.SetColorParam(img_sideAction_R.color, (int)ColorType.a, playerController.weaponCooldownCount >= CharacterData.cD.weaponCooldown[(int)GameSetup.character.type] ? 1 : 0.5f);
        img_sideAction_R.color = DataFunc.SetColorParam(img_sideAction_R.color, (int)ColorType.a, playerController.forceFall || playerController.falling && !playerController.canJump ? 0.5f : 1);
        img_sideAction_R.transform.rotation = Quaternion.Euler(0, _sideRotation, _sideRotation);
    }

    /// <summary>
    /// Refrescamos el icono del arma y su estado actual
    /// </summary>
    private void Refresh_Weapon()
    {
        float weaponCdUnit = DataFunc.KnowPercentOfMax(playerController.weaponCooldownCount, CharacterData.cD.weaponCooldown[(int)GameSetup.character.type]) / 100;

        Vector2 vecCD = new Vector2(weaponCdUnit / 2, weaponCdUnit / 2);
        Vector2 centerAnchor = Vector2.one / 2 ;

        rect_weaponBar.anchorMin = centerAnchor - vecCD;
        rect_weaponBar.anchorMax = centerAnchor + vecCD;

        img_weaponIcon.color = DataFunc.SetColorParam(img_weaponIcon.color, -1, weaponCdUnit == 1 ? 1 : 0);

        string info = weaponCdUnit != 1 ? System.Math.Round(CharacterData.cD.weaponCooldown[(int)GameSetup.character.type] - playerController.weaponCooldownCount,1).ToString() + "s" : "";

        text_weaponCD.text = info;
    }



    /// <summary>
    /// Actualiza la pantalla "Pause"
    /// </summary>
    private void LoadPauseScreen(){

        text_pauseScoreMetters.text = PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters); 
        text_pauseScoreKills.text = PlayerManager.player.killsActual.ToString();

    }
    /// <summary>
    /// Actualiza la pantalla "End" ?
    /// </summary>
    private void LoadEndScreen(){
        text_endMoney.text = DataPass.GetSavedData().actualmoney.ToString();
        text_endScoreMetters.text = PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters);
        text_endScoreKills.text = PlayerManager.player.killsActual.ToString();
    }




    /// <summary>
    /// Revisamos cual boton has presionado y, dependiendo del presionado animamos en el ui el boton
    /// de ataque o el de salto...
    /// </summary>
    /// <param name="isbuttonL"></param>
    public static void AnimateButton(bool isbuttonL, bool isDisabled = false)
    {


        // L == Weapon
        if (isbuttonL){
            _.anim_side_L.SetBool("disabled", isDisabled);
            _.anim_side_L.SetTrigger("pressed");
        }
        //R == Jump
        else{


            _.anim_side_R.SetBool("disabled", isDisabled);
            _.anim_side_R.SetTrigger("pressed");
        }
    }

    /// <summary>
    /// Enciende las particulas de weapon ready
    /// "$-Weapon Ready"
    /// </summary>
    public static void WeaponReady()
    {
        Debug.Log($"Ready Weapon !");
        _.part_weapon_ready.Play();
    }

    /// <summary>
    /// Muestra la animación de la pantalla respectiva
    /// </summary>
    public static void VisualEff(VisualEffType type){
        _.visualEffects.SetTrigger(type.ToString());
    }

    #endregion
}
public enum VisualEffType
{
    Damaged,
    Special,
    Heal
}