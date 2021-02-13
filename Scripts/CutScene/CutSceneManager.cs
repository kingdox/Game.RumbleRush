using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{

    //Maneja tu splash y muestra la intro...
    private float fadeIn = 3.0f;
    private float splashCounter = 0.0f;
    private bool isSplashGone = false;


    private float delay = 5;
    private float delayCount;

    private float bgFaderCount;

    [Header("Settings Splash")]
    public Image splash;
    //Esta servirá para oscurecer
    public Image blackBG;

    private bool isMusicOn = false;
    public ParticleSystem part_snow;

    private void Start() {
        splashCounter = fadeIn;
        bgFaderCount = delay;
        DataFunc.ObjOnOff(blackBG.gameObject, true);
    }
    private void Update() {


        if (delayCount > delay) {

            if (bgFaderCount <= 0) {

                DataFunc.ObjOnOff(blackBG.gameObject, false);
            } else {
                if (!isSplashGone) {
                    SplashFade();
                    
                } else {
                    BGFader();

                    if (!isMusicOn)
                    {
                        isMusicOn = true;
                        part_snow.Play();
                        MusicSystem.SetVolume(0.75f);
                        MusicSystem.PlayThisMusic(MusicSystem.MusicPath.X5_RR);
                    }
                }
            }

        } else {
            delayCount += Time.deltaTime;
        }
    }


    private void SplashFade() {

        splashCounter = Mathf.Clamp(splashCounter - Time.deltaTime, 0, fadeIn);
        isSplashGone = splashCounter <= 0;

        float percent = DataFunc.KnowPercentOfMax(splashCounter, fadeIn) / 100;

        splash.color = DataFunc.SetColorParam(splash.color, (int)ColorType.a, percent);
    }

    private void BGFader() {
        bgFaderCount = Mathf.Clamp(bgFaderCount - Time.deltaTime, 0, delay);
        float percent = DataFunc.KnowPercentOfMax(bgFaderCount, delay) / 100;
        blackBG.color = DataFunc.SetColorParam(splash.color, (int)ColorType.a, percent);
    }


    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(string i) {

        MusicSystem.ReproduceSound(MusicSystem.SfxType.Coin);

        DataFunc.ChangeSceneTo(i);
    }
}
