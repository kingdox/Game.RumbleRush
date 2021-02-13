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
    private bool loaded = false;

    public Text text_recordMeters;
    public Text text_recordKills;
    public Text text_ActualMoney;
    [Space]
    public Image img_music;
    [Space]
    public Text version;

    #endregion
    #region ####################### EVENT
    private void Start()
    {
        version.text = Data.data.version + " ";

        MusicSystem.SetVolume(1);
        MusicSystem.CheckMusic();
        img_music.color = DataFunc.SetColorParam(img_music.color, (int)ColorType.a, MusicSystem.CanSound() ? 1 : 0.5f);

    }
    private void Update()
    {
        LoadInformation();
    }
    #endregion
    #region ####################### METHOD

    /// <summary>
    /// Carga la información que posea el datapass de los datos guardados,
    /// corrobora que estan cargados y que no se ha cargado antes
    /// </summary>
    private void LoadInformation()
    {
        if (DataPass._.isReady && !loaded)
        {
            loaded = true;
            text_recordMeters.text = DataPass.GetSavedData().recordMetersReached.ToString() + Translator.Trns(TKey.SIGN_Metters);
            text_recordKills.text =  DataPass.GetSavedData().recordMonstersKilled.ToString();
            text_ActualMoney.text =  DataPass.GetSavedData().actualmoney.ToString();
        }
    }


    /// <summary>
    /// Dependiendo del estado actual del sonido cambiará a sonar o no
    /// </summary>
    public void OnOffMusic()
    {
        ButtonPressed();
        bool condition = MusicSystem.CanSound();
        MusicSystem.IsSound( !condition );
        img_music.color = DataFunc.SetColorParam(img_music.color,(int)ColorType.a, !condition ? 1: 0.5f);
    }


    /// <summary>
    /// Hace el sonido de un boton
    /// </summary>
    public void ButtonPressed() => MusicSystem.ButtonSound();



    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int i) => DataFunc.ChangeSceneTo(i);


    /// <summary>
    /// Te saca del juego
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
#endregion
