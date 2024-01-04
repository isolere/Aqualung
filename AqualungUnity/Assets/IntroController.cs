using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;

        videoPlayer.Play();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            OnVideoEnd(videoPlayer);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        GameManager.LoadGame();
    }
}
