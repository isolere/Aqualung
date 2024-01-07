using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;


public class AtacEspecialBenzo : MonoBehaviour
{
    private AnimationController control;

    public float distanciaAtac;
    public float _cooldown = 1f;

    private Animator _animator;
    private float _lastAttack;
    private GameObject _player;

    public GameObject specialAttackPrefab;
    public GameObject startPointObject;
    public GameObject endPointObject;

    public List<GameObject> plataformesCova;
    private int _numPlataformes;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;

    public GameObject plataformaCovaPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        control = GetComponent<AnimationController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _numPlataformes = plataformesCova.Count;
        Debug.Log("Num Plataformes= " + _numPlataformes);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(_player.transform.position, transform.position);

        if (distanceFromPlayer < distanciaAtac)
        {
            if (Time.time - _lastAttack > _cooldown)
            {
                _animator.SetTrigger("SpecialAttack");
                control.Bounce(5f);
                _lastAttack = Time.time;

                StartCoroutine(InstantiateAlongLine(startPointObject.transform.position, endPointObject.transform.position, 1f));
                StartCoroutine(NovesPlataformes(3f));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtac);
    }

    IEnumerator InstantiateAlongLine(Vector2 startPoint, Vector2 endPoint, float delay)
    {
        yield return new WaitForSeconds(delay);

        CinemachineShake.Instance.Shake(1f, 0.5f);

        int numberOfInstances = 6;

        for (int i = 0; i < numberOfInstances; i++)
        {
            float t = i / (float)(numberOfInstances - 1);
            Vector2 pointOnLine = Vector2.Lerp(startPoint, endPoint, t);

            Instantiate(specialAttackPrefab, pointOnLine, Quaternion.identity);
        }

        foreach (var obj in plataformesCova)
        {
            obj.SetActive(false);
        }
    }

   IEnumerator NovesPlataformes(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach(var plataforma in plataformesCova)
        {
            float randomX = Random.Range(Mathf.Min(point1.position.x, point2.position.x), Mathf.Max(point3.position.x, point4.position.x));
            float randomY = Random.Range(Mathf.Min(point1.position.y, point3.position.y), Mathf.Max(point2.position.y, point4.position.y));

            Vector2 randomPosition = new Vector2(randomX, randomY);

            plataforma.transform.position = randomPosition;
            plataforma.SetActive(true);
        }
    }
}
