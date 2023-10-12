using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    public RenderTexture mapImage;

    private static ScreenShot instance;

    private new Camera camera;
    private bool takeScreenShotOnNextFrame;

    private void Awake()
    {
        instance = this;
        camera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenShotOnNextFrame)
        {
            takeScreenShotOnNextFrame = false;
            RenderTexture renderTexture = camera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/" + UIManager.Instance.GetUUID() + Record.Instance.DateTime + ".png", byteArray);
            //File.WriteAllBytes(Application.persistentDataPath + "/" + UIManager.instance.GetUUID() + Record.instance.DateTime + ".png", byteArray);
            Debug.Log(Record.instance.DateTime);

            RenderTexture.ReleaseTemporary(renderTexture);
            camera.targetTexture = null;
            camera.targetTexture = mapImage;
        }
    }

    private void TakeScreenShot(int width, int height)
    {
        camera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShotOnNextFrame = true;
    }

    public static void TakeScreenShot_Static(int width, int height)
    {
        instance.TakeScreenShot(width, height);       
    }

    public static Texture2D TakeCameraShot(Camera camera, Rect rect, string filename)
    {
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, -1);
        camera.targetTexture = rt;
        camera.Render();

        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();
        
        camera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(filename, bytes);

        return screenShot;

        // Example.
        //ScreenShot.TakeCameraShot(MapCamera, new Rect(0, 0, Screen.width, Screen.height), Application.persistentDataPath + "/" + "video" + ".png");
    }
}