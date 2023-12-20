using System;
using Cinemachine;
using UnityEngine;

namespace Platformer.Mechanics
{
    /*Aquesta subclasse de la Classe Habilitat gestiona la generació dels projectils d'aigua que llença la Nixie.
     Només hem de tenir en compte el mètode UtilitzarHabilitat, que serà sobreescrit respecte al de la classe pare.
    La resta de mètodes seran heretats d'aquesta.*/
    public class ProjectilAigua : Habilitat
    {
        private int playerDirection;

        private Vector3 projectileDirection;
        public Vector3 ProjectileDirection
        {
            get { return projectileDirection; }
        }
        [SerializeField] private Transform _projectilePosition;

        public override void UtilitzarHabilitat()
        {
            //Ens assegurem que podem utilitzar l'habilitat, i que hi ha prou reserva d'aigua
            if (canUse && checkWaterReserve() == true)
            {
                //Activem el trigger de l'animació dins l'Animator de la Nixie
                _animator.SetTrigger("Projectil");

                //Si l'inventari de la Nixie conté un objecte anomenat "Amulet" no perdrem reserva d'aigua
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement(true);
                }

                /*Invoquem el mètode InstantiateItem, amb un delay de 0,2 segons. D'aquesta manera donem temps
                a l'animació a reproduir-se.*/
                Invoke("InstantiateItem",0.2f);

                //Obtenim la direcció del jugador per poder-la traspassar al projectil instanciat.
                playerDirection = PlayerController.moveDirection;
                projectileDirection = playerDirection > 0 ? new Vector3(1f,.1f,0f) : new Vector3(-1f,.1f,0f);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }
        //Aquest mètode obté la posició des de la que s'ha d'instanciar el projectil i crea una nova instància d'aquest.
        void InstantiateItem()
        {
            Vector2 spawnPosition = _projectilePosition.position;
            item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}