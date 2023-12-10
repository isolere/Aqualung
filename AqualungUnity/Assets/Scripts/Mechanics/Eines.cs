using System;
using UnityEngine;

public class Eines : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject itemInventoryObject;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private AudioClip _audioClip;

    private void OnTriggerEnter2D()
    {
        AudioManager.Instance.PlayClip(_audioClip);
        _inventory.Add(itemName);
        itemInventoryObject.SetActive(true);
        Destroy(gameObject);
    }
}