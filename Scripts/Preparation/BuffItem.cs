using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Donde poseemos el buff y la cantidad escogidas, actualiza internamente sus cosas
/// 
/// </summary>
public class BuffItem : MonoBehaviour
{
    #region ###### VARIABLES

    [Header("Buff info")]
    public Buff buff;

    [Header("BuffItem info")]
    [Range(0, 99)]
    private readonly int maxCount = 99;
    public int count = 0;
    public int totalCost = 0;
    [Space]
    public Text text_count;
    public Text text_cost;
    public Text text_totalCost;
    public Image img_buff;
    [Space]
    public GameObject btn_minus;
    public GameObject btn_plus;
    public GameObject info_count;
    public GameObject info_costTotal;



    #endregion

    #region ###### EVENTS
    void Start()
    {
        count = 0;
        SetBuff();
        SetCostText(text_cost, buff.cost);

        UpdateCount();
    }
    #endregion

    #region #### Methods


    /// <summary>
    /// Actualiza la cantidad que se ha escogido del buff
    /// desaparece el boton "-" en caso de ser 0, el + desaparece si es 99
    /// También actualiza los elementos activados o desactivados dependiendo del count
    /// </summary>
    /// <param name="_c"></param>
    public void UpdateCount(int _c = 0) {

        count = Mathf.Clamp(count + _c, 0, maxCount);
        totalCost = buff.cost * count;

        bool haveCount = count != 0;
        SetOnOff(info_count, haveCount);
        SetOnOff(info_costTotal, haveCount);
        SetOnOff(btn_minus, count > 0);
        SetOnOff(btn_plus, count < maxCount);

        if (haveCount)
        {
            SetCostText(text_totalCost, totalCost);
            SetCountText();
        }

    }




    /// <summary>
    /// Actualiza la cantidad de buff escogidas
    /// </summary>
    private void SetCountText() =>  text_count.text = "x"+count.ToString();

    /// <summary>
    /// Actualiza un texto de tipo monetario
    /// </summary>
    private void SetCostText(Text txt , int val) => txt.text = val.ToString() + GetCurrency();

    /// <summary>
    /// Establecemos el buff con sus valores
    /// </summary>
    private void SetBuff(int i = -1) => buff.SetBuff(i);

    /// <summary>
    /// Usa la función de data para activar o desactivarlo
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="condition"></param>
    private void SetOnOff(GameObject obj, bool condition) => DataFunc._.ObjOnOff(obj, condition);

    /// <returns>El signo de la moneda</returns>
    private string GetCurrency() => ES.es.Trns(TKey.SIGN_Money);
    #endregion
}
