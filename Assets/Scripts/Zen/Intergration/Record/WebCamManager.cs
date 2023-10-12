using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamManager : MonoSingleton<WebCamManager>
{
    public RawImage WebCamRawImage;

    public WebCamTexture backCam;

    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            Debug.Log("No Camera Detected");
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing)
            {
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        WebCamRawImage.texture = backCam;

        //StartCoroutine(WaitToStopCam());
        //backCam.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWebCam()
    {
        backCam.Play();
        WebCamRawImage.texture = backCam;
    }

    public void StopWebCam()
    {
        backCam.Stop();
    }

    IEnumerator WaitToStopCam()
    {
        yield return new WaitForSeconds(0.5f);
        backCam.Stop();
    }
}
