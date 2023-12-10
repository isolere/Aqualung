using System;
using Cinemachine;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;

/*Classe que gestiona la recompensa derivada de la interacció amb les fonts. El que fa és cridar el mètode
 IncrementMultiple del component Health, i augmenta la vida del jugador en el valor establert. També incrementa
la puntuació dins el GameState per saber la quantitat de fonts que s'han purificat al final de la partida.*/
namespace Platformer.Mechanics
{
    public class Font : MonoBehaviour
    {
        [SerializeField] private int quantitatAigua=3;

        private Health _health;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _health = player.GetComponent<Health>();
        }

        public void IncrementaReservaAigua()
        {
            _health.IncrementMultiple(quantitatAigua);
            GameState.Instance.IncreaseScore(1);
        }
    }
}