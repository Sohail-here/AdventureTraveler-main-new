using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FfmpegUnity;

public class FfmpegUse : MonoBehaviour
{
    public FfmpegCommand FfmpegCmd;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="imgPath">ex E:\images\</param>
    /// <param name="impName">ex IMG_  %3d 後面數字 (會照順序)</param>
    /// <param name="outPath">ex E:\</param>
    /// <param name="outName">ex test</param>
    /*public void FfmpegImageToVideo(string imgPath, string impName, string outPath, string outName)
    {
        FfmpegCmd.Options = $"-r 1 -start_number 1 -i \"{imgPath}{impName}%5d.jpg\" -vf \"fps = 25,format = yuv420p\" {outPath}{outName}.mp4";
        FfmpegCmd.StartFfmpeg();
        Debug.Log("StartFfmpeg");
    }*/
}