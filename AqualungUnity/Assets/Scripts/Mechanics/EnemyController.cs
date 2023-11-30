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

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _animator = GetComponent<Animator>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
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

            if (distanceFromPlayer < distanciaDeteccio)
            {
                if (distanceFromPlayer > distanciaAtac)
                {
                    control.move.x = Mathf.Clamp(_player.transform.position.x - transform.position.x, -1, 1);
                }
                else
                {
                    control.move.x = 0;
                }
            }

            if (distanceFromPlayer <= distanciaAtac && Time.time - _lastAttack > _cooldown)
            {
                _animator.SetTrigger("Attacking");
                _lastAttack = Time.time;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanciaDeteccio);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, distanciaAtac);
        }

        public void OnAnimationConnect()
        {
            var health = _player.GetComponent<Health>();
            health.Decrement();
        }

        public void OnAnimationThrow()
        {
            Vector2 spawnPosition = transform.position;
            projectile = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity);
            CheckDirection();
            _projectileDirection = moveDirection > 0 ? new Vector3(1f, .3f, 0f) : new Vector3(-1f, .3f, 0f);
        }

        private void CheckDirection()
        {
            Vector3 forwardDirection = transform.forward;

            if (forwardDirection.x > 0)
            {
                moveDirection = 1;
            }
            else if (forwardDirection.x < 0)
            {
                moveDirection = -1;
            }
        }

    }
}