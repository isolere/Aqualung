using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidPLayer: MonoBehaviour
{
    [SerializeField] string videoFileName;

    private VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;

        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            OnVideoEnd(videoPlayer);
        }
    }

    public void PlayVideo()
    {

        if (videoPlayer)
        {
            string videoPath=System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
                  
        }


    }
    void OnVideoEnd(VideoPlayer vp)
    {
        GameManager.LoadGame();
    }

}
