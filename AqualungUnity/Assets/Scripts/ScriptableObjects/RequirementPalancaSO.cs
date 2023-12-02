using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Requirements/Palanca", fileName = "New Requirement Palanca")]
public class RequirementPalancaSO : RequirementSO
{
    [SerializeField] private int _canonadesOK;

    public override bool Validate(GameObject gameobject)
    {
        if(GameState.Instance.CanonadesReparades>=_canonadesOK)
        {
            Debug.Log("Es compleix");
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string GetErrorMessage()
    {
        return $"Ncessites reparar {_canonadesOK} canonades per poder activar el mecanisme!";
    }
}