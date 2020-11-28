using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

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
    public Image img_cooldownBar;
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


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
