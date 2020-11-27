using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Aquí manejaremos los datos del jugador y estos mismos serán
    //los que pueden afectarse por efectos externos, etc...
    //Aquí se contará la vida entre otros, si se muere el jugador se llama
    //a GameManager desde aquí para llamar gameOver



    public static Player _;


    private void Awake()
    {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
    }





    private void CallGameOver()
    {
        GameManager.GameOver();
    }
}
