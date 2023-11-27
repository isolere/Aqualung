using System;
using UnityEngine;

public class Eines : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject itemInventoryObject;
    [SerializeField] private Inventory _inventory;

    private void OnTriggerEnter2D()
    {
        _inventory.Add(itemName);
        itemInventoryObject.SetActive(true);
        Destroy(gameObject);
    }
}