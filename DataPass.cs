#region ####################### IMPLEMENTATION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
#endregion
#region ### CLASS
public class DataPass : MonoBehaviour
{
    #region ####### DECLARATION

    [HideInInspector]
    public static DataPass Instance;//Singleton....
    [HideInInspector]
    public ActualStatus status;

    [Header("Saved Data")]
    public int Actualmoney;
    public float RecordMetersReached;
    public int RecordMonstersKilled;
    //[Space]
    public int LastMoneySpent;
    public float LastMetersReached;
    public int LastMonstersKilled;



    

    #endregion
    #region ###### EVENTS
    private void Awake()
    {
        //Singleton corroboration
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SetDefault();
    }

    private void Start()
    {
        status = ActualStatus.start;
        StatusUpdate();
    }

    private void Update()
    {

        if (status != ActualStatus.end) //load the img
        {
            StatusUpdate();
        }
    }

    #endregion
    #region ####### METHODS
    /// <summary>
    /// Establecemos por defecto los valores de un jugador nuevo
    /// </summary>
    private void SetDefault()
    {
        Actualmoney = 0;
        RecordMetersReached = 0;
        RecordMonstersKilled = 0;

        LastMoneySpent = 0;
        LastMetersReached = 0;
        LastMonstersKilled = 0;
    }


    public void StatusUpdate()
    {
        switch (status)
        {
            case ActualStatus.end:
                status = ActualStatus.loading;
                DataInit();//Primero carga o crea un archivo
                status = ActualStatus.ready;
                break;

            case ActualStatus.ready:// si el datapass esta listo por que creo o cargo algo entonces inicia
                status = ActualStatus.loading;
                LoadResources();
                break;
            case ActualStatus.loading:
                //....Wait
                break;
            default:
                break;
        }
    }



    public void LoadResources()
    {
        //spriteToken = Resources.Load<Sprite>( data.path_Img + data.pathShapes[indexTokenImg]);
        
        status = ActualStatus.end;
    }



    /// <summary>
    /// Revisamos si existen datos guardados, de no existir los crea
    /// </summary>
    private void DataInit()
    {
        //string path = Application.persistentDataPath + data.savedPath;
        /*
        if (File.Exists(path))
        {
            //lo carga
            LoadData();
        }
        else
        {
            //lo crea
            SaveData(this);
        }
        */
    }



    /// <summary>
    /// Guardamos un archivo con los datos de dataPass
    /// </summary>
    /// <param name="dataPass"></param>
    public void SaveData(DataPass dataPass)
    {
        //string path = Application.persistentDataPath + data.savedPath;
        /*
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        DataStorage dataS = new DataStorage(dataPass);

        formatter.Serialize(stream, dataS);
        stream.Close();
        */
    }

    /// <summary>
    /// Aqui cargamos los archivos
    /// </summary>
    public void LoadData()
    {
        /*
        string path = Application.persistentDataPath + data.savedPath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        DataStorage savedDataStorage = formatter.Deserialize(stream) as DataStorage;
        stream.Close();
        */
        // gamesPlayed = savedDataStorage.gamesPlayed;
           
    }

    #endregion
}
#endregion
#region ###########EXTRA
public enum ActualStatus
{

    start,
    ready,
    loading,
    end
}
#endregion