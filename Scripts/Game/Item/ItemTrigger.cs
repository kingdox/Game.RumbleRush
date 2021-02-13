using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{

    public ItemType type;

    private bool used = false;


    private void Update()
    {
        if (GameManager.status == GameStatus.InGame)
        {
            DestroyChecker();
        }
    }

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
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Coin);
                break;
            case ItemType.Heal:
                PlayerManager.player.HealLife();
                UIManager.VisualEff(VisualEffType.Heal);
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Heal);

                break;
            case ItemType.EnergyBuff:
                PlayerManager.player.energyMax += 1;
                UIManager.SetVisualStat((int)BuffType.Energy, (int)PlayerManager.player.energyMax);
                UIManager.VisualEff(VisualEffType.Special);
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Buff);

                break;
            case ItemType.SpeedBuff:
                PlayerManager.player.speedBase += 1;
                PlayerManager.player.speedActual += 1;
                UIManager.SetVisualStat((int)BuffType.Speed, (int)PlayerManager.player.speedActual);
                UIManager.VisualEff(VisualEffType.Special);
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Buff);

                break;
            case ItemType.ShieldBuff:
                PlayerManager.player.shieldsActual += 1;
                UIManager.SetVisualStat((int)BuffType.Shield, (int)PlayerManager.player.shieldsActual);
                UIManager.VisualEff(VisualEffType.Special);
                MusicSystem.ReproduceSound(MusicSystem.SfxType.Buff);
                break;
        }

    }

    /// <summary>
    /// revisa si se destruye:
    /// Basado con la posición de la camara en eje X, si sale de pantalla por la izquierda
    /// si la Y es inferior a un area safe de Y
    /// </summary>
    private void DestroyChecker()
    {
        //buscamos la camara
        float _cam_X = GameManager.GetCamera().transform.position.x;
        //conocemos su ancho
        float w = GameManager.GetCameraWidth();
        float h = GameManager.GetCameraHeight();

        bool passBoundX = transform.position.x < (_cam_X - w);

        bool passBoundY = transform.position.y < (h * -1);

        if (passBoundX || passBoundY)
        {
            Destroy(gameObject);
        }
    }

}



public enum ItemType
{
    Coin,
    Heal,
    EnergyBuff,
    SpeedBuff,
    ShieldBuff
}