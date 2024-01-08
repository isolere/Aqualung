using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class CombatBenzo : MonoBehaviour
{

    private Collider2D _colliderInici;
    private GameObject _player;
    private PlayerController _playerController;
    private Health _benzoHealth;

    [SerializeField] private AudioClip _musicaBenzo;
    [SerializeField] private GameObject _healthBarBenzo;
    [SerializeField] private GameObject _benzo;

    // Start is called before the first frame update
    void Start()
    {
        _colliderInici = GetComponent<Collider2D>();
        _colliderInici.enabled = true;

        _player = GameObject.FindWithTag("Player");

        _benzoHealth = _benzo.GetComponent<Health>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject == _player)
        {
            _colliderInici.enabled = false;
            AudioManager.Instance.SetCombatTrack(_musicaBenzo);
            _healthBarBenzo.SetActive(true);
        }

    }

    void Update()
    {
        if (_benzoHealth.getCurrentHP <= 0)
        {
            Invoke("AcabaCombat",3f);
        }
    }

    private void AcabaCombat()
    {
        AudioManager.Instance.StopTrack();
        AudioManager.Instance.PlayVictoryClip();

        _playerController = _player.GetComponent<PlayerController>();
        _playerController.enabled = false;

        //Localitzem el LevelManager i carreguem el mètode que finalitza el nivell
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.EndLevel();
    }
}
