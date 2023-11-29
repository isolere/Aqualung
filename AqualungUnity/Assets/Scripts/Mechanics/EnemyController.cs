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
        private Transform _player;
        private Animator _animator;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
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
            float distanceFromPlayer = Vector2.Distance(_player.position, transform.position);

            if (path != null)
            {
                if (mover == null) mover = path.CreateMover(control.maxSpeed * 0.5f);
                control.move.x = Mathf.Clamp(mover.Position.x - transform.position.x, -1, 1);
            }

            if (distanceFromPlayer < distanciaDeteccio)
            {
                //transform.position = Vector2.MoveTowards(this.transform.position, _player.position, speed * Time.deltaTime);
                control.move.x = Mathf.Clamp(_player.position.x - transform.position.x, -1, 1);
            }

            if (distanceFromPlayer < distanciaAtac)
            {
                _animator.SetTrigger("Attacking");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanciaDeteccio);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, distanciaAtac);
        }
    }
}