using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /*Classe aprofitada del projecte Escape From IOC. S'aplica al projectil llençat pels enemics, i gestiona
     la seva direcció i velocitat, així com les col.lisions amb d'altres elements.*/
    public class ProjectileEnemic : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        [SerializeField] private AudioClip impactSound;

        [SerializeField] private float speed;

        private EnemyController _enemyController;

        private Vector3 _direction;

        private void Awake()
        {
            _enemyController = FindObjectOfType<EnemyController>();
        }

        /*Quan el projectil ha estat instanciat, obtenim la direcció en què s'ha de desplaçar, a partir de la
         direcció de l'enemic que l'ha generat.*/
        private void Start()
        {
            _direction = _enemyController.ProjectileDirection;
            Debug.Log("Direction: " + _direction);
        }

        /*Actualitzem la posició del projectil en el temps, a partir de la direcció i velocitat establertes, i tenint
         en compte que es tracta d'un RigidBody, i es veu afectat per la gravetat.*/
        private void Update()
        {
            if (_enemyController != null)
            {
                transform.position += _direction * Time.deltaTime * speed;
            }
        }

        /*Quan el projectil "impacta" amb un objecte amb el que pot col.lidir (hem configurat les col.lisions als Project Settings)
         es reprodueix un so. Comprovem si l'impacte ha estat amb el jugador, i en cas afirmatiu, disminuim la seva salut. Per 
        acabar, destruim la instància del projectil.*/
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Impacte:" + other);

            AudioManager.Instance.PlayClip(impactSound);

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