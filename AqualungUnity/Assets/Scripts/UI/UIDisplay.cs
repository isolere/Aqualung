using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Platformer.Mechanics;

/**
 * Component per configurar la interficie d'usuari durant el joc.
 */
public class UIDisplay : MonoBehaviour
{
    [Header("Player Stats")] 

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider healthSliderUnFrag;
    [SerializeField] private Slider healthSliderDosFrag;
    [SerializeField] private Slider reservaAigua;

    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missionText;

    private LevelManager _levelManager;

    //[Header("Animations")] 

    //[SerializeField] Animation scoreAnimation;
    //[SerializeField] Animation healthAnimation;

    void Start()
    {
        GameState.Instance.OnScoreChanged += UpdateScore;

        _levelManager = FindObjectOfType<LevelManager>();
        levelText.text = _levelManager.GetCurrentLevelTitle();
        missionText.text = _levelManager.GetCurrentLevelMission();

        SetHealthBar();

       /* healthSlider.maxValue = _health.maxHP;
        healthSlider.value = healthSlider.maxValue;*/

        reservaAigua.maxValue = _health.maxHP;
        reservaAigua.value = _health.reservaAigua;
        
        scoreText.text = GameState.Instance.GetScore().ToString();

        _health.OnHealthChanged += UpdateHealth;
    }

    public void SetHealthBar()
    {
        if (GameState.Instance.FragmentsAqualung == 1)
        {
            healthSlider.gameObject.SetActive(false);
            healthSliderUnFrag.gameObject.SetActive(true);
            healthSlider = healthSliderUnFrag;
        }
        else if (GameState.Instance.FragmentsAqualung == 2)
        {
            healthSlider.gameObject.SetActive(false);
            healthSliderDosFrag.gameObject.SetActive(true);
            healthSlider = healthSliderDosFrag;
        }

        healthSlider.maxValue = _health.maxHP;
        healthSlider.value = healthSlider.maxValue;
    }

    void UpdateHealth(int amount)
    {
        int newHealth = _health.getCurrentHP;

        healthSlider.value = newHealth;

        if(newHealth<_health.reservaAigua)
        {
            reservaAigua.value = newHealth;
        }
        else
        {
            reservaAigua.value = _health.reservaAigua;
        }

        /*if (amount < 0)
        {
            healthAnimation.Play();
        }*/
    }

    void UpdateScore()
    {
        //scoreAnimation.Play();
        scoreText.text = GameState.Instance.GetScore().ToString();
    }

    private void OnDestroy()
    {
        // Com que _gameState és un singleton, si no ens desuscribim quan
        // es canvia de nivell continua intentant actualitzar la UI antiga
        GameState.Instance.OnScoreChanged -= UpdateScore;
        _health.OnHealthChanged -= UpdateHealth;

    }
}