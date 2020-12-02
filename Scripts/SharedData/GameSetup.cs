#region ###### Imp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class GameSetup: MonoBehaviour
{
    #region Variables

    #region VISUAL-Variables que es DEBUG,
    [Header("DEBUG")]
    [Space]
    //_Este apartado es visual, pero con debug vez las cosas
    [SerializeField]
    private Character visual_character;
    [Space]
    [SerializeField]
    private Buff[] visual_buffs;
    public float visual_easyMetters;
    #endregion


    // -> Personaje escogido, aquí manejaremos los datos
    public static Character character;
    // -> Los Buff con la cantidad comprada
    public static Buff[] buffs;
    public static float easyMetters;

    //con el que sabremos si es modo dificil o facil
    public static bool hardMode = false;
    
    #endregion
    #region EVENT
    private void Update()
    {
        
    }
    #endregion
    #region Methods
    /// <summary>
    /// Esto permite actualizar los datos reales que
    /// tenemos para verlos con el objeto asignado en el inspector
    /// </summary>
    public void Debug_Visuals()
    {
        if (GameManager.isDebug)
        {
            visual_character.SetType();
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
            + DataPass.GetSavedData().lastMonstersKilled * Data.data.monsterEasyMettersValue
            ) / Random.Range(1, 2.5f);

        Debug.Log($"Poseerás {easyMetters} Metros faciles :)");
    }
    #endregion

}