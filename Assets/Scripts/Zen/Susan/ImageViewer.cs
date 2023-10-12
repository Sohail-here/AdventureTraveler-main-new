using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageViewer : MonoBehaviour
{
    public enum PathPrefix
    {
        none,
        dataPath,
        persistentDataPath
    }

    [SerializeField]
    RawImage rawImage;
    public PathPrefix pathPrefix;
    public string ImgPath;
    public float PlaybackTime = 3f;

    public UnityEvent StartPlayback;
    public UnityEvent EndPlayback;

    private void OnEnable()
    {
        // Setting path prefix
        string prefix = "";
        if (pathPrefix == PathPrefix.dataPath)
        {
            prefix = Application.dataPath;
        }
        else if (pathPrefix == PathPrefix.persistentDataPath)
        {
            prefix = Application.persistentDataPath;
        }

        // Load image from path
        byte[] byteArray = File.ReadAllBytes(prefix + "/" + ImgPath);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(byteArray);
        rawImage.texture = texture;

        // Call start event
        StartPlayback.Invoke();

        // Start playback coroutine
        StartCoroutine(PlaybackImage(PlaybackTime));
    }

    IEnumerator PlaybackImage(float second)
    {
        yield return new WaitForSeconds(second);
        EndPlayback.Invoke();
    }
}
