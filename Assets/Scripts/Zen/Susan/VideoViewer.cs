using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoViewer : MonoBehaviour
{
    public enum PathPrefix
    {
        none,
        dataPath,
        persistentDataPath
    }

    [SerializeField]
    VideoPlayer videoPlayer;
    public PathPrefix pathPrefix;
    public string VideoPath;

    public UnityEvent StartPlayback;
    public UnityEvent EndPlayback;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void OnEnable()
    {
        // Setting video path
        string prefix = "";
        if (pathPrefix == PathPrefix.dataPath)
        {
            prefix = Application.dataPath;
        }
        else if (pathPrefix == PathPrefix.persistentDataPath)
        {
            prefix = Application.persistentDataPath;
        }
        videoPlayer.url = prefix + "/" + VideoPath;

        // Set video playback end event
        videoPlayer.loopPointReached += EndReached;

        // call start event
        StartPlayback.Invoke();

        // Play video
        videoPlayer.Play();
    }

    void EndReached(VideoPlayer vp)
    {
        vp.Stop();
        EndPlayback.Invoke();
    }
}
