#region ###### Imp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion


public class GameSetup: MonoBehaviour
{
    #region Variables

    #region VISUAL-Variables que es DEBUG
    //_Este apartado es visual, solo visual...
    [Header("Buff")]
    [SerializeField]
    private Character visual_character;
    [Header("Buff")]
    [SerializeField]
    private Buff[] visual_buffs;
    public float visual_easyMetters;
    #endregion
    // -> Personaje escogido
    public static Character character;
    // -> Los Buff con la cantidad comprada
    public static Buff[] buffs;
    public static float easyMetters;

    #endregion
    #region EVENT
    
    #endregion
    #region Methods
    /// <summary>
    /// Esto permite actualizar los datos reales que
    /// tenemos para verlos con el objeto asignado en el inspector
    /// </summary>
    public void UpdateVisuals()
    {
        if (GameManager.isDebug)
        {
            character = visual_character;
            buffs = visual_buffs;
            easyMetters = visual_easyMetters;
        }
        else
        {
            visual_character = character;
            visual_buffs = buffs;
            visual_easyMetters = easyMetters;
        }
    }
    /// <summary>
    /// Basado en los datos obtenidos y, los guardados formulará
    /// una cantidad de metros que el jugador tendrá "Facil"
    /// </summary>
    public static void SetEasyMetters(){

        //Nesecita en el calculo Recorrido anterior, Dinero invertido Monstruos muertos
        easyMetters = (
            DataPass.GetSavedData().lastMetersReached
            + DataPass.GetSavedData().lastMoneySpent
            + DataPass.GetSavedData().lastMonstersKilled * Data.data.monsterValue
            ) / Random.Range(1, 2.5f);

        Debug.Log($"Poseerás {easyMetters} Metros faciles :)");
    }
    #endregion

}