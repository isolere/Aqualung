 using System.Collections.Generic;
using UnityEngine;

/**
 * Component auxiliar per gestionar quan entra o surt un adversary (segons la configuració del layer) del seu
 * àrea d'influencia, que estarà lligada a un collider en el mateix GameObject.
 */
public class TestTrigger : MonoBehaviour
{
    public delegate void OnEventTargetDelegate(GameObject target);

    public event OnEventTargetDelegate OnTargetEnter;
    public event OnEventTargetDelegate OnTargetExit;

    private List<GameObject> _targets = new List<GameObject>();

    public GameObject[] GetTargets()
    {
        return _targets.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnTargetEnter != null) OnTargetEnter(other.gameObject);
        _targets.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (OnTargetExit != null) OnTargetExit(other.gameObject);
        _targets.Remove(other.gameObject);
    }

    public void RemoveTarget(GameObject target)
    {
        _targets.Remove(target);
    }
}