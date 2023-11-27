using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_logic : MonoBehaviour
{
    public GameObject panel_NPC_1;
    public GameObject panel_NPC_2;
    public GameObject panel_NPC_3;

    public TextMeshProUGUI textoMission;

    public GameObject player;
    public GameObject amuletObjecte;

    public bool jugadorCerca;
    public bool aceptarMission;




    
    void Start()
    {
        

        textoMission.text = "Cal tenir l'amulet";
        player = GameObject.FindGameObjectWithTag("Player");
        panel_NPC_1.SetActive(false);
        amuletObjecte.SetActive(false);

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && aceptarMission == false)
        {
            // Un cop que el Player detectiu el collider del NPC té que mirar-lo
            //Vector2 playerPosition = new Vector2(player.gameObject.transform.position.x, transform.position.y);
            //player.gameObject.transform.LookAt(playerPosition);


            // Activar i desactivar els diferents panels
            panel_NPC_1.SetActive(false);
            panel_NPC_2.SetActive(true);
        }
    }

    // Funció per detectar el collider quan el Player està a prop del NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            jugadorCerca = true;

            if (aceptarMission == false)
            {
                panel_NPC_1.SetActive(true);

            }
        }
    }

    //Funció per detectar quan surt el Player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            jugadorCerca = false;

            // Camvi de codi
            panel_NPC_1.SetActive(false);
            panel_NPC_2.SetActive(false);
        }
    }

    // Funció per quan s'agafa opció NO
    public void NO()
    {
        panel_NPC_2.SetActive(false);
        panel_NPC_1.SetActive(true);
    }

    // Funció per quan s'agafa opció SI
    public void SI()
    {
        aceptarMission = true;

        amuletObjecte.SetActive(true);


        jugadorCerca = false;

        panel_NPC_1.SetActive(false);
        panel_NPC_2.SetActive(false);
        panel_NPC_3.SetActive(true);

        //Desactivar el missatge després de 3 segons
       StartCoroutine(DeactivateMessage(3.0f));

    }


    // Desactivar planel 3
    private IEnumerator DeactivateMessage(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        panel_NPC_3.SetActive(false);
    }

}
