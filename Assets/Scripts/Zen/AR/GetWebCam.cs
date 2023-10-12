using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWebCam : MonoBehaviour
{
    public static GetWebCam instance;

    public GetWebCam()
    {
        instance = this;
    }

    public RawImage background;
    public AspectRatioFitter fit;
    public bool isForward;

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;

    // Start is called before the first frame update
    void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                //backCam = new WebCamTexture(devices[i].name, (int)background.rectTransform.rect.size.x, (int)background.rectTransform.rect.size.y);
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null)
        {
            Debug.Log("Unable to find back camera");
            return;
        }

        backCam.Play();
        background.texture = backCam;

        camAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isForward)
        {
            if (!camAvailable)
            {
                return;
            }

            float ratio = (float)backCam.width / (float)backCam.height;
            fit.aspectRatio = ratio;

            float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;

            background.rectTransform.localScale = new Vector3(2f, 2 * scaleY, 1f);

            int orient = -backCam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }
}
