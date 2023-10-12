using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeUse : MonoSingleton<NativeUse>
{
    /// <summary>
    /// Share VideoDetail's video url
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="content"></param>
    /// <param name="url"></param>
    public void ShareWithOtherApp(string subject, string content, string url)
    {
        Debug.Log(subject);
        Debug.Log(content);
        Debug.Log(url);
        new NativeShare()
            .SetSubject(subject).SetText(content).SetUrl(url)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    /// <summary>
    /// Target is the thing what you want to shrare.(Image)
    /// </summary>
    /// <param name="Target"></param>
    public void ShareWithOtherApp(GameObject Target, string subject, string content, string url)
    {
        StartCoroutine(C_ShareWithOtherApp(Target, subject, content, url));
    }

    IEnumerator C_ShareWithOtherApp(GameObject Target, string subject, string content, string url)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = (Texture2D)Target.GetComponent<RawImage>().texture;

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject(subject)
            .SetText(content)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    public void ShareVideoWithOtherApp(string filePath)
    {
        //Test.Instance.DebugPrint("before share");
        StartCoroutine(C_ShareVideoWithOtherApp(filePath));
        //Test.Instance.DebugPrint("after share");
    }

    IEnumerator C_ShareVideoWithOtherApp(string filePath)
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("nativeshare-- " + filePath);
        new NativeShare().AddFile(filePath)
            .SetSubject("Subject goes here").SetText("AdventureTraveler")
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }
}