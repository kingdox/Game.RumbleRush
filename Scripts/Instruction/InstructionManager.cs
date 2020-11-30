using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    #region VAR

    [Header("Page settings")]
    public GameObject pagesContainer;
    public Text text_actualPage;
    private int actualPage = 0;
    private GameObject[] pages;
    #endregion
    #region Events
    private void Start()
    {
        GetPages();
        RefreshPage();

    }
    #endregion
    #region Methods

    /// <summary>
    /// Obtenemos todas las paginas que posee el contenedor de paginas
    /// </summary>
    private void GetPages()
    {
        //Obtenemos las paginas
        int _c = pagesContainer.transform.childCount;

        pages = new GameObject[_c];
        for (int x = 0; x < _c; x++)
        {
            pages[x] = pagesContainer.transform.GetChild(x).gameObject;
        }
    }

    /// <summary>
    /// Dependiendo del actualPage activamos o desactivamos las paginas
    /// </summary>
    private void RefreshPage() 
    {
        for (int j = 0; j < pages.Length; j++) DataFunc.ObjOnOff(pages[j], j == actualPage);
        text_actualPage.text = (actualPage + 1) + " / " + pages.Length;
    }



    /// <summary>
    /// Cambiamos la pagina
    /// </summary>
    /// <param name="goForward"></param>
    public void ChangePage(bool goForward)
    {
        actualPage = DataFunc.TravelArr(goForward, pages.Length, actualPage);
        RefreshPage();
    }

    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int i) => DataFunc.ChangeSceneTo(i);
    #endregion
}