using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component per afegir un sistema d'inventari simple basat en cadenes de text. Permet afegir,
 * eliminar i comprovar si un element es troba a l'inventari.
 */
public class Inventory : MonoBehaviour
{
    public delegate void OnEventInventoryDelegate(String item);

    public event OnEventInventoryDelegate OnAddItem;
    public event OnEventInventoryDelegate OnRemoveItem;


    [SerializeField] private List<String> items;

    public void Add(String item)
    {
        if (OnAddItem != null) OnAddItem(item);
        NotificationManager.Instance.ShowNotification($"Afegit <{item}> a l'inventari");
        items.Add(item);
    }

    public bool Has(String item)
    {
        return items.Contains(item);
    }
}