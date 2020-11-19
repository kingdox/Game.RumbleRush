#region ###### Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#endregion

public class PreparationManager : MonoBehaviour
{
    #region ###### VAR
    //Usar un dinero para manejar los precios
    [Header("Preparation Settings")]
    public Text text_actualMoney;
    public Text text_costMoney;

    [Header("Visual Settings")]
    public PreparationVisual preparationVisual = new PreparationVisual();


    [Header("Preparation info")]
    public Character characterSelected;
    //La cantidad de dinero que poseemos tras los gastos
    public int budget;

    #endregion
    #region ###### EVENT
    private void Start()
    {
        text_actualMoney.text = ES.es.Trns(TKey.Money) + DataPass.Instance.savedData.actualmoney.ToString() + ES.es.Trns(TKey.SIGN_Money);
        characterSelected.SetType();
        //Poner los textos
        RefreshScene();
    }
    #endregion
    #region ###### METHOD


    /// <summary>
    /// Se cambiará el personaje mostrado en pantalla
    /// irá hacia el siguiente personaje ó al anterior
    /// </summary>
    /// <param name="_goforward"></param>
    public void ChangeCharacter(bool _goforward)
    {
        int _newIndex = DataFunc._.TravelArr(_goforward, (CharacterData.cD.characters.Length - 1), (int)characterSelected.type);
        characterSelected.SetType(_newIndex);
        RefreshScene();
    }


    // TODO falta completar
    /// <summary>
    /// Actualizamos Los datos de la escena basado en la nueva información
    /// </summary>
    private void RefreshScene()
    {
        //tenemos el dinero que posee el jugador y con esta la reduciremos para saber el resultado
        budget = DataPass.Instance.savedData.actualmoney;
        budget -= characterSelected.cost;



        text_costMoney.text = budget.ToString() + ES.es.Trns(TKey.SIGN_Money);
        preparationVisual.SetText(characterSelected);
    }


    /// <summary>
    ///  Cambiamos a la escena indicada
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int _i) => Data.data.ChangeSceneTo(_i);
    #endregion
}


/// <summary>
/// Se contendrá las referencias de textos para mostrar
/// </summary>
[System.Serializable]
public struct PreparationVisual
{
    public Image img_character;

    public Text text_character;
    public Text text_energy;
    public Text text_speed;
    public Text text_jump;
    public Text text_power;


    public void SetText(Character _c)
    {
        //int i = (int)_c.type;
        text_character.text = ES.es.ClampKey(_c.keyName, CharacterData.cD.charKeys);  //ES.es.Trns(CharacterData.cD.charKeys[i]);
        text_energy.text = _c.energy.ToString();
        text_speed.text = _c.speed.ToString();
        text_jump.text = _c.jump.ToString();
        text_power.text = ES.es.ClampKey(_c.keyPower, CharacterData.cD.powKeys); 
    }
}