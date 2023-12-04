using UnityEngine;

/**
 * Component que defineix la condició de victoria per aquest joc. Quan el jugador entra dins del vólum
 * el nivell es completa.
 */
[RequireComponent(typeof(Collider2D))]
public class VictoryZonee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Evitem que es dispari més d'una vegada
            GetComponent<Collider2D>().enabled = false;
            AudioManager.Instance.StopTrack();
            AudioManager.Instance.PlayVictoryClip();

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            levelManager.EndLevel();
        }
    }
}