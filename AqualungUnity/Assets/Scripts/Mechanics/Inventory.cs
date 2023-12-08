using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Component per afegir un sistema d'inventari simple basat en cadenes de text. Permet afegir,
 * eliminar i comprovar si un element es troba a l'inventari.
 */
/*Aquesta classe s'ha obtingut del projecte Escape from IOC.*/
public class Inventory : MonoBehaviour
{
    public delegate void OnEventInventoryDelegate(String item);

    public event OnEventInventoryDelegate OnAddItem;
    public event OnEventInventoryDelegate OnRemoveItem;


    [SerializeField] private List<String> items;

    public void Add(String item)
    {
        if (OnAddItem != null) OnAddItem(item);

        //Notifiquem a través de la UI que s'ha afegit l'ítem a l'inventari, i l'afegim a la llista
        NotificationManager.Instance.ShowNotification($"Afegit <{item}> a l'inventari");
        items.Add(item);
    }
    /*Aquest mètode ens permetrà saber si tenim un ítem concret a l'inventari. Ens pot servir per exemple per
    saber si tenim l'amulet, i evitar gastar reserva d'aigua a l'utilitzar habilitats.*/
    public bool Has(String item)
    {
        return items.Contains(item);
    }
}