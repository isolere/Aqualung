using UnityEngine;

/**
 * Component pels elements que poden interactuar amb altres (el jugador).
 *
 * ALERTA! Aquest component ha d'anar en un fill de l'arrel, no pot afegir-se directament perquè requereix
 * un component de tipus collider propi.
 */
/*Aquest mètode s'ha extret del projecte Escape From IOC. Genera un error de NullReference amb el mètode Unselect, que no hem
 sapigut solucionar.*/
[RequireComponent(typeof(Collider2D))]
public class Interactor : MonoBehaviour
{
    private Interactable selected;

    /*Què succeeix quan l'Interactor interactua amb un Interactable. Executem el mètode Interact de l'Interactable. En el nostre
    cas, com que a l'interactuar amb la majoria d'elements desactivem el seu collider, hem eliminat la segona part del codi per
    evitar una doble trucada al mètode Unselect, que aportava un error de referència nul.la.*/
    public void Interact()
    {
        if (selected != null)
        {
            bool success = selected.Interact(gameObject);
            Debug.Log("Metode Interact.Selected = " + selected);

           /* if (success)
            {
                Unselect(selected.gameObject);
            }*/
        }
    }

    //Què succeeix quan l'Interactor s'allunya de la influència de l'Interactable.
    private void Unselect(GameObject target)
    {
        if (selected.transform.root == target.transform.root)
        {
            selected.Unselect(gameObject);
            selected = null;
        }
    }

    //Què succeeix quan l'Interactor entra en la influència de l'Interactable.
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

    //Mètode per obtenir l'Interactable d'un objecte
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