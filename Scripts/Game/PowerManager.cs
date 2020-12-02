using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {
    private static PowerManager _;

    //Estos son los beneficios de los poderes de cada uno
    [Header("Powers")]
    // cantidad que aumenta de velocidad al monje
    public float monk_buffSpeed = 0.005f;

    // cantidad que cura al paladín, es un int... hacer un cooldown mejor?
    public float paladin_heal = 25f;

    // comprobación de si puedes volver a saltar
    public bool hunter_canReJump = true;

    // reducción para aumento de generación de monstruos, esto dividirá el range
    public int brutus_reductSpawnMonster = 2;

    [Header("Settings")]
    [Tooltip("Vemos si puede usarse o no el poder")]
    public bool isPoweOn = false;

    public void Awake() {
        if (_ == null) _ = this;
        else if (_ != this) Destroy(gameObject);
    }

    private void Update() {

        isPoweOn = PlayerManager.player.cooldownActual >= PlayerManager.player.cooldownMax;

    }





    /// <summary>
    /// Checkea si eres el personaje Monje y, tienes poder
    /// para aumentar la velocidad.
    /// En caso de que no lo seas usarás la velocidad base establecida
    /// </summary>
    /// <returns> la nueva velocidad</returns>
    public static float PlayerSpeedUpdate() {
        float newSpeedActual = PlayerManager.player.speedActual;

        if (_.isPoweOn && GameSetup.character.type == CharacterType.Monk) {

            newSpeedActual += _.monk_buffSpeed;
            // Debug.Log($"Monk Speed {newSpeedActual}  de {PlayerManager.player.speedActual}");

        } else {
            newSpeedActual = PlayerManager.player.speedBase;
        }
        return newSpeedActual;
    }

    /// <summary>
    /// Reduce la vida del personaje
    /// en caso de ser paladin y tener poder activará la curación gradual
    /// </summary>
    /// <returns></returns>
    public static float PlayerEnergyUpdate() {
        float newEnergyActual = PlayerManager.player.energyActual;

        //Revisamos si el poder esta disponible y si es tipo paladin
        if (_.isPoweOn && GameSetup.character.type == CharacterType.Paladin) {
            newEnergyActual += Time.deltaTime / _.paladin_heal ;
            //Debug.Log($"{newEnergyActual} de {PlayerManager.player.energyActual}");
        } else {
            newEnergyActual -= Time.deltaTime / Data.data.lifeReductor;
        }

        return newEnergyActual;
    }


    /// <summary>
    /// Checkea si eres el jugador Hunter, en caso de serlo
    /// no cambiará el salto del controller si posee uno aquí
    /// en caso de no ser este personaje no hará alteración de la var ?
    /// </summary>
    /// <returns>si puede saltar o no</returns>
    public static bool PlayerJumpUpdate(bool canJump, bool toRecover = false) {

        bool newCanJump = canJump;

        //Si el proposito es recuperarse por que llego al suelo
        if (toRecover) {
            //y como reestablecemos el reJump?
            newCanJump = true;
            _.hunter_canReJump = true;

            //Si el proposito es sustraer alguno...
        } else {

            //En caso de tener el poder y ser el personaje...
            if (_.isPoweOn && GameSetup.character.type == CharacterType.Hunter) {

                //Reviso si hay un rejump, de ser así lo usas y dejas true
                //el canJump , poniendo false el rejump
                if (_.hunter_canReJump) {
                    _.hunter_canReJump = false;
                    newCanJump = true;
                } else {
                    newCanJump = false;
                }

            } else {
                //lo normal sería canJump false
                newCanJump = false;
            }
        }


        return newCanJump;
    }


    /// <summary>
    /// Si eres el Barbaro y tienes el poder reduces  el tiempo que nesecita para
    /// generar mosntruos
    /// </summary>
    /// <returns>regresa el nuevo rango</returns>
    public static float PlayerSpawnsMonsterUpdate(float cd) {

        float newCooldown = cd;

        if (_.isPoweOn && GameSetup.character.type == CharacterType.Brutus) {
            newCooldown /= _.brutus_reductSpawnMonster;
            //Debug.Log($"Brutus spawner {newCooldown} de {_.brutus_reductSpawnMonster}");
        }
        return newCooldown;
    }

}



/*
 Poseeremos las variables y la cantidad que aumenta en los determinados valores o lo que sea
 pueden ser var de cualquier tipo
*/