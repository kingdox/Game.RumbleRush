using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Var
    private float endOfPlatform_X;
    [Header("Settings")]
    // -> para conocer el final de la plataforma
    public GameObject bodyPlatform;
    //-> para conocer junto con bodyPlatform la pos final de la plataforma
    //(podría invertir el indicador y evitarme ese calculo, pero lo dejaré así por
    // que me permite saber el tamaño de la plataforma....
    public GameObject indicator;
    #endregion
    #region Events
    private void Start()
    {
        endOfPlatform_X = indicator.transform.position.x + bodyPlatform.transform.localScale.x;
    }
    private void Update()
    {
        CheckDestroyMargin();
    }
    private void OnDestroy()
    {
        //LLamamos al generator
        //TODO
    }
    #endregion
    #region Methods

    /// <summary>
    /// Revisa si ha pasado el limite especificado para que sea borrado sin verse en pantalla, Elimina si la plataforma atraviesa el margen,
    /// </summary>
    private void CheckDestroyMargin(){
        float margin = GameManager.GetCamera().transform.position.x + (Vector3.left * GameManager.GetCameraWidth()).x;
        if (endOfPlatform_X < margin) DestroyPlatform();
    }

    /// <summary>
    /// Elimina la plataforma y manda la señal al generador de que una plataforma ha sido eliminada
    /// </summary>
    private void DestroyPlatform() => Destroy(gameObject);

    #endregion
    #region DEBBUG

    /// <summary>
    /// Calculos shidos xdxdxd
    /// </summary>
    private void OnDrawGizmos(){
        float camHeightAprox = DataFunc.GetScreenHeightUnit();

        Vector3 range = Vector3.up * (camHeightAprox / 2);
        Vector3 ind_pos = indicator.transform.position;
        Vector3 end_pos = Vector3.right * bodyPlatform.transform.localScale.x;
        Vector3 safeDestroy = ind_pos + end_pos;

        Gizmos.color = Color.white;
        // Area donde al hacer contacto se elimina, es el final de la plataforma
        //NOTA: Aqui range no significa nada, es solo para uqe se vea coherente en el debugger y no moleste
        Gizmos.DrawLine(safeDestroy + range / 2, safeDestroy - range / 2);

    }
    #endregion
}