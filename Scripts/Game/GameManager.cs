using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    //Permite pausar los objetos del mapa
    public static bool paused = false;
    public static bool isDebug = false;


    [Header("DEBUG")]
    public bool visual_paused = false;
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
        isDebug = visual_isDebug;
        if (visual_isDebug) Debug.LogError("WARN ! --> DEBUG_MODE ON");
        gameSetup.UpdateVisuals();
    }

    void Update()
    {
        isDebug = visual_isDebug;
        if (isDebug)
        {
            paused = visual_paused;
        }
    }
    #endregion
    #region Methods
    public static void OnOffPause(bool condition)
    {
        //TODO
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


    //--- Privadas



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

    #endregion
}