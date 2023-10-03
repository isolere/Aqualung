using System;
using Cinemachine;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Projectile : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        //[SerializeField] private AudioClip impactSound;

        [SerializeField] private float speed;
        [SerializeField] private int damage = 10;
        [SerializeField] private GameObject projectilePrefab;
        private GameObject projectile;
        private int playerDirection;
        private Vector3 projectileDirection;
        [SerializeField] private float shotCooldown = 1.5f;
        private float coolDown;
        private bool canShoot = true;

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
                //projectileDirection = playerDirection > 0 ? Vector3.right : Vector3.left;
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
            if (projectile)
            {
                projectile.transform.position += projectileDirection * Time.deltaTime * speed;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Impacte:" + other);

            /*AudioManager.Instance.PlayClip(impactSound);

            GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(FX, 2f);

            Health health = other.gameObject.GetComponent<Health>();

            // Si te salut apliquem el mal
            if (health != null)
            {
                health.TakeDamage(damage);
            }*/

            Destroy(gameObject);
        }
    }
}