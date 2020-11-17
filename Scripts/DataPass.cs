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

    private readonly Data data = new Data();
    [HideInInspector]
    public static DataPass Instance;//Singleton....

    [Header("Saved Data")]
    public SavedData savedData = new SavedData();

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
    }

    private void Start()
    {
        DataInit();
    }

    #endregion
    #region ####### METHODS

    /// <summary>
    /// Revisamos si existen datos guardados, de no existir los crea
    /// </summary>
    private void DataInit()
    {
        string path = Application.persistentDataPath + data.savedPath;
        Debug.Log($"El archivo Existe?? {File.Exists(path)}, Ruta: {path}");
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
    }

    /// <summary>
    /// Guardamos un archivo con los datos de dataPass
    /// </summary>
    /// <param name="dataPass"></param>
    public void SaveData(DataPass dataPass)
    {
        string path = Application.persistentDataPath + data.savedPath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);


        DataStorage dataS = new DataStorage(dataPass.savedData);
        Debug.Log($"Veces que se ha Guardado el archivo '{data.savedPath}' : {dataS.savedData.debug_savedTimes}");
        
        formatter.Serialize(stream, dataS);
        stream.Close();
    }



    /*
    private string GetAppPath() => Application.persistentDataPath + data.savedPath;
    */

    public FileSettings SettingFile(DataPass _datapass = null)
    {
        bool wantSave = _datapass;

        FileSettings fileSettings = new FileSettings();

        fileSettings.path = Application.persistentDataPath + data.savedPath;
        fileSettings.formatter = new BinaryFormatter();
        fileSettings.mode = wantSave ? FileMode.Open : FileMode.Create;

        return fileSettings;
    }
    /// <summary>
    /// Aqui cargamos los archivos basado en el Data.savedPath
    /// </summary>
    public void LoadData()
    {
        //FileSettings f = new FileSettings();
        //f = SettingFile();
        string path = Application.persistentDataPath + data.savedPath;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        DataStorage savedDataStorage = formatter.Deserialize(stream) as DataStorage;
        stream.Close();

        savedData = savedDataStorage.savedData;
        Debug.Log($"Veces que se ha Guardado el archivo '{data.savedPath}' : {savedData.debug_savedTimes}");
    }

    #endregion
}
#endregion
#region ###########EXTRA

public class FileSettings
{
    public string path ="";
    public BinaryFormatter formatter;
    public FileMode mode;
}
#endregion