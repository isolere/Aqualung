using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// A simple controller for enemies. Provides movement control over a patrol path.
    /// </summary>
    /// 
    /*Aquesta classe ve incorporada al template del projecte original, però hi hem realitzat alguns ajustaments per
     adaptar-la al nostre projecte. Gestionarem el seguiment i atac contra el jugador, així com la mort de l'enemic.*/
    [RequireComponent(typeof(AnimationController), typeof(Collider2D))]
    public class EnemyController : MonoBehaviour
    {
        public PatrolPath path;
        public AudioClip ouch;

        internal PatrolPath.Mover mover;
        internal AnimationController control;
        internal Collider2D _collider;
        internal AudioSource _audio;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        public float distanciaDeteccio;
        public float distanciaAtac;
        private GameObject _player;
        private Animator _animator;

        [SerializeField] private float _cooldown=1f;
        private float _lastAttack;

        [SerializeField] private GameObject _projectilePrefab;
        private GameObject projectile;

        private int moveDirection;
        private Vector3 _projectileDirection;
        public Vector3 ProjectileDirection
        {
            get { return _projectileDirection; }
        }

        private bool _isDead;
        private bool _outOfRange;

        private Rigidbody2D _rigidbody;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = GetComponent<Animator>();
            _isDead = false;
            _outOfRange = true;
            _rigidbody = GetComponent<Rigidbody2D>();

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null&&!_isDead)
            {
                var ev = Schedule<PlayerEnemyCollision>();
                ev.player = player;
                ev.enemy = this;
            }
        }

        void Update()
        {
            float distanceFromPlayer = Vector2.Distance(_player.transform.position, transform.position);

            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }
            /*Gestionem la distància envers el jugador, i la reacció de l'enemic en conseqüència. Si la distància és menor a la
             distància de detecció, l'enemic s'aproximarà al jugador. En cas contrari, l'enemic es quedarà al seu lloc, o tornarà
            al Patrol Path que li hagi estat assignat.*/
            if (distanceFromPlayer < distanciaDeteccio)
            {
                //Gestionem les alertes dins el GameState per aplicar o no música de combat.
                if (_outOfRange)
                {
                    Debug.Log("Incrementa");
                    GameState.Instance.IncreaseAlert();
                    _outOfRange = false;
                }

                /*Si la distància és major a la distància d'atac, l'enemic s'aproparà al jugador. En cas contrari, l'enemic deixarà de moure's, 
                 i començarà a atacar al jugador.*/
                if (distanceFromPlayer > distanciaAtac)
                {
                    control.move.x = Mathf.Clamp(_player.transform.position.x - transform.position.x, -1, 1);
                }
                else
                {
                    control.move.x = 0;
                    if (Time.time - _lastAttack > _cooldown && !_isDead)
                    {
                        _animator.SetTrigger("Attacking");
                        _lastAttack = Time.time;
                    }
                }
            }
            else
            {
                //Si el jugador està fora de l'abast, indiquem al GameState que l'alerta disminueix, i es reprodueix la música normal del nivell.
                if (!_outOfRange)
                {
                    Debug.Log("Disminueix");
                    GameState.Instance.DecreaseAlert();
                    _outOfRange = true;
                }
            }
            /*CheckDirection();
            _projectileDirection = moveDirection > 0 ? new Vector3(1f, .3f, 0f) : new Vector3(-1f, .3f, 0f);
            Debug.Log("Projectile Direction: " + _projectileDirection);*/
        }

        //Mostra a l'escena un Gizmo amb la distància de detecció i la d'atac.
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanciaDeteccio);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, distanciaAtac);
        }

        //Esdeveniment d'animació que detecta el punt en què impacta amb el jugador, per fer-li perdre vida.
        public void OnAnimationConnect()
        {
            var health = _player.GetComponent<Health>();
            health.Decrement(false);
        }

        //Esdeveniment d'animació que detecta el punt en què hauria de llençar el projectil, i crea la instància d'aquest
        public void OnAnimationThrow()
        {
            Vector2 spawnPosition = transform.position;
            projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);
            CheckDirection();
            _projectileDirection = moveDirection > 0 ? new Vector3(1f, .3f, 0f) : new Vector3(-1f, .3f, 0f);
        }

        //Mètode que comprova la direcció en la que mira l'enemic, per tal de fer que el projectil creat vagi en aquesta direcció
        private void CheckDirection()
        {
            //Vector3 forwardDirection = transform.right;

            if (control.spriteRenderer.flipX == false)
            {
                Debug.Log("Dreta");
                moveDirection = 1;
            }
            else if (control.spriteRenderer.flipX == true)
            {
                Debug.Log("Esquerra");
                moveDirection = -1;
            }
            else
            {
                Debug.Log("cap");
            }
        }

        /*Mètode que gestiona la mort de l'enemic. Com que l'esdeveniment EnemyDeath del template no funciona del tot correctament, i en alguns
         casos desprèn un error de referència nul.la, s'opta per utilitzar aquest mètode. Reproduim una animació i desactivem l'AnimationController
        per, després d'1,5 segons Invocar el mètode que destrueix el gameObject de l'enemic.*/
        public void EnemyDeath()
        {
            _isDead = true;
            Debug.Log("Enemic mort");
            _animator.SetTrigger("death");
            _rigidbody.simulated = false;
            _collider.enabled = false;
            control.enabled = false;
            Invoke("DestroyBody",1.5f);
        }

        void DestroyBody()
        {
            Destroy(gameObject);
            Debug.Log("Disminueix");
            GameState.Instance.DecreaseAlert();
            _outOfRange = true;
        }

    }
}