using System;
using UnityEngine;

/**
 * Classe que configura el nivell actual quan carrega la escena
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
        GameState.Instance.CurrentLevel++;

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