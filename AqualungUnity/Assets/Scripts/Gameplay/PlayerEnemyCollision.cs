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


        /*En aquest mètode hem hagut de realitzar alguns canvis, ja que al template original el jugador
         moria després de rèbrer un impacte. Per altra banda, es té en compte tant si l'enemic té un component 
        Health com si no. En el nostre cas, sempre el tindrà.*/
        public override void Execute()
        {
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;
            var awayFromPlayer = enemy.Bounds.center.y >= player.Bounds.max.y;

            if (willHurtEnemy)
            {
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(false);
                    player.Bounce(4);
                }
                /*Aquest condicional només es reprodueix si l'enemic no té component Health. En el nostre cas no hauria
                de succeir mai.*/
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else if (awayFromPlayer)
            {
                if (enemy.control.spriteRenderer.flipX == false)
                {
                    Vector2 direccioRebot = new Vector2(4f, 5f);
                    enemy.control.Bounce(direccioRebot);
                }
                else
                {
                    Vector2 direccioRebot = new Vector2(-4f, 5f);
                    enemy.control.Bounce(direccioRebot);   
                }
            }
            /*Ja que el jugador té un component Health, i no mor amb un únic impacte, accedim a la seva vida i la disminuim 
             en 1 punt.*/
            else
            {
                var playerHealth = player.GetComponent<Health>();
                playerHealth.Decrement(false);
                Debug.Log("Vida: " + playerHealth.getCurrentHP);
            }
        }
    }
}