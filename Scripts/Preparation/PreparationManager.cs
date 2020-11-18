using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PreparationManager : MonoBehaviour
{
    #region ###### VAR
    //Usar un dinero para manejar los precios

    public Text actualMoney;

    #endregion
    #region ###### EVENT
    private void Start()
    {
        actualMoney.text = ES.es.Trns(TKey.Money) + DataPass.Instance.savedData.actualmoney.ToString() + ES.es.Trns(TKey.SIGN_Money);
        //Poner los textos
    }
    #endregion
    #region ###### METHOD




    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int i) => Data.data.ChangeSceneTo(i);
    #endregion
}