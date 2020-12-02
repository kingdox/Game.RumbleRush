using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{

    public ItemType type;

    private bool used = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && !used)
        {
            used = true;
            ActionItemType();
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Dependiendo del tipo de item ejecutara algo distinto
    /// </summary>
    private void ActionItemType()
    {
        switch (type)
        {
            case ItemType.Coin:
                PlayerManager.player.CollectCoin();
                break;
            case ItemType.Heal:
                PlayerManager.player.HealLife();
                break;
        }
    }

}



public enum ItemType
{
    Coin,
    Heal
}