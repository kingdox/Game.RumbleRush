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

    public Sprite[] spr_character = new Sprite[4];
    public Sprite[] spr_weapons = new Sprite[4];


    [Space]

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
    public Image img_sideAction_L;
    // - Right
    public Image img_sideAction_R;
    [Space]

    // Bottom
    public Image img_energyBar;
    public RectTransform rect_energyBar;
    [Space]
    public Image img_cooldownBar;
    public RectTransform rect_cooldownBar;
    [Space]

    public Image img_profileIcon;

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
        int _charIndex = (int)GameSetup.character.type;
        //Tomamos el sprite en el orden de los CharacterType
        spr_player.sprite = spr_character[_charIndex];
        spr_playerWeapon.sprite = spr_weapons[_charIndex];
        img_profileIcon.sprite = spr_character[_charIndex];

        img_sideAction_L.sprite = spr_weapons[_charIndex];


        //Recorres entre los buffs visuales
        for (int i = 0; i < obj_VisualBuffs.Length; i++)
        {
            int finded = -1;
            //Buscamos para ver si está presente este buff de los selectos
            for (int x = 0; x < GameSetup.buffs.Length; x++)
            {
                //Revisamos si de los buffs escogidos alguno posee este buff visual
                if ((int)GameSetup.buffs[x].type == i)
                {
                    finded = x;
                    break;
                }
            }

            //Si existe le asigna la cantidad correspondiente en su texto
            Text _child_txt = obj_VisualBuffs[i].transform.GetChild(0).GetComponent<Text>();

            if (finded != -1){
                _child_txt.text = "+"+GameSetup.buffs[finded].counts.ToString();
            }
            else
            {
                _child_txt.text = "";
                //Sino lo pone mas invisible y esconde el texto
                Image _img = obj_VisualBuffs[i].GetComponent<Image>();
                _img.color = DataFunc.SetColorParam(_img.color, (int)ColorType.a, 0.25f);

            }
        }

    }







    /// <summary>
    /// Actualiza la pantalla correspondiente
    /// quien activa a este señor?
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

        img_energyBar.color =  DataFunc.SetColorParam(img_energyBar.color, (int)ColorType.a, Mathf.Clamp(lifeUnit,0.25f,1));
        img_cooldownBar.color = DataFunc.SetColorParam(img_cooldownBar.color, (int)ColorType.a, cdUnit);


        img_profileIcon.transform.rotation = Quaternion.Euler(0, 0, lifeUnit > 0.5f ? 0 : 90);
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