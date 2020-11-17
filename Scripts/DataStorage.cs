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

/*
 * Que se va a guardar?
 * 
 * - Dinero actual del jugador
 * - Record en metros recorridos
 * - Record de enemigos eliminados
 * - Ultima distancia en metros recorridos
 * - Ultima cantidad de enmigos eliminados
 * - Ultima cantidad de dinero gastado
 */