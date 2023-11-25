using System;
using Cinemachine;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ProjectilAigua : Habilitat
    {
        private int playerDirection;

        private Vector3 projectileDirection;
        public Vector3 ProjectileDirection
        {
            get { return projectileDirection; }
        }

        public override void UtilitzarHabilitat()
        {
            if (canUse && checkWaterReserve() == true)
            {
                _health.Decrement();
                Vector2 spawnPosition = transform.position;
                item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                playerDirection = PlayerController.moveDirection;
                projectileDirection = playerDirection > 0 ? new Vector3(1f,.3f,0f) : new Vector3(-1f,.3f,0f);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
            else
            {
                Debug.Log("Vida= " + _health.getCurrentHP);
                Debug.Log("Reserva d'aigua insuficient");
            }
        }
    }
}