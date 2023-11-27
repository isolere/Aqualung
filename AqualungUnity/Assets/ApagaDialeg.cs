using UnityEngine;

public class ApagaDialeg : MonoBehaviour
{
    public float desactivationTime = 10f;
    private Inventory _inventory;

    void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public void Start()
    {
        Invoke("DesactivarGameObject", desactivationTime);
        Invoke("ObteAmulet", desactivationTime-8f);
    }

    void DesactivarGameObject()
    {
        gameObject.SetActive(false);
    }

    public void ObteAmulet()
    {
        if (_inventory != null && _inventory.Has("Amulet")==false && gameObject.CompareTag("Trito"))
        {
            _inventory.Add("Amulet");
        }
    }
}
