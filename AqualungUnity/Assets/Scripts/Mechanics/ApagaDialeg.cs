using UnityEngine;
using System.Collections;

public class ApagaDialeg : MonoBehaviour
{
    public float desactivationTime = 10f;
    public float moveDuration = 2f;
    public string animationName="AnimacioAmulet";

    private Inventory _inventory;
    [SerializeField] private GameObject _prefabAmulet;
    [SerializeField] private GameObject _amuletUI;
    [SerializeField] private ParticleSystem _explosio;


    void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Start()
    {
        CancelInvoke("DesactivarGameObject");
        CancelInvoke("ObteAmulet");
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
                Invoke("ActivaAmuletUI", animation[animationName].length);
            }
        }
    }

    void ActivaAmuletUI()
    {
        _amuletUI.SetActive(true);
        _prefabAmulet.SetActive(false);
        _explosio.Play();
    }
}
