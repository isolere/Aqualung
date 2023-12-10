using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    /// 
    /*Classe que incorporem a les DeathZones existents al joc. Quan un gameObject, ja sigui jugador o enemic entra en contacte amb
     el seu collider, s'activa el mètode Die() del seu component Health, que fa que la seva vida sigui zero, i per tant el fa morir.*/
    public class DeathZone : MonoBehaviour
    {
        private Health _health;

        void OnTriggerEnter2D(Collider2D collider)
        {
            _health = collider.gameObject.GetComponent<Health>();
            _health.Die();
        }
    }
}