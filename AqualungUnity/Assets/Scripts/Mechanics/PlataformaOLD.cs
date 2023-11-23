/*using System;
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    public class Plataforma : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        //[SerializeField] private AudioClip impactSound;

        [SerializeField] private GameObject platformPrefab;
        private GameObject platform;
        private List<GameObject> platforms = new List<GameObject>();
        [SerializeField] private float platformCooldown = 1.0f;
        private float coolDown;
        private bool canPlace = true;
        [SerializeField] private float platformDuration = 5.0f;
        private float duration;


        private void Awake()
        {
            coolDown = platformCooldown;
            duration = platformDuration;
        }

        public void Colocar()
        {
            if (canPlace)
            {
                Vector3 cursorPosition = Input.mousePosition;
                cursorPosition.z = 1.0f;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
                platform = Instantiate(platformPrefab, worldPosition, Quaternion.identity);
                platforms.Add(platform);
                canPlace = false;
            }
        }

        private void Update()
        {
            if (!canPlace)
            {
                coolDown -= Time.deltaTime;
                if (coolDown <= 0)
                {
                    canPlace = true;
                    coolDown = platformCooldown;
                }
            }
            foreach (GameObject platform in platforms)
            {
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    Destroy(platform);
                    duration = platformDuration;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Impacte:" + other);

            /*AudioManager.Instance.PlayClip(impactSound);

            GameObject FX = Instantiate(impactPrefab, transform.position, transform.rotation);
            Destroy(FX, 2f);

            Health health = other.gameObject.GetComponent<Health>();

            // Si te salut apliquem el mal
            if (health != null)
            {
                health.TakeDamage(damage);
            }*/
/*
            Destroy(gameObject);
        }
    }
}*/