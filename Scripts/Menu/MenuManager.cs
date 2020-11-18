#region ####################### IMPLEMENTATION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        text_recordMeters.text = "Metros: " + DataPass.Instance.savedData.recordMetersReached.ToString() + "M";
        text_recordKills.text = "Monstruos: " + DataPass.Instance.savedData.recordMonstersKilled.ToString();
        text_ActualMoney.text = "Monedas: " + DataPass.Instance.savedData.actualmoney.ToString()+"$";

    }
    private void Update()
    {
        
    }
    #endregion
    #region ####################### METHOD

    /// <summary>
    /// Basado en el tipo de escena te cambiará
    /// </summary>
    public void ChangeSceneTo(SceneIndex sceneIndex)
    {
        Debug.Log($"Cargando Escena : {sceneIndex}");
        SceneManager.LoadScene((int)sceneIndex);
    }


    #endregion
}
#endregion
