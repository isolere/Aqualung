using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBenzo : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 1.3f;

    private GameObject target;


    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funció per designar el moviment del Enemic (Benzo)
    private void UpdateTarget()
    {
        // Primer  es crea el target
        if (target == null)
        {
            target = new GameObject("Target");
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
            // Return per torna el valor
            return;
        }

        // Al arribar al límit de l'esquerra cal tornar cap a la detra
        if (target.transform.position.x == minX)
        {
            target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1,1,1);
        }

        // Si estem a la part dreta cal tornar a l'esquerra
        else if (target.transform.position.x == maxX)
        {
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1,1,1);
        }
    }

    private IEnumerator PatrolToTarget()
    {
        //Corrutina per moure l'enemic
        while (Vector2.Distance(transform.position, target.transform.position) > 0.1f)
        {
            Vector2 direction = target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            // Return sempre que el valor no sigui menor de 0.1f
            yield return null;
        }

        //Si s'ha aconseguit arribar al target
        Debug.Log("Tenim el target");
        transform.position = new Vector2(target.transform.position.x, transform.position.y);

        // Esperar un moment. Temps d'espera
        Debug.Log("Esperar" + waitingTime);
        yield return new WaitForSeconds(waitingTime);

        //Quan acaba el temps d'espera cal tornar al target. Comença de nou tot el procés
        Debug.Log("Espera terminada. Tornar al target");
        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }
}
