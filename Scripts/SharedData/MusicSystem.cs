﻿#region Imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

public class MusicSystem : MonoBehaviour
{
    #region Var
    private static MusicSystem _;

    private bool isSoundOn = true;

    //-> los path de las canciones
    public enum MusicPath {
        no,
        X1_RR,
        X2_RR,
        RR_Preparation,
        RR_End,
        X5_RR
    };

    // -> new, evitemos cargar o haces unos picos re feo feo
    public AudioClip musicRef;


    //-> los path de los sfx, su orden determina los hijos del sfx
    public enum SfxType{
        Coin,
        Heal,
        Buff,
        Damage,
        Weapon,
        Jump,
        Fall, // Forced Fall
        Grounded,
        Dead,
        WeaponReady,
        FootSteps,
        ButtonOn,
        ButtonOff, //cuando esta desactivado o no funcione...

        //TODO - cuando puedas
        //Voces
        Monk,
        Paladin,
        Hunter,
        Brutus,

        //Voces monstruos
        Bat,
        Slime,
    }



    private string musicPath = "Sound/Music/";

    [Header("Settings")]
    public AudioSource audio_music;
    public AudioClip clip_music;

    [Space]
    public AudioSource audio_sfx;
    public AudioClip clip_sfx;

    [Header("Sfx")]
    //SfxItem
    public SfxItem[] sfxItems;


    #endregion
    #region Events
    private void Awake()
    {
        //Singleton corroboration
        if (_ == null)
        {
            DontDestroyOnLoad(gameObject);
            _ = this;
        }
        else if (_ != this)
        {
            Destroy(gameObject);
        }
    }
    
    #endregion
    #region Methods


    /// <summary>
    /// Activamos o desactivamos la musica general
    /// </summary>
    /// <param name="condition"></param>
    public static void IsSound(bool condition){
        Debug.Log($"Cosas : era {_.isSoundOn} , pero ahora es {condition}");

        _.isSoundOn = condition;

        if (condition)
        {
            CheckMusic(true);
        }
        else
        {
            _.StopMusic();
        }
    }


    /// <summary>
    /// Revisamos si la musica puede sonar y qué musica poner,
    /// si está sonando la misma que corresponde no hace nada
    /// </summary>
    public static void CheckMusic(bool bypass = false){
        if (CanSound())
        {
            Data.Scenes _activeScene = DataFunc.ActiveScene();
            MusicPath key = MusicPath.no;
            switch (_activeScene)
            {
                case Data.Scenes.MenuScene:
                    key = (MusicPath)1;

                    break;
                case Data.Scenes.PreparationScene:
                    key = (MusicPath)3;

                    break;
                case Data.Scenes.GameScene:
                    key = (MusicPath)2;
                    break;
                default:
                    //nada supongo
                    break;
            }
           PlayThisMusic(key, bypass);
        }
        else
        {
            _.StopMusic();
        }

    }


    /// <summary>
    /// Reproduce la musica colocada
    /// </summary>
    public static void PlayThisMusic(MusicPath path = MusicPath.no, bool byPass = false){
        if (path != MusicPath.no && CanSound())
        {
            _.clip_music = Resources.Load<AudioClip>(_.musicPath + path);

            

            if (!_.clip_music.Equals(_.audio_music.clip) || byPass)
            {

                Debug.Log($"Reproduciendo : {path}");
                _.audio_music.clip = _.clip_music;
                _.audio_music.Play();
            }
        }
        else
        {
            _.StopMusic();
        }
    }

    /// <summary>
    /// Detenemos la musica actual
    /// </summary>
    private void StopMusic()
    {
        audio_music.Stop();
        clip_music = null;

    }

    /// <summary>
    /// Establecemos el volumen de la musica
    /// </summary>
    /// <param name="v"></param>
    public static void SetVolume(float v)
    {
        _.audio_music.volume = v;
    }

    /// <summary>
    /// Preguntamos si la musica esta permittida o no
    /// </summary>
    /// <returns></returns>
    public static bool CanSound() => _.isSoundOn;
    #endregion


    /// <summary>
    /// Reproduce el sonido de un boton, si esta disponible o
    /// no hara un sonido o otro
    /// </summary>
    /// <param name="condition"></param>
    public static void ButtonSound(bool condition = true)
    {
        ReproduceSound(condition ? SfxType.ButtonOn : SfxType.ButtonOff);
    }


    /// <summary>
    /// Inicia el sonido correspondiente
    /// </summary>
    public static void ReproduceSound(SfxType type){
        if (CanSound())
        {
            _.sfxItems[(int)type].PlaySound();

        }
    }
}

/*
 
  float percent = DataFunc.KnowPercentOfMax(index, fars.Length) / 100;
            MusicSystem.SetVolume(percent);
 
 */
