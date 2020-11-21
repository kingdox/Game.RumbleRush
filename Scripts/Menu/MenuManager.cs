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
        
        text_recordMeters.text = TransData._.Trns(TKey.Metters) + DataPass.Instance.savedData.recordMetersReached.ToString() + TransData._.Trns(TKey.SIGN_Metters);
        text_recordKills.text = TransData._.Trns(TKey.Monsters) + DataPass.Instance.savedData.recordMonstersKilled.ToString();
        text_ActualMoney.text = TransData._.Trns(TKey.Money) + DataPass.Instance.savedData.actualmoney.ToString()+ TransData._.Trns(TKey.SIGN_Money);

    }
    private void Update()
    {
        
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
    public void ChangeSceneTo(int i) => Data.data.ChangeSceneTo(i);
    #endregion
}
#endregion
