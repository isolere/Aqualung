using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float duration=5.0f;
        private float remainingTime;

        private void Start()
        {
            Debug.Log("Plataforma creada");
            remainingTime = duration;   
        }

        private void Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}