using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{

    [Header("Monster Floor")]
    public GameObject obj_monsterContainer_floor;
    public GameObject pref_monster_floor;

    [Header("Monster Aero")]
    public GameObject obj_monsterContainer_aero;
    public GameObject pref_monster_aero;

    [Header("MonsterGenerator settings")]
    //Limite de monstruos en pantalla
    public int monsterLimit = 5;

    //Indicador de cuantos segundos deben pasar para generar un monstruo
    public float spawnCooldown_floor  = 4;
    private float cooldownCount_floor = 0;

    public float spawnCooldown_aero = 4;
    private float cooldownCount_aero = 0;


    private void Update()
    {
        if (GameManager.status == GameStatus.InGame)
        {
            SpawnChecker();
        }
    }
        
    /// <summary>
    /// Revisa si se puede hacer spawn o no
    /// </summary>
    private void SpawnChecker()
    {
        //Spawn de slimes
        if (cooldownCount_floor + Time.deltaTime > spawnCooldown_floor)
        {
            cooldownCount_floor = 0;
            int[] range = GameSetup.hardMode ? Data.data.hardMode_spawnTimeRange : Data.data.spawnTimeRange_floor;

            spawnCooldown_floor = PowerManager.PlayerSpawnsMonsterUpdate(Random.Range(range[0], range[1]));
            SpawnMonster_Floor(Vector2.zero);
        }

        if (cooldownCount_aero + Time.deltaTime > spawnCooldown_aero)
        {
            cooldownCount_aero = 0;
            int[] range = GameSetup.hardMode ? Data.data.hardMode_spawnTimeRange : Data.data.spawnTimeRange_aero;

            spawnCooldown_aero = PowerManager.PlayerSpawnsMonsterUpdate(Random.Range(range[0], range[1]));
            SpawnMonster_Aero(Vector2.zero);

        }

        cooldownCount_aero += Time.deltaTime;
        cooldownCount_floor += Time.deltaTime;

    }


    /// <summary>
    /// Generas un Monstruo de tipo suelo
    /// </summary>
    private void SpawnMonster_Floor(Vector2 especificPos) {

        Vector2 _newPos = especificPos;

        if (especificPos.Equals(Vector2.zero)){
            _newPos = GameManager.GetCamera().transform.position;
            float w = GameManager.GetCameraWidth();
            _newPos.x += (w / 2) + 5;
            _newPos.y = 0.5f;
        }

        // GameObject _obj = 
        Instantiate(
           pref_monster_floor,
           _newPos,
           Quaternion.identity,
           obj_monsterContainer_floor.transform
       );


    }


    /// <summary>
    /// Generas un mosntruo de tipo aero 
    /// </summary>
    private void SpawnMonster_Aero(Vector2 especificPos){
        Vector2 _newPos = especificPos;

        if (especificPos.Equals(Vector2.zero))
        {
            _newPos = GameManager.GetCamera().transform.position;
            float w = GameManager.GetCameraWidth();
            float h = GameManager.GetCameraHeight();

            _newPos.x += (w / 2) + 5;
            _newPos.y = Random.Range(3, h - h/4);
            
        }

        Instantiate(
           pref_monster_aero,
           _newPos,
           Quaternion.identity,
           obj_monsterContainer_aero.transform
       );
    }
}

public enum MonsterType
{
    Monster_Floor,
    Monster_Aero
}