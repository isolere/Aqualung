using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using Cinemachine;


public class IntroEscena : MonoBehaviour
{
    private GameObject _player;
    private PlayerController _playerController;
    private Animator _animator;
    private GameObject _uiCanvas;
    private GameObject _limitPantalla;
    private Camera _mainCamera;
    private Vector3 _camInitPosition;
    private float speed = 2f;


    public Transform desti;
    public Transform destiCamera;
    public GameObject explicacio1;
    public GameObject explicacio2;

    private Transform _uiHealthBar;
    private Transform _uiScore;
    private Transform _uiMission;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _uiCanvas = GameObject.FindWithTag("UI");
        _limitPantalla = GameObject.FindWithTag("LimitPantalla");
        _mainCamera = Camera.main;
        _playerController = _player.GetComponent<PlayerController>();
        _animator = _player.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiHealthBar= _uiCanvas.transform.Find("HealthBarAnim");
        _uiScore= _uiCanvas.transform.Find("Score");
        _uiMission= _uiCanvas.transform.Find("Mission");

        _uiHealthBar.gameObject.SetActive(false);
        _uiScore.gameObject.SetActive(false);
        //_uiMission.gameObject.SetActive(false);

        _limitPantalla.SetActive(false);
        _playerController.enabled = false;
        StartCoroutine(Desplacament());
    }

    IEnumerator Desplacament()
    {
        speed = 2f;
        _animator.SetBool("grounded",true);
        _animator.SetFloat("velocityX", 1f);

        while (_player.transform.position != desti.position)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, desti.position, speed * Time.deltaTime);
            yield return null;
        }
        _animator.SetFloat("velocityX", 0f);

        yield return new WaitForSeconds(1f);

        explicacio1.SetActive(true);
        yield return new WaitForSeconds(7f);
        explicacio1.SetActive(false);

        StartCoroutine(MouCamera());
    }

    IEnumerator MouCamera()
    {
        _camInitPosition = _mainCamera.transform.position;
        speed = 9f;
        _mainCamera.GetComponent<CinemachineBrain>().enabled = false;

        while (_mainCamera.transform.position != destiCamera.position)
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, destiCamera.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        while (_mainCamera.transform.position != _camInitPosition)
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _camInitPosition, speed * Time.deltaTime);
            yield return null;
        }
        _mainCamera.GetComponent<CinemachineBrain>().enabled = true;

        yield return new WaitForSeconds(1f);

        explicacio2.SetActive(true);
        yield return new WaitForSeconds(7f);
        explicacio2.SetActive(false);

        _uiHealthBar.gameObject.SetActive(true);
        _uiScore.gameObject.SetActive(true);

        //_uiMission.gameObject.SetActive(true);
        _uiMission.gameObject.GetComponent<Animation>().Play();
        
        _limitPantalla.SetActive(true);
        _playerController.enabled = true;
    }
}
