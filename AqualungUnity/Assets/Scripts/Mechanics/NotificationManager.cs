using System;
using TMPro;
using UnityEngine;

/**
 * Gestor de notificacions implementat com un singleton simplificat sense comprovació ni persistencia que
 * permet afegir notificacions fàcilment des de qualsevol altre classe.
 *
 * Com que només es mostren durant el joc es requereix que estigui lligat a un component UI Display
 */
[RequireComponent(typeof(UIDisplay))]
public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private Animation notificationAnimation;

    private static NotificationManager _instance;

    public static NotificationManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void ShowNotification(String notification)
    {
        notificationText.text = notification;
        notificationAnimation.Play();
    }
}