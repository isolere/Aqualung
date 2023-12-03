using System;
using UnityEngine;

/**
 * ScriptableObject pare de la jerarquia de requisits
 */
abstract public class RequirementSO : ScriptableObject
{
    abstract public bool Validate(GameObject gameobject);

    virtual public String GetErrorMessage()
    {
        return "No s'acompleix el requisit";
    }
}