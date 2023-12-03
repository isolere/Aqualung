using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/**
 * Gestor de so i pistes de música. Inclou sistema de crossfade per fer l'intercanvi de pistes
 */
public class AudioManager : MonoBehaviour
{
    [Header("Music")] [SerializeField] private AudioClip CombatClip;
    [SerializeField] private AudioClip AmbientClip;
    [SerializeField] [Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField] private float fadeSpeed = 1f;


    [Header("SFX")] [SerializeField] private AudioClip hoverClip;
    [SerializeField] [Range(0f, 1f)] private float hoverVolume = 1f;

    [SerializeField] private AudioClip selectClip;
    [SerializeField] [Range(0f, 1f)] private float selectVolume = 1f;

    [SerializeField] private AudioClip VictoryClip;
    
    [SerializeField] private AudioClip defeatClip;

    
    private static AudioManager _instance;
    private AudioSource _audioSource1;
    private AudioSource _audioSource2;
    private AudioSource _audioSourceCurrent;

    // Variables públiques per als Sliders
    public Slider sliderMusic;
    public Slider sliderEffects;

    public static AudioManager Instance
    {
        get { return _instance; }
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
            _audioSource1 = gameObject.AddComponent<AudioSource>();
            _audioSource1.volume = musicVolume;
            _audioSource1.loop = true;

            _audioSource2 = gameObject.AddComponent<AudioSource>();
            _audioSource2.volume = 0f;
            _audioSource2.loop = true;

            _audioSourceCurrent = _audioSource1;

            DontDestroyOnLoad(gameObject);
        }

        // Cridar a la funció d'inicialització de volum
        InitializeVolume();
    }

    private void Start()
    {
        // Ens suscribim als esdeveniments d'alerta
        GameState.Instance.OnAlertStateChange += CheckAlert;
    }

    //Funció per inicialitzar els valor de volum de música i efectes de so
    private void InitializeVolume()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        hoverVolume = PlayerPrefs.GetFloat("effectsVolume", 1.0f);

        sliderMusic.value = musicVolume;
        sliderEffects.value = hoverVolume;
    }


    public void PlayTrack(AudioClip clip)
    {
        if (_audioSourceCurrent.clip == clip)
        {
            // No cal fer res, ja s'està redproduïnt
            return;
        }

        AudioSource nextAudioSource;

        if (_audioSourceCurrent == _audioSource1)
        {
            nextAudioSource = _audioSource2;
        }
        else
        {
            nextAudioSource = _audioSource1;
        }

        nextAudioSource.clip = clip;
        StartCoroutine(StartFade(_audioSourceCurrent, fadeSpeed, 0f));
        StartCoroutine(StartFade(nextAudioSource, fadeSpeed, musicVolume));

        _audioSourceCurrent = nextAudioSource;
    }

    public void PlayCombatTrack()
    {
        PlayTrack(CombatClip);
    }

    public void PlayAmbientTrack()
    {
        PlayTrack(AmbientClip);
    }

    public void SetCombatTrack(AudioClip clip)
    {
        Instance.CombatClip = clip;
    }

    public void SetAmbientTrack(AudioClip clip)
    {
        Instance.AmbientClip = clip;
    }

    public void StopTrack()
    {
        _audioSource1.Stop();
        _audioSource1.clip = null;
        _audioSource2.Stop();
        _audioSource2.clip = null;
    }

    public void PlayHoverClip()
    {
        PlayClip(hoverClip, hoverVolume);
    }

    public void PlaySelectClip()
    {
        PlayClip(selectClip, selectVolume);
    }


    public void PlayDefeatClip()
    {
        PlayClip(defeatClip);
    }
    public void PlayClip(AudioClip clip, float volume = 1f)
    {
        PlayClip(clip, Camera.main.transform.position, volume);
    }

    public void PlayClip(AudioClip clip, Vector3 location, float volume = 1f)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, location, volume);
        }
    }


    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        audioSource.Play();
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }

    private void CheckAlert()
    {
        if (GameState.Instance.IsAlerted())
        {
            PlayCombatTrack();
        }
        else
        {
            PlayAmbientTrack();
        }
    }

    public void PlayVictoryClip()
    {
        PlayClip(VictoryClip);
    }


    // Funció per desar i actualitzar els valors de volum de la música
    public void OnMusicVolumeUpdate()
    {
        musicVolume = sliderMusic.value;

        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    //Funció per desar i actualitzar els valors del volum dels efectes
    public void OnEffectsVolumeUpdate()
    {
        hoverVolume = sliderEffects.value;

        PlayerPrefs.SetFloat("effectsVolume", hoverVolume);
        PlayerPrefs.Save();
    }
}