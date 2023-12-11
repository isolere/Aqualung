using System;
using UnityEngine;


/*Classe que incorporem a tots els pickups utilitzats en les missions. A partir de la col.lisió amb el jugador s'incorpora
 l'element al seu inventari, i s'activa a la UI la icona de l'ítem, per indicar que el jugador disposa d'ell. Per acabar, 
destruim el gameObject.*/
public class Eines : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject itemInventoryObject;
    [SerializeField] private AudioClip _audioClip;

    private Inventory _inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayClip(_audioClip);
            _inventory = other.GetComponent<Inventory>();
            _inventory.Add(itemName);
            itemInventoryObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}