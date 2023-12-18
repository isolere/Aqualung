using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    
    // Es fa la variable com a static. Així es pot utilitzar en un altre Script
    public static bool isGrounded;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        EnemyController enemyController = GetComponent<EnemyController>();

        // comprovar si el gameObject amb el qual col·lisiona té el tag "Enemic"
        if (collision.gameObject.CompareTag("Enemic"))
        {

            //Verificar si l'enemic té vida
            if (health != null && enemyController != null)
            {
                health.Decrement();
            }
            
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            isGrounded = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            isGrounded = false;
        }
    }
}
