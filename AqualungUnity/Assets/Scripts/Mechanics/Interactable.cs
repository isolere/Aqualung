using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Component pels elements amb els que es pot interactuar, per exemple portes i cofres
 */
/*Classe complementària a Interactor. Extreta del projecte Escape From IOC.*/
public class Interactable : MonoBehaviour
{
    public UnityEvent OnSelect;
    public UnityEvent OnUnselect;
    public UnityEvent OnInteract;
    public UnityEvent OnRequirementFail;

    [SerializeField] private List<RequirementSO> requirements;

    //Què succeeix quan un Interactor s'aproxima
    public void Select(GameObject selector)
    {
        Debug.Log("Interactua");
        OnSelect.Invoke();

    }
    
    //Què succeeix quan un Interactor s'allunya
    public void Unselect(GameObject selector)
    {
        OnUnselect.Invoke();
    }

    //Què succeeix quan un Interactor Seleccionat Interactua amb l'Interactable
    public bool Interact(GameObject interactor)
    {
        if (ValidateRequirements(interactor))
         {
             OnInteract.Invoke();
             return true;
         }
         else
         {
             OnRequirementFail.Invoke();
             return false;
         }
        OnInteract.Invoke();
        return true;
    }

    //Mètode que valida si els requeriments es compleixen, i ho notifica
    private bool ValidateRequirements(GameObject gameObject)
    {
        foreach (RequirementSO requirement in requirements)
        {
            if (!requirement.Validate(gameObject))
            {
                NotificationManager.Instance.ShowNotification(requirement.GetErrorMessage());
                return false;
            }
        }

        return true;
    }
}