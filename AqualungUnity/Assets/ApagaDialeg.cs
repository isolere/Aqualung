using UnityEngine;
using System.Collections;

public class ApagaDialeg : MonoBehaviour
{
    public float desactivationTime = 10f;
    public float moveDuration = 2f;
    public Transform targetPosition;
    public string animationName="AnimacioAmulet";

    private Inventory _inventory;
    [SerializeField] private GameObject _prefabAmulet;
    [SerializeField] private GameObject _amuletUI;

    void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Start()
    {
        Invoke("DesactivarGameObject", desactivationTime);
        Invoke("ObteAmulet", desactivationTime - 8f);
    }

    void DesactivarGameObject()
    {
        gameObject.SetActive(false);
    }

    void ObteAmulet()
    {
        if (_inventory != null && _inventory.Has("Amulet") == false && gameObject.CompareTag("Trito"))
        {
            _inventory.Add("Amulet");
            _prefabAmulet.SetActive(true);

            Animation animation = _prefabAmulet.GetComponent<Animation>();
            if (animation != null && !string.IsNullOrEmpty(animationName))
            {
                animation.Play(animationName);
                Invoke("MoveObjectAfterAnimation", animation[animationName].length);
            }
        }
    }

    void MoveObjectAfterAnimation()
    {
        StartCoroutine(MoveToObject(_prefabAmulet.transform, targetPosition.position, moveDuration));
        _amuletUI.SetActive(true);
    }

    IEnumerator MoveToObject(Transform objTransform, Vector3 target, float duration)
    {
        Debug.Log("Invoca");

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objTransform.position = Vector3.Lerp(objTransform.position, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        objTransform.position = target;
    }
}
