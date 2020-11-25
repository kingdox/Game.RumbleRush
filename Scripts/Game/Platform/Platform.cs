using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Var

    private Camera camera;
    public int destroyMargin;

    public GameObject bodyPlatform;

    /// <summary>
    /// Si el indicator de laa plataforma
    /// posee una distancia de -X cantidad se elimina, significando que ha
    /// terminado su uso con el player, luego se le añade un extras
    /// </summary>
    public GameObject indicator;

    


    #endregion
    #region Events
    private void OnDrawGizmos()
    {
        Vector3 range = Vector3.up * (Data.data.platformRangeY / 2);
        Vector3 ind_pos = indicator.transform.position;
        Vector3 end_pos = Vector3.right * bodyPlatform.transform.localScale.x;


        Gizmos.color = Color.white;

        // Area donde otras no pueden spawnearse
        Gizmos.DrawLine( ind_pos + range, ind_pos - range);

        // Area donde muestra la distancia de la plataforma
        Gizmos.DrawLine( ind_pos - range, ind_pos - range + end_pos );

        // Area donde al hacer contacto se elimina
        Gizmos.DrawLine(
            ind_pos + range,
            (ind_pos + end_pos - range) 
            );

        //Descubrimos el punto para saber si podemos eliminar la plataforma
        Vector3 safeDestroy = ind_pos + end_pos + (Vector3.right * Data.data.safeBoundDeleteX);
        Gizmos.DrawLine(ind_pos, safeDestroy);


    }
    #endregion
    #region Methods


    private void DestroyPlatform()
    {
        // solicito que genere nueva plataforma
        //Manager.instance.Spawn();

        //Destroy(gameObject);
    }

    private void CheckDestroyMargin()
    {
        if (transform.position.x < camera.transform.position.x - destroyMargin)
        {
            DestroyPlatform();
        }


        Vector2 lineA = new Vector3(
            camera.transform.position.x - destroyMargin,
            camera.transform.position.y + Data.data.platformMaxY
        );
        Vector2 lineB = new Vector3(
            camera.transform.position.x - destroyMargin,
            camera.transform.position.y
        );//camera.transform.position.y - 10f

        //Gizmos.color = Color.green;
        Debug.DrawLine(lineA, lineB, Color.green);
    }

    #endregion
}