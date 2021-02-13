using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Var
    private float endOfPlatform_X;
    [Header("Settings")]
    public GameObject indicator;

    #endregion
    #region Events
    private void Start()
    {
        endOfPlatform_X = indicator.transform.position.x + transform.localScale.x;
    }
    private void Update()
    {
        CheckDestroyMargin();
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisa si ha pasado el limite especificado para que sea borrado sin verse en pantalla, Elimina si la plataforma atraviesa el margen,
    /// </summary>
    private void CheckDestroyMargin(){
        float margin = GameManager.GetCamera().transform.position.x + (Vector3.left * GameManager.GetCameraWidth()).x;
        if (endOfPlatform_X < margin)
        {
            Destroy(gameObject);
            Generator.CheckSpawnTo();
        }

    }

        #endregion
}