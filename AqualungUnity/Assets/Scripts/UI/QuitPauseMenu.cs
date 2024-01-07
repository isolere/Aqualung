using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPauseMenu : MonoBehaviour
{
    public GameObject confirmationPauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadMenu()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        SceneManager.LoadScene("MainMenu");
    }

}
