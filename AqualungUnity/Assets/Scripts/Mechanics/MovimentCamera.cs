using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Cinemachine;


public class MovimentCamera : MonoBehaviour
{
    private GameObject _player;
    private PlayerController _playerController;
    private Camera _mainCamera;
    private Vector3 _camInitPosition;
    private float speed = 2f;


    public Transform destiCamera;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _mainCamera = Camera.main;
        _playerController = _player.GetComponent<PlayerController>();
    }

    public void Executar()
    {
        _playerController.enabled = false;
        StartCoroutine(MouCamera());
    }

    IEnumerator MouCamera()
    {
        _camInitPosition = _mainCamera.transform.position;
        speed = 10f;
        _mainCamera.GetComponent<CinemachineBrain>().enabled = false;

        while (_mainCamera.transform.position != destiCamera.position)
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, destiCamera.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (_mainCamera.transform.position != _camInitPosition)
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _camInitPosition, speed * Time.deltaTime);
            yield return null;
        }
        _mainCamera.GetComponent<CinemachineBrain>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        _playerController.enabled = true;
    }
}
