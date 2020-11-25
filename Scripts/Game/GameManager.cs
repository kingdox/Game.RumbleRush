using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class GameManager : MonoBehaviour
{
    public GameSetup gameSetup;

    //Permite pausar los objetos del mapa

    public static bool paused = false;
    public static bool isDebug = false;

    [Header("DEBUG")]
    public bool visual_paused = false;
    public bool visual_isDebug = false;



    private void Awake()
    {
        if (visual_isDebug) Debug.LogError("WARN ! --> DEBUG_MODE ON");
        isDebug = visual_isDebug;
    }
    void Start()
    {
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



    public static void OnOffPause(bool condition)
    {
        //TODO
    }


    public static void NavigateTo(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
