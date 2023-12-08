using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();


        /*En aquest m�tode hem hagut de realitzar alguns canvis, ja que al template original el jugador
         moria despr�s de r�brer un impacte. Per altra banda, es t� en compte tant si l'enemic t� un component 
        Health com si no. En el nostre cas, sempre el tindr�.*/
        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(1);
                    }
                    else
                    {
                        player.Bounce(3);
                    }
                }
                /*Aquest condicional nom�s es reprodueix si l'enemic no t� component Health. En el nostre cas no hauria
                de succeir mai.*/
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            /*Ja que el jugador t� un component Health, i no mor amb un �nic impacte, accedim a la seva vida i la disminuim 
             en 1 punt.*/
            else
            {
                var playerHealth = player.GetComponent<Health>();
                playerHealth.Decrement();
                Debug.Log("Vida: " + playerHealth.getCurrentHP);
            }
        }
    }
}