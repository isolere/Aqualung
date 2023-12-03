using UnityEngine;

/**
 * Classe que manté l'estat del joc durant la partida
 */
public class GameState : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnScoreChanged;
    public event OnEventDelegate OnAlertStateChange;

    private int _score;
    private int _fragmentsAqualung;

    public int FragmentsAqualung
    {
        get { return _fragmentsAqualung; }
        set { _fragmentsAqualung=value; }
    }

    private int _canonadesReparades;

    public int CanonadesReparades
    {
        get {return _canonadesReparades;}
    }

    private static GameState _instance;

    private int _alertedEnemies = 0;

    public static GameState Instance
    {
        get { return _instance; }
    }

    private int _currentLevel;


    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        _canonadesReparades = 0;
    }

    public int GetScore()
    {
        return _score;
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        Mathf.Clamp(_score, 0, int.MaxValue);

        if (OnScoreChanged != null) OnScoreChanged();
    }

    public void ReparaCanonada()
    {
        _canonadesReparades+=1;
        Debug.Log("Reparada= " + _canonadesReparades);
    }

    public void Reset()
    {
        _score = 0;
        _currentLevel = 0;

        ResetAlert();
    }

    public void ResetAlert()
    {
        _alertedEnemies = 0;
    }

    public void IncreaseAlert()
    {
        _alertedEnemies++;

        if (_alertedEnemies == 1 && OnAlertStateChange != null)
        {
            OnAlertStateChange();
        }
    }

    public void DecreaseAlert()
    {
        _alertedEnemies--;
        if (_alertedEnemies == 0 && OnAlertStateChange != null)
        {
            OnAlertStateChange();
        }
    }

    public bool IsAlerted()
    {
        return _alertedEnemies > 0;
    }
}