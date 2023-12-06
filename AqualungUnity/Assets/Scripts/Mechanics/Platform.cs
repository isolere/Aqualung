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
        private GameObject _particleObject;
        private ParticleSystem _particules;

        private float remainingTime;

        private void Start()
        {
            Debug.Log("Plataforma creada");
            remainingTime = duration;
            _particleObject = GameObject.FindWithTag("ParticulesPlataforma");
            _particules = _particleObject.GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                _particules.transform.position = transform.position;
                _particules.Play();
                Destroy(this.gameObject);
            }
        }
    }
}