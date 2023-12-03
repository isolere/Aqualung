using System;
using UnityEngine;
using Platformer.Mechanics;


public class FragAqualung : MonoBehaviour
{
    private GameObject _player;
    private Health _health;
    private UIDisplay _uiDisplay;

    void Awake()
    {
        _player= GameObject.FindGameObjectWithTag("Player");
        if( _player != null )
        {
            _health = _player.GetComponent<Health>();
        }
        else
        {
            Debug.Log("No s'ha trobat el jugador");
        }

        _uiDisplay= FindObjectOfType<UIDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.Instance.FragmentsAqualung += 1;
            _health.maxHP += 2;
            _health.setCurrentHP = _health.maxHP;
            _uiDisplay.SetHealthBar();
            Destroy(gameObject);
        }
    }
}