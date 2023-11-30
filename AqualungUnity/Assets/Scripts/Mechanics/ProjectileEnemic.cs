using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class ProjectileEnemic : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        //[SerializeField] private AudioClip impactSound;

        [SerializeField] private float speed;

        private EnemyController _enemyController;

        private Vector3 _direction;

        private void Awake()
        {
            _enemyController = FindObjectOfType<EnemyController>();
        }

        private void Start()
        {
            _direction = _enemyController.ProjectileDirection;
        }

        private void Update()
        {
            if (_enemyController != null)
            {
                transform.position += _direction * Time.deltaTime * speed;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Impacte:" + other);

            //AudioManager.Instance.PlayClip(impactSound);

            /*GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(FX, 2f);*/

            Health health = other.gameObject.GetComponent<Health>();
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            // Si te salut apliquem el mal
            if (health != null && playerController != null)
            {
                health.Decrement();
            }
            Destroy(gameObject);
        }
    }
}