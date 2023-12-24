using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    
    // Es fa la variable com a static. Així es pot utilitzar en un altre Script
    public static bool isGrounded;
    public static bool onWater;

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            if(collision.CompareTag("PiscinaAigua"))
            {
                onWater=true;
            }
            else
            {
                onWater = false;
            }

            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
           /* if (collision.CompareTag("PiscinaAigua"))
            {
                onWater = true;
            }
            else
            {
                onWater = false;
            }*/

            isGrounded = false;
        }
    }
}
