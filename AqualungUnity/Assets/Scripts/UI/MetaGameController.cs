using Platformer.Mechanics;
using Platformer.UI;
using UnityEngine;
using System.Collections;

namespace Platformer.UI
{
    /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        /// <summary>
        /// The main UI object which used for the menu.
        /// </summary>
        public MainUIController mainMenu;

        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        /// </summary>
        public Canvas[] gamePlayCanvasii;

        /// <summary>
        /// The game controller.
        /// </summary>
        public GameController gameController;

        bool showMainCanvas = false;

        private GameObject _player;
        private PlayerController _playerController;

        public Transform desti;
        public float speed = 2f;

        void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _playerController = _player.GetComponent<PlayerController>();
        }

        private void Start()
        {
            //Time.timeScale = 0;
            _playerController.enabled = false;
            IntroNivell();
        }

        void OnEnable()
        {
            _ToggleMainMenu(showMainCanvas);
        }

        /// <summary>
        /// Turn the main menu on or off.
        /// </summary>
        /// <param name="show"></param>
        public void ToggleMainMenu(bool show)
        {
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        void _ToggleMainMenu(bool show)
        {
            if (show)
            {
                Time.timeScale = 0;
                mainMenu.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
                _playerController.enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
                _playerController.enabled = true;

            }
            this.showMainCanvas = show;
        }

        void Update()
        {
            if (Input.GetButtonDown("Menu"))
            {
                ToggleMainMenu(show: !showMainCanvas);
            }
        }

        void IntroNivell()
        {
            StartCoroutine(Desplacament());
        }

        IEnumerator Desplacament()
        {
            while (_player.transform.position != desti.position)
            {
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, desti.position, speed * Time.deltaTime);
                yield return null;
            }
            _playerController.enabled = true;
        }
    }
}
