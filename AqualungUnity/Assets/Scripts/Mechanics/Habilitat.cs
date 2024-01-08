using System;
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    /*Classe principal que gestiona les habilitats del personatge. En ella comprobarem si es pot utilitzar una habilitat,
    revisant la reserva d'aigua del personatge, i també assegurant-nos que el cooldown per realitzar de nou l'habilitat 
    s'hagi complert. El mètode UtilitzarHabilitat és genèric, i serà sobreescrit en cada subclasse, corresponent a les 
    diferents habilitats.*/
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
        protected Animator _animator;


        private void Awake()
        {
            _health = GetComponent<Health>();
            _animator = GetComponent<Animator>();
            coolDown = itemCooldown;
            _inventory = FindObjectOfType<Inventory>();
        }

        /*Mètode virtual, que serà sobreescrit a les subclasses. Podem veure, però l'estructura bàsica, on es comprova que
        el cooldown s'hagi completat, i la reserva d'aigua sigui suficient. També es comprovarà si el personatge té al seu
        inventari l'Amulet, que li permetria realitzar l'Habilitat sense gastar reserva d'aigua.*/
        public virtual void UtilitzarHabilitat()
        {
            if (canUse && checkWaterReserve()==true)
            {
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement(true);
                }
                item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }

        //Mètode que comprova si el nivell d'aigua de la Nixie es troba per sobre de la seva reserva.
        protected bool checkWaterReserve()
        {
            if (_health.getCurrentHP <= _health.reservaAigua && _inventory.Has("Amulet") == false)
            {
                NotificationManager.Instance.ShowNotification("Reserva d'aigua insuficient");
                return false;
            }
            else return true;
        }

        /*Cada frame dins el mètode Update restarem una fracció de temps al cooldown fins que aquest sigui zero, i canvii el valor
        del booleà que controla l'ús de l'Habilitat.*/
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