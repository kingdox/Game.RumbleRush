using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataStorage
{
    //aquí se vuelve a colocar los datos puestos debajo...
    public SavedData savedData = new SavedData();
    //Con esto podremos guardar los datos de datapass a DataStorage
    public DataStorage(SavedData saved)
    {
        savedData = saved;
    }
}
