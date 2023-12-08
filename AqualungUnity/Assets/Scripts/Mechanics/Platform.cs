using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /*Classe aplicada als prefabs de les plataformes d'aigua creades per la Nixie. Establim una durada, després
     de la qual el prefab es destruirà, eliminant la plataforma creada.*/
    public class Platform : MonoBehaviour
    {
        [SerializeField] private float duration=5.0f;
        private GameObject _particleObject;
        private ParticleSystem _particules;

        private float remainingTime;

        private void Start()
        {
            Debug.Log("Plataforma creada");

            //Establim el temps restant a la durada configurada per la plataforma
            remainingTime = duration;

            //Busquem el sistema de partícules que es reproduirà quan la plataforma s'elimini.
            _particleObject = GameObject.FindWithTag("ParticulesPlataforma");
            _particules = _particleObject.GetComponent<ParticleSystem>();
        }

        /*Al mètode Update anirem disminuint el valor de remainingTime fins que aquest arribi a zero. En aquest
         moment situarem el sistema de partícules a la posició de la plataforma i el reproduirem. Tot seguit eliminarem
        el gameObject corresponent a la plataforma en qüestió.*/
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