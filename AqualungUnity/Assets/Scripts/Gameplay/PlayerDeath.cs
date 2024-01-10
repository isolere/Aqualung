using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (!player.health.IsAlive)
            {
                player.health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                // player.collider.enabled = false;
                player.controlEnabled = false;

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                player.animator.SetBool("dead", true);
                AudioManager.Instance.StopTrack();

                /*Aquí hem afegit un canvi, ja que al template original, quan el jugador moria, aquest tornava a 
                 reviure, i apareixia en un punt concret de l'escena. En el nostre cas no és així, i el que fem 
                és carregar l'escena de GameOver.*/

                GameManager.LoadGameOver();
                //Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}