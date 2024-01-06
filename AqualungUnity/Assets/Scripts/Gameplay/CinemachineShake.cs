using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Random = UnityEngine.Random;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    // Singleton no persistent simplificat, no comprovem si s'ha instanciat previament
    private static CinemachineShake _instance;

    public static CinemachineShake Instance
    {
        get { return _instance; }
    }

    private float shakeTimer;


    private void Awake()
    {
        _cinemachineVirtualCamera =
            FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _instance = this;
    }

    public void Shake(float intensity, float time)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}