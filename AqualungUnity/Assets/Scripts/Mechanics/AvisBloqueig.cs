using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisBloqueig : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NotificationManager.Instance.ShowNotification("Has de restablir el subministrament d'aigua per poder passar!");
        }
    }

}
