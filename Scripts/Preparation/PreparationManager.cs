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
    public BuffItem[] buffItems = new BuffItem[3];

    //La cantidad de dinero que poseemos tras los gastos, con ello podemos saber si tenemos o no sin afectar la data principal
    public int budget;

    #endregion
    #region ###### EVENT
    private void Start()
    {
        text_actualMoney.text = TransData._.Trns(TKey.Money) + DataPass.Instance.savedData.actualmoney.ToString() + TransData._.Trns(TKey.SIGN_Money);
        characterSelected.SetType();
        RefreshScene();
    }

    private void Update()
    {
        //TODO buscar luego una forma mejor que hacer una busqueda.....
        CheckBuffItems(); 
    }
    #endregion
    #region ###### METHOD


    /// <summary>
    /// Revisamos si alguno de los buff ha cambiado para actualizar el precio
    /// </summary>
    private void CheckBuffItems()
    {
        bool needChanges = false;

        //revisamo si alguno ha cambiado para actualizarlos
        foreach (BuffItem bI in buffItems)
        {
            if (bI.HasChanged) {
                needChanges = true;
                bI.HasChanged = false;
                // Debug.Log($"{ES.es.Trns(bI.buff.keyName)} | {bI.buff.keyName} ha cambiado => {bI.count}");
            }
        }
        //si nesecita cambios entonces:
        if (needChanges) RefreshScene();
    }

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


    /// <summary>
    /// Actualizamos Los datos de la escena basado en la nueva información
    /// </summary>
    private void RefreshScene()
    {
        //tenemos el dinero que posee el jugador y con esta la reduciremos para saber el resultado
        budget = DataPass.Instance.savedData.actualmoney;
        budget -= characterSelected.cost;

        foreach (BuffItem item in buffItems) budget -= item.totalCost;

        text_costMoney.text = budget.ToString() + TransData._.Trns(TKey.SIGN_Money);
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
    public Text text_cost;


    public Text text_character;
    public Text text_energy;
    public Text text_speed;
    public Text text_jump;
    public Text text_power;


    public void SetText(Character _c)
    {
        //int i = (int)_c.type;
        text_cost.text = _c.cost.ToString();
        text_character.text = TransData._.ClampKey(_c.keyName, CharacterData.cD.charKeys); 
        text_energy.text = _c.energy.ToString();
        text_speed.text = _c.speed.ToString();
        text_jump.text = _c.jump.ToString();
        text_power.text = TransData._.ClampKey(_c.keyPower, CharacterData.cD.powKeys); 
    }
}