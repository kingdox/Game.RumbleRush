using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSetup gameSetup;

    //Permite pausar los objetos del mapa
    public bool pause = false;

    /*
     Es el que hace el manejo del juego
     
     */


    void Start()
    {
        gameSetup.UpdateVisuals();

    }

    void Update()
    {
        
    }
}
