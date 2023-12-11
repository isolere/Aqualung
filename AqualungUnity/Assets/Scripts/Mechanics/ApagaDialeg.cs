using UnityEngine;
using System.Collections;

/*Classe que s'utilitza amb els NPCs per fer que el di�leg que mostrin s'apagui despr�s d'un temps concret. En el cas de Aqua, el
 trit�, tamb� es gestiona l'entrega a la Nixie de l'amulet que li permetr� utilitzar les seves habilitats sense gastar vida.*/
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
        /*Comencem amb el CancelInvoke, per si hi ha un Invoke esperant a ser implementat, ja que sin�, ens podem trobar amb moments en 
         qu� el text apareix, i desapareix massa r�pid.*/
        CancelInvoke("DesactivarGameObject");
        CancelInvoke("ObteAmulet");
        Invoke("DesactivarGameObject", desactivationTime);
        Invoke("ObteAmulet", desactivationTime - 8f);
    }

    void DesactivarGameObject()
    {
        gameObject.SetActive(false);
    }

    /*M�tode d'obtenci� de l'amulet. Comprovem que la Nixie no el tingui ja al seu inventari, i que l'NPC amb qu� estiguem parlant sigui el
     trit�.Si �s aix�, afegim l'amulet a l'inventari, i reproduim l'animaci� corresponent. Quan l'animaci� acabi, activem l'element de la UI
    que representa que tenim l'amulet en el nostre poder, i reporiduim el sistema de part�cules.*/
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
