using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Requirements/Palanca", fileName = "New Requirement Palanca")]
public class RequirementPalancaSO : RequirementSO
{
    [SerializeField] private int _canonadesOK;
    private ReparaCanonades _reparaCanonades;
    private GameObject _fuites;

    public override bool Validate(GameObject gameobject)
    {
        _fuites = GameObject.FindWithTag("Fuites");
        _reparaCanonades = _fuites.GetComponent<ReparaCanonades>();

        if(_reparaCanonades.CanonadesReparades>=_canonadesOK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string GetErrorMessage()
    {
        return $"Ncessites reparar {_canonadesOK} canonades per poder activar el mecanisme! ({_reparaCanonades.CanonadesReparades} de {_canonadesOK})";
    }
}