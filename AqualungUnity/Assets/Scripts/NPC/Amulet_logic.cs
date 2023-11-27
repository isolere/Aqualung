using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amulet_logic : MonoBehaviour
{
    public NPC_logic npc_logic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            npc_logic.textoMission.text = "Agafa l'amulet per salvar l'Aqualung"; 

            
            Destroy(gameObject);
            npc_logic.textoMission.text = "Has agafat l'amulet";

        }


    }


    
}
