using System.Collections;
using UnityEngine;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;

public class NatcoderManager : MonoSingleton<NatcoderManager>
{
    [Header(@"Recording")]  
    public bool recordMicrophone;
    public Camera TargetCamera;

    private int videoWidth = Screen.width;
    private int videoHeight = Screen.height;

    private MP4Recorder recorder;
    private CameraInput cameraInput;
    ///private AudioInput audioInput;
    //private AudioSource microphoneSource;

    /*
    private IEnumerator Start()
    {
        // Start microphone
        microphoneSource = gameObject.AddComponent<AudioSource>();
        microphoneSource.mute =
        microphoneSource.loop = true;
        microphoneSource.bypassEffects =
        microphoneSource.bypassListenerEffects = false;

        try
        {
            microphoneSource.clip = Microphone.Start(null, true, 1, AudioSettings.outputSampleRate);
        }
        catch (System.Exception e)
        {
            Debug.Log("mocrophone source-- " + e);
            Test.Instance.DebugPrint("mocrophone source-- " + e);
        }

        yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
        microphoneSource.Play();
    }
    */

    /*
    private void OnDestroy()
    {
        // Stop microphone
        microphoneSource.Stop();
        Microphone.End(null);
    }
    */

    public void StartRecording()
    {
        if (videoWidth % 2 != 0)
        {
            videoWidth -= 1;
        }

        if (videoHeight % 2 != 0)
        {
            videoHeight -= 1;
        }

        // Start recording
        var frameRate = 30;
        var sampleRate = recordMicrophone ? AudioSettings.outputSampleRate : 0;
        var channelCount = recordMicrophone ? (int)AudioSettings.speakerMode : 0;
        var clock = new RealtimeClock();
        recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount, audioBitRate: 96_000);
        // Create recording inputs
        cameraInput = new CameraInput(recorder, clock, TargetCamera);
        ///audioInput = recordMicrophone ? new AudioInput(recorder, clock, microphoneSource, true) : null;
        // Unmute microphone
        ///microphoneSource.mute = audioInput == null;
    }

    public async void StopRecording()
    {
        // Mute microphone
        ///microphoneSource.mute = true;
        // Stop recording
        ///audioInput?.Dispose();
        cameraInput.Dispose();
        var path = await recorder.FinishWriting();
        // Playback recording
        Debug.Log($"Saved recording to: {path}");
       
        string[] words = path.Split('\\');
        UIManager.Instance.Filename = words[words.Length - 1];
        Debug.Log("natcoder stoprecording set video name-- " + UIManager.Instance.Filename);
        //Test.Instance.DebugPrint("natcoder stoprecording set video name-- " + UIManager.Instance.Filename);

#if UNITY_EDITOR_OSX
        Debug.Log("Before CheckFileRunDone-- " + UIManager.Instance.Filename);
        MonoExpend.Instance.CheckFileRunDone(UIManager.Instance.Filename, Photo_Video.Instance.PlayVideoOnRawImage);     
#elif UNITY_IPHONE
        MonoExpend.Instance.CheckFileRunDone(UIManager.Instance.Filename, Photo_Video.Instance.PlayVideoOnRawImage);
        //Test.Instance.DebugPrint("Before CheckFileRunDone-- " + UIManager.Instance.Filename);
#elif UNITY_EDITOR_WIN
        Debug.Log("Before CheckFileRunDone-- " + Application.persistentDataPath + "/" + UIManager.Instance.Filename);
        MonoExpend.Instance.CheckFileRunDone(Application.persistentDataPath + "/" + UIManager.Instance.Filename, Photo_Video.Instance.PlayVideoOnRawImage);
#elif UNITY_ANDROID
        MonoExpend.Instance.CheckFileRunDone(UIManager.Instance.Filename, Photo_Video.Instance.PlayVideoOnRawImage);
        //Test.Instance.DebugPrint("Before CheckFileRunDone-- " + UIManager.Instance.Filename);     
#endif
    }

    public async void CancelRecording()
    {
        // Mute microphone
        ///microphoneSource.mute = true;
        // Stop recording
        ///audioInput?.Dispose();
        cameraInput.Dispose();
        var path = await recorder.FinishWriting();
    }
}