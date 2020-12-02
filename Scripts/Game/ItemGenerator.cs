using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    private static ItemGenerator _;
    // Genera un item en la posición señalada
    // Dependiendo de la cantidad, puede generar un healer o moneda

    //Healer y moneda son objetos kinematicos con trigger

    //para sacar un tipo healer debe pasar x tiempo, si eso no ocurre
    //queda en un 50% de si crear o no una moneda

    //Cuando se debe crear un healer se crea SI o SI
    #region VAR
    [Header("Spawn settings")]
    //Prefabs de los items
    public GameObject pref_healer;
    public GameObject pref_money;
    public GameObject obj_container_healer;
    public GameObject obj_container_money;



    [Header("Settings")]
    private float[] healerItemCooldownRange = { 10 , 20 };
    private float healerItemCooldownLimit;
    private float healerItemCooldownCount = 0;

    #endregion
    #region EVENTS
    private void Awake()
    {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
    }
    private void Start()
    {
        SetRandomHealerCooldown();
    }
    private void Update()
    {
        //Hacemos conteo de si se puede o no hacer el conteo
        healerItemCooldownCount = Mathf.Clamp(healerItemCooldownCount + Time.deltaTime, 0, healerItemCooldownLimit);
    }
    #endregion
    /// <summary>
    /// Vemos si se puede crear un item healer, sino tendremos 50%
    /// de crear una moneda.
    /// </summary>
    /// <param name="plat_size"></param>
    /// <param name="plat_pos"></param>
    public static void Generate(Vector2 plat_size, Vector2 plat_pos)
    {
        Vector2 spawnPos = plat_pos;
        float _extra = Random.Range(0, plat_size.x / 2);
        spawnPos.x = Random.Range(0, 2) == 0 ? spawnPos.x + _extra : spawnPos.x - _extra;
        spawnPos.y += plat_size.y * 2;

        //En caso de que pueda generar un healer lo hace
        if (_.healerItemCooldownCount >= _.healerItemCooldownLimit)
        {
            _.healerItemCooldownCount = 0;
            _.SetRandomHealerCooldown();
            _.GenItem(_.pref_healer, _.obj_container_healer, spawnPos);
        }
        else
        {
            //sino pregunta en 50% si hace moneda o no
            if (Random.Range(0,2) == 0)
            {
                //Genera la moneda
                _.GenItem(_.pref_money, _.obj_container_money, spawnPos);
            }

        }

    }

    /// <summary>
    /// Generas el item escogido con los parametros
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <param name="pos"></param>
    private void GenItem(GameObject prefab, GameObject parent, Vector2 pos) => Instantiate(prefab, pos, Quaternion.identity, parent.transform);

    /// <summary>
    /// Actualiza el limite para que se pueda generar un botiquín
    /// </summary>
    private void SetRandomHealerCooldown() => healerItemCooldownLimit = Random.Range(healerItemCooldownRange[0], healerItemCooldownRange[1]);

}
//Divide y vencerás
