using UnityEngine;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;

/**
 * Component que defineix la condició de victoria per aquest joc. Quan el jugador entra dins del vólum
 * el nivell es completa.
 */
[RequireComponent(typeof(Collider2D))]
public class VictoryZone : MonoBehaviour
{
    private float _speed = 2f;
    private GameObject _player;
    private PlayerController _playerController;
    private Animator _animator;
    public Transform desti;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Evitem que es dispari més d'una vegada
            GetComponent<Collider2D>().enabled = false;
            AudioManager.Instance.StopTrack();
            AudioManager.Instance.PlayVictoryClip();
            _player = other.gameObject;
            _playerController= _player.GetComponent<PlayerController>();
            _playerController.enabled = false;
            _animator= _player.GetComponent<Animator>();

            StartCoroutine(Desplacament());

            //Localitzem el LevelManager i carreguem el mètode que finalitza el nivell
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.EndLevel();
        }
    }

    IEnumerator Desplacament()
    {
        _speed = 2f;
        _animator.SetBool("grounded", true);
        _animator.SetFloat("velocityX", 1f);

        while (_player.transform.position != desti.position)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, desti.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}