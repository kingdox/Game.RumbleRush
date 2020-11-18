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
    #region ####### VARIABLES

    [HideInInspector]
    public static DataPass Instance;//Singleton....

    [Header("Saved Data")]
    public SavedData savedData = new SavedData();

    [Header("DataPass info")]
    public bool isReady = false;

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
        isReady = false;
        string path = Application.persistentDataPath + Data.data.savedPath;
        Debug.Log($"El archivo Existe?? {File.Exists(path)}, Ruta: {path}");

        SettingFile(!File.Exists(path));
        isReady = true;
    }

    /// <summary>
    /// Guardamos ó cargamos el archivo que poseeremos para contener los datos importantes
    /// </summary>
    /// <param name="wantSave"></param>
    public void SettingFile(bool wantSave = false)
    {
        string _path = Application.persistentDataPath + Data.data.savedPath;
        BinaryFormatter _formatter = new BinaryFormatter();
        FileStream _stream = new FileStream(_path, wantSave ? FileMode.Create : FileMode.Open);
        DataStorage _dataStorage;
       
        //Dependiendo de si va a cargar o guardar hará algo o no
        if (wantSave)
        {
            _dataStorage = new DataStorage(savedData);
            _formatter.Serialize(_stream, _dataStorage);
            _stream.Close();
        }
        else
        {
            _dataStorage = _formatter.Deserialize(_stream) as DataStorage;
            _stream.Close();
            savedData = _dataStorage.savedData;
        }
    }
   
    #endregion
}
#endregion
