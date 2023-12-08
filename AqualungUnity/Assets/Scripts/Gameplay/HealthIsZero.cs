using UnityEngine;
using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player health reaches 0. This usually would result in a 
    /// PlayerDeath event.
    /// </summary>
    /// <typeparam name="HealthIsZero"></typeparam>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;

        /*En aquest m�tode va ser necessari realitzar canvis, ja que nom�s contemplava l'esdeveniment
         <PlayerDeath>. Sense saber perqu�, el jugador moria quan impactava amb els enemics i els eliminava,
        i va ser necessari afegir aquest condicional per comprovar si el GameObject que es quedava amb el 
        component Health a zero era el jugador o un enemic.*/
        public override void Execute()
        {
            if (health.gameObject.CompareTag("Player"))
            {
                Schedule<PlayerDeath>();
            }
            else if (health.gameObject.CompareTag("Enemic"))
            {
                Schedule<EnemyDeath>();
            }
        }
    }
}