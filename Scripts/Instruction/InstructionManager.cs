using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{




    /// <summary>
    ///  Cambiamos de escena
    /// </summary>
    /// <param name="i"></param>
    public void ChangeSceneTo(int i) => DataFunc.ChangeSceneTo(i);
}
