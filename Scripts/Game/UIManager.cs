#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class UIManager : MonoBehaviour
{
    #region Variables


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
    void Update()
    {
        ScreenUpdate();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Actualiza la pantalla correspondiente
    /// </summary>
    private void ScreenUpdate()
    {
        switch (GameManager.status)
        {
            case GameStatus.Paused:
                LoadPause();
                break;
            case GameStatus.InGame:
                InGameUpdate();
                break;
            case GameStatus.GameOver:
                EndGameUpdate();
                break;
        }
    }


    /// <summary>
    /// Actualiza la pantalla "Pause"
    /// </summary>
    private void LoadPause()
    {

    }
    /// <summary>
    /// Actualiza los valores del juego
    /// </summary>
    private void InGameUpdate()
    {
        text_scoreMetters.text = PlayerManager.player.mettersActual.ToString();
        text_scoreKills.text = PlayerManager.player.killsActual.ToString();

        //Imagen derecha del salto
        img_sideAction_R.transform.rotation = Quaternion.Euler(0,0, playerController.canJump ? 0 : 180);


        //Barra de energía


        float lifeUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.energyActual, PlayerManager.player.energyMax) / 100;
        float cdUnit = DataFunc.KnowPercentOfMax(PlayerManager.player.cooldownActual, PlayerManager.player.cooldownMax) / 100;
        rect_energyBar.anchorMax = new Vector2(lifeUnit, 1);
        rect_cooldownBar.anchorMax = new Vector2(cdUnit, 1);

        img_energyBar.color =  DataFunc.SetColorParam(img_energyBar.color, (int)ColorType.a, lifeUnit);
        img_cooldownBar.color = DataFunc.SetColorParam(img_cooldownBar.color, (int)ColorType.a, cdUnit);


    }

    /// <summary>
    /// Actualiza la pantalla "End" ?
    /// </summary>
    private void EndGameUpdate()
    {

    }
    #endregion
}