using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Font : MonoBehaviour
    {
        [SerializeField] private int quantitatAigua=3;

        private Health _health;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _health = player.GetComponent<Health>();
        }

        public void IncrementaReservaAigua()
        {
            _health.IncrementMultiple(quantitatAigua);
        }

        private void Update()
        {
           
        }
    }
}