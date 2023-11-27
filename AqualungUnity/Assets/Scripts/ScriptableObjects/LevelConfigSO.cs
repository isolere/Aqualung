using UnityEngine;

/**
 * ScriptableObject que permet configurar un nivell
 */
[CreateAssetMenu(menuName = "Level Config", fileName = "New LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] public string sceneName = "Game";
    [SerializeField] public string levelTitle = "Level";
    [SerializeField] public string levelMission = "Mission";
    [SerializeField] public AudioClip ambientMusic = null;
    [SerializeField] public AudioClip combatMusic = null;    
}