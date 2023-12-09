using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /*Classe aprofitada del projecte Escape From IOC. S'aplica al projectil llençat pels enemics, i gestiona
     la seva direcció i velocitat, així com les col.lisions amb d'altres elements.*/
    public class Projectile : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        [SerializeField] private AudioClip impactSound;

        [SerializeField] private float speed;
        [SerializeField] private int damage = 10;

        private ProjectilAigua _projectilAigua;

        private Vector3 _direction;

        private SpriteRenderer _spriteRenderer;

        private GameObject _particleObject;
        private ParticleSystem _particules;

        private void Awake()
        {
            Debug.Log("Bola d'aigua creada");
            _projectilAigua = FindObjectOfType<ProjectilAigua>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            //Cerquem el sistema de partícules que s'executarà quan el projectil impacti
            _particleObject = GameObject.FindWithTag("ParticulesProjectil");
            _particules = _particleObject.GetComponent<ParticleSystem>();
        }

        /*Quan el projectil ha estat instanciat, obtenim la direcció en què s'ha de desplaçar, a partir de la
        direcció en la que mira el jugador.*/
        private void Start()
        {
            _direction = _projectilAigua.ProjectileDirection;
            //Ajustem la direcció en la que apunta el Sprite del projectil
            if(_direction.x>0)
            {
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        }

        /*Actualitzem la posició del projectil en el temps, a partir de la direcció i velocitat establertes, i tenint
         en compte que es tracta d'un RigidBody, i es veu afectat per la gravetat.*/
        private void Update()
        {
            if (_projectilAigua != null)
            {
                transform.position += _direction * Time.deltaTime * speed;
            }
        }

        /*Quan el projectil "impacta" amb un objecte amb el que pot col.lidir (hem configurat les col.lisions als Project Settings)
        es reprodueix un so. Comprovem si l'impacte ha estat amb un enemic, i en cas afirmatiu, disminuim la seva salut. Per 
        acabar, destruim la instància del projectil.*/
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Impacte:" + other);

            AudioManager.Instance.PlayClip(impactSound);

            /*GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(FX, 2f);*/

            Health health = other.gameObject.GetComponent<Health>();
            EnemyController enemyController = other.gameObject.GetComponent<EnemyController>();

            // Si te salut apliquem el mal
            if (health != null && enemyController != null)
            {
                health.Decrement();
            }
            //Situem el sistema de partícules a la posició de l'impacte i l'executem.
            _particules.transform.position = transform.position;
            _particules.Play();
            Destroy(gameObject);
        }
    }
}