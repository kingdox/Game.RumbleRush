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
    public Light lightScene;
    [Space]

    //public Animator anim_camera;


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
        Time.timeScale = 1;

        status = GameStatus.Init;
        UpdateStatus();

        // DEBUG
        isDebug = visual_isDebug;
        if (visual_isDebug) Debug.LogError("WARN ! --> DEBUG_MODE ON");
        gameSetup.Debug_Visuals();
        // Debug


        //Revisamos la musica
        MusicSystem.SetVolume(0.25f);
        MusicSystem.CheckMusic();
        
        StartCoroutine(SetFarCamera());

    }
    void Update()
    {
        if (status == GameStatus.Init)
        {
            Debug.Log("Run Game ? ");
            status = GameStatus.InGame;
        }
        else if (status != GameStatus.GameOver)
        {
           UpdateStatus();
        }
       
    }
    #endregion
    #region Methods


    /// <summary>
    /// Esta se llamará así mismo hasta que la escena haya terminado de mostrarse
    /// 0, 35, 50
    /// Luces, camara, accion !
    /// donde isStart determina si vamos hacia adelante o hacia atrás
    /// donde moment dice el estado actual de la camara
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    private IEnumerator SetFarCamera( bool isStart = true)
    {
        float[] fars = { 10 , 35, 50 };
        //si esta iniciando lo vamos elevando hasta el tope de longitud, si esta terminando lo llevamos hasta -1
        int index = 1;

        for (int x = 0; x < fars.Length; x++)
        {
            //Conseguimos el indice actual
            if (fars[x] == cam.farClipPlane) index = x;
        }

        //Pasados los 6 segundos
        yield return new WaitForSeconds(0.6f);

        index += isStart ? 1 : -1;

        if (DataFunc.IsOnBoundsArr(index,fars.Length))
        {
            if (isStart)
            {
                float percent = DataFunc.KnowPercentOfMax(index, fars.Length) / 100;
                Debug.Log($"percent {percent}");
                MusicSystem.SetVolume(percent);
            }

            lightScene.intensity = DataFunc.KnowPercentOfMax(index+1,fars.Length) / 100;
            cam.farClipPlane = fars[index];
            StartCoroutine(SetFarCamera(isStart));
        }
        else
        {
            if (isStart)
            {
                MusicSystem.SetVolume(1);
            }
        }   

    }




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

        if (status != GameStatus.GameOver){

            MusicSystem.SetVolume(condition ? 0.25f : 1);

            status = condition ? GameStatus.Paused : GameStatus.InGame;
            CheckScreens();
        }
    }

    /// <summary>
    /// Revisa el estado de las pantallas y si alguna debe activarse o desactivarse
    /// </summary>
    public static void CheckScreens()
    {
        DataFunc.ObjOnOff(_.screenPause, status == GameStatus.Paused);
        DataFunc.ObjOnOff(_.screenGame, status == GameStatus.InGame);
        DataFunc.ObjOnOff(_.screenEnd, status == GameStatus.GameOver);

        UIManager.LoadScreen();
    }

    /// <summary>
    /// Hace el sonido de un boton
    /// </summary>
    public void ButtonPressed() => MusicSystem.ButtonSound();

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
    /// Corremos al animación del screenshake
    /// </summary>
    //public static void ScreenShake()
    //{
    //    _.anim_camera.Play("Camera");

    //}
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
        
        Debug.Log("GG, Game Over");
        status = GameStatus.GameOver;
        UIManager.InGameUpdate();

        //Tomamos los datos guardados para poder editarlos
        SavedData newSavedData = DataPass.GetSavedData();

        //Añadimos el dinero obtenido
        newSavedData.actualmoney += PlayerManager.player.collectedMoney;

        newSavedData.lastMetersReached = PlayerManager.player.mettersActual;
        newSavedData.lastMonstersKilled = PlayerManager.player.killsActual;

        newSavedData.recordMetersReached = newSavedData.recordMetersReached > PlayerManager.player.mettersActual ? newSavedData.recordMetersReached : PlayerManager.player.mettersActual;
        newSavedData.recordMonstersKilled = newSavedData.recordMonstersKilled > PlayerManager.player.killsActual ? newSavedData.recordMonstersKilled : PlayerManager.player.killsActual;

        //Insertamos los datos
        DataPass.SetData(newSavedData);
        //Guardamos los datos
        DataPass.SaveLoadFile(true);


        //Actualiza con los nuevos datos


        _.StartCoroutine(_.GameEnd());

    }


    /// <summary>
    /// Pasados los sgundos establecidos se muestra la pantalla de game End...
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator GameEnd()
    {
        //Retrocedemos la camara para ocultar la escena
        //el tiempo que espero , aprox 5 sec
        MusicSystem.SetVolume(0.75f);
        yield return new WaitForSeconds(5);
        StartCoroutine(SetFarCamera(false));

        MusicSystem.SetVolume(0.25f);


        yield return new WaitForSeconds(0.6f * 3);
        Time.timeScale = 0;
        CheckScreens();

        //Reproducimos la musica final
        MusicSystem.PlayThisMusic(MusicSystem.MusicPath.RR_End);
        MusicSystem.SetVolume(1);

    }


    #endregion
}