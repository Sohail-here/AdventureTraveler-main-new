using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using FfmpegUnity;

public class Test : MonoSingleton<Test>
{
    public GameObject[] AR_Models;

    public Button BtnModel0;
    public Button BtnModel1;
    public Button BtnModel2;

    void Start()
    {
        BtnModel0.onClick.AddListener(SwitchToModel0);
        BtnModel1.onClick.AddListener(SwitchToModel1);
        BtnModel2.onClick.AddListener(SwitchToModel2);
    }

    public void SwitchToModel0()
    {
        AR_Models[0].SetActive(true);
        AR_Models[1].SetActive(false);
        AR_Models[2].SetActive(false);
    }

    public void SwitchToModel1()
    {
        AR_Models[0].SetActive(false);
        AR_Models[1].SetActive(true);
        AR_Models[2].SetActive(false);
    }

    public void SwitchToModel2()
    {
        AR_Models[0].SetActive(false);
        AR_Models[1].SetActive(false);
        AR_Models[2].SetActive(true);
    }
}