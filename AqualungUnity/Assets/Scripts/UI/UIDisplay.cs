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
    [SerializeField] private Slider reservaAigua;
    [SerializeField] private Slider marquesReserva;

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

        healthSlider.maxValue = _health.maxHP;
        healthSlider.value = healthSlider.maxValue;

        reservaAigua.maxValue = _health.maxHP;
        reservaAigua.value = _health.reservaAigua;
        
        scoreText.text = GameState.Instance.GetScore().ToString();

        _health.OnHealthChanged += UpdateHealth;
    }

    void UpdateHealth(int amount)
    {
        int newHealth = _health.getCurrentHP;

        healthSlider.value = newHealth;

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