using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Conocemos el estado del juego
public enum GameStatus
{
    Init,
    Paused,
    InGame,
    GameOver
}

[System.Serializable]
public class GameManager : MonoBehaviour
{
    #region Var

    private Camera cam;
    private float camHeight;
    private float camWidth;

    private static GameManager _;

    [Header("GameManager info")]
    public GameSetup gameSetup;

    // vemos el estado del juego
    public static GameStatus status = GameStatus.Init;

    //Permite pausar los objetos del mapa
    public static bool isDebug = false;

    [Header("Screen Setting")]
    public GameObject screenGame;
    public GameObject screenPause;
    public GameObject screenEnd;



    [Header("DEBUG")]
    public GameStatus debug_status; 
    public bool visual_isDebug = false;

    #endregion
    #region Events
    private void Awake()
    {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
        SetCamera();
    }
    void Start()
    {
        status = GameStatus.Init;
        UpdateStatus();

        // DEBUG
        isDebug = visual_isDebug;
        if (visual_isDebug) Debug.LogError("WARN ! --> DEBUG_MODE ON");
        gameSetup.Debug_Visuals();
        // Debug

    }
    void Update()
    {
        if (status == GameStatus.Init)
        {
            Debug.Log("Run Game ? ");
            status = GameStatus.InGame;
        }
        else
        {
            UpdateStatus();

        }
       
           
    }
    #endregion
    #region Methods

    /// <summary>
    /// Actualizamos los efectos dependientes de los estados referentes al juego 
    /// </summary>
    private void UpdateStatus()
    {
        Time.timeScale = status != GameStatus.InGame ? 0 : 1;
    }

    /// <summary>
    /// Hace los cambios de estado entre juego y pausa, y viceversa
    /// </summary>
    /// <param name="condition"></param>
    public static void OnOffPause(bool condition) {

        status = condition ? GameStatus.Paused : GameStatus.InGame;
        CheckScreens();
    }

    /// <summary>
    /// Revisa el estado de las pantallas y si alguna debe activarse o desactivarse
    /// </summary>
    public static void CheckScreens()
    {
        DataFunc.ObjOnOff(_.screenPause, status == GameStatus.Paused);
        DataFunc.ObjOnOff(_.screenGame, status == GameStatus.InGame);
        DataFunc.ObjOnOff(_.screenEnd, status == GameStatus.GameOver);
    }

    /// <summary>
    /// Cambia a la escena establecida
    /// </summary>
    /// <param name="_sceneName"></param>
    public static void NavigateTo(string _sceneName) => DataFunc.ChangeSceneTo(_sceneName);

    /// <returns>Pasa el alto de la camara en unidades Unity</returns>
    public static float GetCameraHeight() => _.camHeight;

    /// <returns>Pasa el ancho de la camara en unidades Unity</returns>
    public static float GetCameraWidth() => _.camWidth;

    /// <returns>la camara asignada en juego</returns>
    public static Camera GetCamera() => _.cam;


    /// <summary>
    /// Asigna la camara y conocemos los parametros
    /// de ancho y alto 
    /// </summary>
    /// <param name="camera"></param>
    private void SetCamera(Camera camera = null)
    {
        cam = camera ? camera : Camera.main;
        camHeight = DataFunc.GetScreenHeightUnit(cam); // ~10~ aprox
        camWidth = DataFunc.GetScreenWidthUnit(camHeight);
    }

    public static void GameOver()
    {
        
        Debug.Log("GG");
        status = GameStatus.GameOver;
        CheckScreens();

    }

    #endregion
}