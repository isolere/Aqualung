using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Projectile : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        //[SerializeField] private AudioClip impactSound;

        [SerializeField] private float speed;
        [SerializeField] private int damage = 10;

        private ProjectilAigua _projectilAigua;

        private Vector3 _direction;

        private void Awake()
        {
            Debug.Log("Bola d'aigua creada");
            _projectilAigua = FindObjectOfType<ProjectilAigua>();
        }

        private void Start()
        {
            _direction = _projectilAigua.ProjectileDirection;
        }

        private void Update()
        {
            if (_projectilAigua != null)
            {
                transform.position += _direction * Time.deltaTime * speed;
            }
            /*El problema és que forcem que segueixi en la mateixa direcció, en comptes de canviar, si impacta amb algun objecte.
             Hem de fer que quan impacti, es destrueixi*/
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Impacte:" + other);

            //AudioManager.Instance.PlayClip(impactSound);

            /*GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(FX, 2f);*/

            Health health = other.gameObject.GetComponent<Health>();
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            // Si te salut apliquem el mal
            if (health != null && enemyController != null)
            {
                Debug.Log("fa mal:" + health.currentHP);
                health.Decrement();
                if (!health.IsAlive)
                {
                    Schedule<EnemyDeath>().enemy = enemyController;
                }
            }
            Destroy(gameObject);
        }
    }
}