using System;
using Cinemachine;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ProjectilAigua : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        private GameObject projectile;
        private int playerDirection;
        private Vector3 projectileDirection;
        [SerializeField] private float shotCooldown = 1.5f;
        private float coolDown;
        private bool canShoot = true;

        public Vector3 ProjectileDirection
        {
            get { return projectileDirection; }
        }

        private void Awake()
        {
            coolDown = shotCooldown;
        }

        public void Disparar()
        {
            if (canShoot)
            {
                Vector2 spawnPosition = transform.position;
                projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
                playerDirection = PlayerController.moveDirection;
                projectileDirection = playerDirection > 0 ? new Vector3(1f,.3f,0f) : new Vector3(-1f,.3f,0f);
                canShoot = false;
            }
        }

        private void Update()
        {
            if (!canShoot)
            {
                coolDown -= Time.deltaTime;
                if (coolDown <= 0)
                {
                    canShoot = true;
                    coolDown = shotCooldown;
                }
            }
        }
    }
}