#region ####################### IMPLEMENTATION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion
#region ####################### CLASS
public class MenuManager : MonoBehaviour
{
    #region ####################### Variables

    public Text text_recordMeters;
    public Text text_recordKills;
    public Text text_ActualMoney;

    #endregion
    #region ####################### EVENT
    private void Start()
    {
        
        text_recordMeters.text = Translator.Trns(TKey.Metters) + DataPass.GetSavedData().recordMetersReached.ToString() + Translator.Trns(TKey.SIGN_Metters);
        text_recordKills.text = Translator.Trns(TKey.Monsters) + DataPass.GetSavedData().recordMonstersKilled.ToString();
        text_ActualMoney.text = Translator.Trns(TKey.Money) + DataPass.GetSavedData().actualmoney.ToString()+ Translator.Trns(TKey.SIGN_Money);

    }
    #endregion
    #region ####################### METHOD



    /// <summary>
    /// Dependiendo del estado actual del sonido cambiará a sonar o no
    /// </summary>
    public void OnOffMusic()
    {

    }

    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int i) => DataFunc.ChangeSceneTo(i);
    #endregion
}
#endregion
