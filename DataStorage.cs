using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorage
{

    //aquí se vuelve a colocar los datos puestos debajo...

    public int Actualmoney;
    public float RecordMetersReached;
    public int RecordMonstersKilled;

    public int LastMoneySpent;
    public float LastMetersReached;
    public int LastMonstersKilled;




    public DataStorage(DataPass dataPass)
    {
        //Aqui se pone los datos que van a ser guardados
    }



}


/*
 * TODO
 * 
 * Que se va a guardar?
 * 
 * - Dinero actual del jugador
 * - Record en metros recorridos
 * - Record de enemigos eliminados
 * 
 * --
 * 
 * - Ultima distancia en metros recorridos
 * - Ultima cantidad de enmigos eliminados
 * - Ultima cantidad de dinero gastado
 * 
 * 
 * 
 * -> EXTRA (en caso de tener tiempo libre
 * - Partidas jugadas
 * - Total de metros
 * - Total de monstruos
 * - Total dinero gastado
 * 
 */