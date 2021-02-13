using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowInX : MonoBehaviour
{
    ///Con este script seguiremos a un objeto en el eje X
    ///
    public Transform tr_follow;

    //Movimiento extra
    public float OffsetX = 8;

    private void LateUpdate()
    {
        UpdateX();
    }

    /// <summary>
    /// Actualiza la pos del objeto con respecto al seleccionado
    /// en el eje X
    /// </summary>
    private void UpdateX() {
        Vector3 _newPos = transform.position;
        _newPos.x = tr_follow.position.x + OffsetX;
        transform.position = _newPos;
    }

}
