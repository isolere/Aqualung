using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBenzo : MonoBehaviour
{

    private Collider2D _colliderInici;
    private GameObject _player;

    [SerializeField] private AudioClip _musicaBenzo;
    [SerializeField] private GameObject _healthBarBenzo;

    // Start is called before the first frame update
    void Start()
    {
        _colliderInici = GetComponent<Collider2D>();
        _colliderInici.enabled = true;

        _player = GameObject.FindWithTag("Player");
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
}
