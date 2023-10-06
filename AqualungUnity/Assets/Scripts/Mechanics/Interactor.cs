using UnityEngine;

/**
 * Component pels elements que poden interactuar amb altres (el jugador).
 *
 * ALERTA! Aquest component ha d'anar en un fill de l'arrel, no pot afegir-se directament perquè requereix
 * un component de tipus collider propi.
 */
[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    private Interactable selected;

    public void Interact()
    {
        if (selected != null)
        {
            bool success = selected.Interact(gameObject);
            if (success)
            {
                Unselect(selected.gameObject);
            }
        }
    }

    private void Unselect(GameObject target)
    {
        if (selected.transform.root == target.transform.root)
        {
            selected.Unselect(gameObject);
            selected = null;
        }
    }

    private void Select(GameObject target)
    {
        if (!selected || target.transform.root != selected.transform.root)
        {
            Interactable candidate = GetInteractable(target.gameObject);

            // Aquest collider només ha de colisionar amb els interactables, però ens assegurem
            if (candidate != null)
            {
                if (selected != null)
                {
                    selected.Unselect(gameObject);
                }

                selected = candidate;
                candidate.Select(gameObject);
            }
            else
            {
                Debug.LogWarning(
                    "Alerta! s'ha detectat un objecte en el layer d'interaccions sense component interactable: " +
                    target);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Unselect(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Select(other.gameObject);
    }

    Interactable GetInteractable(GameObject source)
    {
        Interactable interactable = null;

        interactable = source.GetComponent<Interactable>();

        // Si no és interactable, comprovem si es troba al pare (és l'habitual)
        if (interactable == null)
        {
            interactable = source.GetComponentInParent<Interactable>();
        }

        return interactable;
    }
}