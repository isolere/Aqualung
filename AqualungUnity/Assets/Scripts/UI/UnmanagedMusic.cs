using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmanagedMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    void Start()
    {
        AudioManager.Instance.PlayTrack(music);
    }

    
}
