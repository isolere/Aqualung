using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    /// 
    /*Aquesta classe prov� del template del projecte base utilitzat, per� hi hem realitzat algunes modificacions
     per adaptar-la a les necessitats del nostre projecte. Hem incorporat la reserva d'aigua, i la possibilitat 
    d'incrementar Health en m�s d'un punt.Aquesta classe �s utilitzada tant pel jugador com pels enemics, i es 
    combina amb diversos esdeveniments, que comproven si el gameObject afectat �s el jugador o un enemic.*/
    public class Health : MonoBehaviour
    {
        public delegate void OnHealthChangeDelegate(int amount);

        public event OnHealthChangeDelegate OnHealthChanged;

        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;
        public int reservaAigua = 4;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        private int currentHP;

        public int getCurrentHP
        {
            get{ return currentHP; }
        }
        public int setCurrentHP
        {
            set { currentHP = value; }
        }

        private bool _isAlreadyDead=false;

        private Animator _animator;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            if (OnHealthChanged != null) OnHealthChanged(1);
        }

        /*Aquest m�tode permet incrementar la vida del jugador en un valor concret, que podem establir. Ser� utilitzat
         per les Fonts per restablir de cop diversos punts de vida del jugador.*/
        public void IncrementMultiple(int value)
        {
            currentHP = Mathf.Clamp(currentHP + value, 0, maxHP);
            if (OnHealthChanged != null) OnHealthChanged(value);
        }
        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        /// 
        /*Aquest m�tode disminueix la vida del jugador en 1 punt. Comprovem primer que el jugador no estigui ja mort, per
         evitar que es resti m�s vida, i aquesta no sigui correcta en cas de respawn. Si la vida del jugador �s zero, cridem 
        l'esdeveniment HealthIsZero, que comprovar� si es tracta del jugador o un enemic, i procedir� a executar la mort del
        element concret.*/
        public void Decrement(bool habilitat)
        {
            if (_isAlreadyDead == false)
            {
                currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
                if (habilitat == false)
                {
                    _animator.SetTrigger("hurt");
                }
                if (currentHP == 0)
                {
                    _isAlreadyDead = true;
                    var ev = Schedule<HealthIsZero>();
                    ev.health = this;
                }
                if (OnHealthChanged != null) OnHealthChanged(-1);
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        /// 
        //Permet "matar" un gameObject, independentment de la quantitat de vida de la que disposi.
        public void Die()
        {
            while (currentHP > 0) Decrement(false);
        }

        //Restablim la vida fins al seu valor m�xim
        public void RestoreLife()
        {
            currentHP = maxHP;
            _isAlreadyDead=false;
        }

        void Start()
        {
            /*Al comen�ar cada escena establim la vida al seu valor m�xim. Comprovem si el GameState cont�
            fragments d'Aqualung, i afegim 2 punts a la vida m�xima per cada fragment obtingut.*/
            currentHP = maxHP+(GameState.Instance.FragmentsAqualung * 2);
            _animator = GetComponent<Animator>();
        }
    }
}
