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
    [Space]
    public Button btn_Buy;
    public Image img_Buy;
    public Text text_Buy;

    [Header("Visual Settings")]
    public PreparationVisual preparationVisual = new PreparationVisual();
    public Sprite[] spr_character = new Sprite[4];


    [Header("Preparation info")]
    public Character characterSelected;
    public BuffItem[] buffItems = new BuffItem[3];
    //La cantidad de dinero que poseemos tras los gastos, con ello podemos saber si tenemos o no sin afectar la data principal
    public int budget;

    #endregion
    #region ###### EVENT
    private void Start()
    {
        text_actualMoney.text = DataPass.GetSavedData().actualmoney.ToString();
        characterSelected.SetType();
        RefreshScene();

        MusicSystem.SetVolume(1);
        MusicSystem.CheckMusic();
    }

    private void Update()
    {
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
        if (needChanges) {
            ButtonPressed();
            RefreshScene();
        };
    }

    /// <summary>
    /// Se cambiará el personaje mostrado en pantalla
    /// irá hacia el siguiente personaje ó al anterior
    /// </summary>
    /// <param name="_goforward"></param>
    public void ChangeCharacter(bool _goforward)
    {
        int _newIndex = DataFunc.TravelArr(_goforward, CharacterData.cD.characters.Length, (int)characterSelected.type);
        characterSelected.SetType(_newIndex);
        RefreshScene();
    }


    /// <summary>
    /// Actualizamos Los datos de la escena basado en la nueva información
    /// </summary>
    private void RefreshScene()
    {

        //tenemos el dinero que posee el jugador y con esta la reduciremos para saber el resultado
        budget = DataPass.GetSavedData().actualmoney;
        budget -= characterSelected.cost;

        foreach (BuffItem item in buffItems) budget -= item.totalCost;

        text_costMoney.text = (DataPass.GetSavedData().actualmoney - budget).ToString();
        preparationVisual.SetText(characterSelected);

        preparationVisual.img_character.sprite = spr_character[(int)characterSelected.type];

        //Aqui si esto es mayor significa que pueda gastar el dinero
        bool canBuy = budget >= 0;

        btn_Buy.enabled = canBuy;

        img_Buy.color = canBuy ? Color.white : DataFunc.SetColorParam(Color.white / 2, (int)ColorType.a);

        text_Buy.color = canBuy ? Color.black : Color.red;
        
    }




    /// <summary>
    /// Coloca la información en GameSetup
    /// y actualiza DataPass para que posea los gastos nuevos.
    /// </summary>
    public void StartGame()
    {
        MusicSystem.ReproduceSound(MusicSystem.SfxType.Coin);

        // -> Conservamos la info por que la vamos a alterar
        SavedData _saved = DataPass.GetSavedData();

        _saved.actualmoney = budget;
        _saved.lastMoneySpent = DataPass.GetSavedData().actualmoney - budget;

        DataPass.SetData(_saved);
        DataPass.SaveLoadFile(true);


        // -> se coloca los datos en gameSetup
        GameSetup.character = characterSelected;

        Buff[] _buffs = GetBuffs(); //new Buff[buffItems.Length];

        GameSetup.buffs = _buffs;
        GameSetup.SetEasyMetters();

        ChangeSceneTo((int)Data.Scenes.GameScene);
    }


    /// <summary>
    /// Revisamos del arreglo items de buff si se ha añadido alguno, de ser así
    /// se agregan al Buff
    /// </summary>
    /// <returns>Retorna los buff comprados</returns>
    private Buff[] GetBuffs()
    {
        int count = 0;
        int[] indexBuff = new int[buffItems.Length];

        for (int x = 0; x < buffItems.Length ; x++) if (buffItems[x].buff.counts > 0) indexBuff[count++] = x;

       
        Buff[] _buffs = new Buff[count];
        for (int j = 0; j < count; j++) _buffs[j] = buffItems[indexBuff[j]].buff;

        // Losiento profe le fallé :( no se me ocurrió
        // algo mejor que hacer que hay prisas tic toc tic toc
        return _buffs;
    }

    /// <summary>
    /// Hace el sonido de un boton
    /// </summary>
    public void ButtonPressed() => MusicSystem.ButtonSound();

    /// <summary>
    ///  Cambiamos a la escena indicada
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int _i) => DataFunc.ChangeSceneTo(_i);
    #endregion
}

#region PreparationVisual
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
        text_character.text = Translator.ClampKey(_c.keyName, CharacterData.cD.charKeys); 
        text_energy.text = _c.energy.ToString();
        text_speed.text = _c.speed.ToString();
        text_jump.text = _c.jump.ToString();
        text_power.text = Translator.ClampKey(_c.keyPower, CharacterData.cD.powKeys); 
    }
}
#endregion