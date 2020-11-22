#region ###### Imp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public static class GameSetup
{
    #region Variables

    [Header("Player info")]
    // -> Personaje escogido
    public static Character character;
    // -> Los Buff con la cantidad comprada
    public static BuffItem[] buffs;

    [Header("Setup")]
    public static float easyMetters;

    #endregion


    #region Methods



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

/*

    GameSetup guarda los datos de la ultima partida, esta info estará presente

 
 
 */
