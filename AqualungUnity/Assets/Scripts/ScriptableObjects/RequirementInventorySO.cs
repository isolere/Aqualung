using System;
using UnityEngine;

/**
 * Requisit que espera el caràcter tingui un inventari i a l'inventari es trobi
 * l'objecte amb el nom ItemName.
 */
[CreateAssetMenu(menuName = "Requirements/Inventory", fileName = "New Requirement Inventory")]
public class RequirementInventorySO : RequirementSO
{
    [SerializeField] private String ItemName;

    public override bool Validate(GameObject gameobject)
    {
        Inventory inventory = gameobject.GetComponentInParent<Inventory>();

        if (!inventory)
        {
            return false;
        }

        if (inventory.Has(ItemName))
        {
            return true;
        }

        return false;
    }

    public override string GetErrorMessage()
    {
        return $"Es requereix <{ItemName}>!!";
    }
}