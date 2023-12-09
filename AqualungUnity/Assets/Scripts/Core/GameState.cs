using UnityEngine;

/**
 * Classe que manté l'estat del joc durant la partida. Es tracta d'un Singleton que es mantindrà actiu
 * encara que canviem de nivell
 */
public class GameState : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnScoreChanged;
    public event OnEventDelegate OnAlertStateChange;

    private int _score;

    //Controlarem els fragments d'aqualung recollits per mantenir actualitzat el nivell de vida màxia
    //durant la resta de la partida
    private int _fragmentsAqualung;

    public int FragmentsAqualung
    {
        get { return _fragmentsAqualung; }
        set { _fragmentsAqualung=value; }
    }
    //Controlarem les canonades reparades durant el nivell 1
    private int _canonadesReparades;

    public int CanonadesReparades
    {
        get {return _canonadesReparades;}
    }

    private static GameState _instance;

    private bool _alertedEnemies = false;

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
        /*Ens assegurem que la instància de la classe es manté activa quan s'inicii una escena
         nova. Si no existeix, creem una instància nova.*/
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
    //Controlarem la quantitat de canonades reparades per l'objectiu de la missió del nivell 1
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
        _alertedEnemies = false;
    }

    public void IncreaseAlert()
    {
        _alertedEnemies=true;
        Debug.Log("Alerted: " + _alertedEnemies);

        if (_alertedEnemies == true && OnAlertStateChange != null)
        {
            OnAlertStateChange();
        }
    }

    public void DecreaseAlert()
    {
        _alertedEnemies=false;
        Debug.Log("Alerted: " + _alertedEnemies);

        if (_alertedEnemies == false && OnAlertStateChange != null)
        {
            OnAlertStateChange();
        }
    }

    public bool IsAlerted()
    {
        return _alertedEnemies;
    }
}