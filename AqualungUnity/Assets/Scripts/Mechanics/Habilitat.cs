using System;
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    public class Habilitat : MonoBehaviour
    {
        //[SerializeField] private GameObject impactPrefab;
        //[SerializeField] private AudioClip impactSound;

        [SerializeField] protected GameObject itemPrefab;
        protected GameObject item;
        [SerializeField] private float itemCooldown = 1.0f;
        private float coolDown;
        protected bool canUse = true;

        protected Health _health;
        protected Inventory _inventory;


        private void Awake()
        {
            _health = GetComponent<Health>();
            coolDown = itemCooldown;
            _inventory = FindObjectOfType<Inventory>();

        }

        public virtual void UtilitzarHabilitat()
        {
            if (canUse && checkWaterReserve()==true)
            {
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement();
                }
                item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }

        protected bool checkWaterReserve()
        {
            if (_health.getCurrentHP <= _health.reservaAigua)
            {
                NotificationManager.Instance.ShowNotification("Reserva d'aigua insuficient");
                return false;
            }
            else return true;
        }

        private void Update()
        {
            if (!canUse)
            {
                coolDown -= Time.deltaTime;
                if (coolDown <= 0)
                {
                    canUse = true;
                    coolDown = itemCooldown;
                }
            }
        }
    }
}