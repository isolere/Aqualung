using System;
using Cinemachine;
using UnityEngine;

namespace Platformer.Mechanics
{
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
            if (canUse && checkWaterReserve() == true)
            {
                _animator.SetTrigger("Projectil");
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement();
                }
                Invoke("InstantiateItem",0.2f);
                playerDirection = PlayerController.moveDirection;
                projectileDirection = playerDirection > 0 ? new Vector3(1f,.3f,0f) : new Vector3(-1f,.3f,0f);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }

        void InstantiateItem()
        {
            Vector2 spawnPosition = _projectilePosition.position;
            item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}