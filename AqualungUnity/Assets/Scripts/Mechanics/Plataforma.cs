using System;
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

namespace Platformer.Mechanics
{
    /*Aquesta subclasse de la Classe Habilitat gestiona la generació de les plataformes d'aigua que crea la Nixie.
    Només hem de tenir en compte el mètode UtilitzarHabilitat, que serà sobreescrit respecte al de la classe pare.
   La resta de mètodes seran heretats d'aquesta.*/
    public class Plataforma : Habilitat
    {
        public override void UtilitzarHabilitat()
        {
            //Ens assegurem que podem utilitzar l'habilitat, i que hi ha prou reserva d'aigua
            if (canUse && checkWaterReserve() == true)
            {
                //Activem el trigger de l'animació dins l'Animator de la Nixie
                _animator.SetTrigger("Plataforma");

                //Si l'inventari de la Nixie conté un objecte anomenat "Amulet" no perdrem reserva d'aigua
                if (_inventory.Has("Amulet") == false)
                {
                    _health.Decrement();
                }

                /*Invoquem el mètode InstantiateItem, amb un delay de 0,3 segons. D'aquesta manera donem temps
                a l'animació a reproduir-se.*/
                Invoke("InstantiateItem", 0.3f);
                canUse = false;
                Debug.Log("Vida= " + _health.getCurrentHP);
            }
        }
        /*Aquest mètode localitza la posició del cursor del ratolí un cop s'ha clicat per generar la plataforma,
         i situa aquesta posició dins el nivell de joc, tenint en compte la relació entre aquest i l'enquadrament
        de la càmera. Un cop obtinguda la posició correcta, s'instancia la plataforma.*/
        void InstantiateItem()
        {
            Vector3 cursorPosition = Input.mousePosition;
            cursorPosition.z = 1.0f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            item = Instantiate(itemPrefab, worldPosition, Quaternion.identity);
        }
    }
}