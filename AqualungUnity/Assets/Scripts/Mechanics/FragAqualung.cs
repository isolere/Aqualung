using System;
using UnityEngine;
using Platformer.Mechanics;

/*Classe que gestiona la recol.lecci� dels fragment d'Aqualung per part del jugador. Si aquest col.lisiona amb un
 fragment, la seva vida m�xima es veur� incrementada en 2 punts, i a m�s, aquesta ser� restablerta fins al seu nou
valor m�xim. Guardarem a GameState la quantitat de fragments obtinguts per comptar amb el nou valor m�xim de vida
encara que canviem de nivell.*/
public class FragAqualung : MonoBehaviour
{
    private GameObject _player;
    private Health _health;
    private UIDisplay _uiDisplay;
    [SerializeField] private AudioClip _audioClip;

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
            AudioManager.Instance.PlayClip(_audioClip);
            GameState.Instance.FragmentsAqualung += 1;
            _health.maxHP += 2;
            _health.setCurrentHP = _health.maxHP;
            _uiDisplay.SetHealthBar();
            Destroy(gameObject);
        }
    }
}