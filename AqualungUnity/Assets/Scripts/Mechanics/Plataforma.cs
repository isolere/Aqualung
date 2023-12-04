using System;
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    public class Plataforma : Habilitat
    {
        public override void UtilitzarHabilitat()
        {
            if (canUse && checkWaterReserve() == true)
            {
                _animator.SetTrigger("Plataforma");
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement();
                }
                Vector3 cursorPosition = Input.mousePosition;
                cursorPosition.z = 1.0f;
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
                item = Instantiate(itemPrefab, worldPosition, Quaternion.identity);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }
    }
}