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

    [Header("Settings")]
    public PlayerController playerController;

    [Header("Game Screen")]

    //Top
    // - Buff
    public GameObject[] obj_VisualBuffs;
    [Space]

    // - Scores
    public Text text_scoreKills;
    public Text text_scoreMetters;
    [Space]

    //Sides
    // - Left
    public Image img_sideBG_L;
    public Image img_sideAction_L;
    [Space]
    // - Right
    public Image img_sideBG_R;
    public Image img_sideAction_R;
    [Space]


    // Bottom
    public Image img_energyBar;
    public RectTransform rect_energyBar;
    [Space]
    public Image img_cooldownBar;
    public RectTransform rect_cooldownBar;
    [Space]

    public Image img_profileBG;
    public Image img_profileIcon;

    [Header("Pause Screen")]
    public Image img_pauseBG;
    public Text text_pauseScoreMetters;
    public Text text_pauseScoreKills;

    [Header("End Screen")]
    public Image img_endBG;
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
    private void Update()
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
        
        //TODO
        for (int i = 0; i < obj_VisualBuffs.Length; i++)
        {
            //if (i < GameSetup.buffs.Length && GameSetup.buffs[i].counts > 0 )    
            //{
            //    //le pone los datos correspondientes
            //}
            //else
            //{
            //    Image _img = obj_VisualBuffs[i].GetComponent<Image>();
            //    _img.color = DataFunc.SetColorParam(_img.color, (int)ColorType.a, 0.5f);
                
                    
            //    //Los pone con bajo alpha 
            //}

        }
        

    }

    /// <summary>
    /// Actualiza la pantalla correspondiente
    /// quiena ctiva a este señor? TODO
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
    private void InGameUpdate(){
        text_scoreMetters.text = PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters); ;
        text_scoreKills.text = PlayerManager.player.killsActual.ToString();

        //Imagen derecha del salto
        img_sideAction_R.transform.rotation = Quaternion.Euler(0,0, playerController.canJump ? 0 : 180);


        //Barra de energía
        float lifeUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.energyActual, PlayerManager.player.energyMax) / 100;
        float cdUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.cooldownActual, PlayerManager.player.cooldownMax) / 100;
        rect_energyBar.anchorMax = new Vector2(lifeUnit, 1);
        rect_cooldownBar.anchorMax = new Vector2(cdUnit, 1);

        img_energyBar.color =  DataFunc.SetColorParam(img_energyBar.color, (int)ColorType.a,lifeUnit);
        img_cooldownBar.color = DataFunc.SetColorParam(img_cooldownBar.color, (int)ColorType.a, cdUnit);
    }

    /// <summary>
    /// Actualiza la pantalla "Pause"
    /// </summary>
    private void LoadPauseScreen(){

        text_pauseScoreMetters.text = Translator.Trns(TKey.Metters) +  PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters); 
        text_pauseScoreKills.text = Translator.Trns(TKey.Monsters) + PlayerManager.player.killsActual.ToString();

    }
    /// <summary>
    /// Actualiza la pantalla "End" ?
    /// </summary>
    private void LoadEndScreen(){
        text_endMoney.text = Translator.Trns(TKey.Money) + DataPass.GetSavedData().actualmoney.ToString() + Translator.Trns(TKey.SIGN_Money);
        text_endScoreMetters.text = Translator.Trns(TKey.Metters) + PlayerManager.player.mettersActual.ToString() + Translator.Trns(TKey.SIGN_Metters);
        text_endScoreKills.text = Translator.Trns(TKey.Monsters) + PlayerManager.player.killsActual.ToString();
    }
    #endregion
}