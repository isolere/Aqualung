using System;
using UnityEngine;

/**
 * Classe que configura el nivell actual quan carrega la escena. Controla l'ScriptableObject que s'encarrega de
 * la configuració del nivell, i entrega a la UI la informació corresponent. També gestiona com ha d'actuar el
 * joc en el moment en que es finalitza un nivell.
 */
public class LevelManager : MonoBehaviour
{
    public delegate void OnEventDelegate();

    public event OnEventDelegate OnLevelEnd;

    private LevelConfigSO _currentLevelConfig;

    private void Start()
    {
        _currentLevelConfig = GetConfigLevel(GameState.Instance.CurrentLevel);

        AudioManager.Instance.SetAmbientTrack(_currentLevelConfig.ambientMusic);
        AudioManager.Instance.SetCombatTrack(_currentLevelConfig.combatMusic);
        AudioManager.Instance.PlayAmbientTrack();
    }

    public LevelConfigSO GetConfigLevel(int level)
    {
        var gl = GameManager.GameLevels;
        return GameManager.GameLevels.Levels[level];
    }

    public String GetCurrentLevelTitle()
    {
        return _currentLevelConfig.levelTitle;
    }
    
    public String GetCurrentLevelMission()
    {
        return _currentLevelConfig.levelMission;
    }

    public void EndLevel()
    {
        //Indiquem a GameState que s'ha incrementat de nivell
        GameState.Instance.CurrentLevel++;

        /*En cas que aquest sigui l'últim nivell, carregarem l'escena de victòria, en cas contrari
        carregarem des del GameManager el següent nivell*/
        if (GameState.Instance.CurrentLevel == GameManager.GameLevels.Levels.Count)
        {
            GameManager.LoadVictory();
        }
        else
        {
            if (OnLevelEnd != null) OnLevelEnd();

            GameManager.LoadLevel(GetConfigLevel(GameState.Instance.CurrentLevel).sceneName);
        }
    }
}