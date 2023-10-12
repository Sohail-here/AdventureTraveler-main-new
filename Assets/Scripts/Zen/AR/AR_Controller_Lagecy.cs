using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using DanielLochner.Assets.SimpleScrollSnap;

public class AR_Controller_Lagecy : MonoBehaviour
{
    /*

    // ScrollSnap
    public GameObject AR_ObjSelecter;
    // For selfie
    public GameObject[] SelfModel;
    // 3D models for world
    public GameObject[] WorldModel;
    public GameObject _3D_Camera;
    public GameObject[] AR_ObjButtons;

    public RenderTexture AR_RT;
    public RenderTexture SelfRT;
    
    public Renderer[] FaceRenderer;
    public static int ScrollIndex = 0;

    public PlaceOnPlane placeOnPlane;
    public ChangeCamera changeCamera;

    public RawImage CaptureImage;

    // Start is called before the first frame update
    void Start()
    {
        WebCamManager.Instance.StopWebCam();
    }

    public void SwapObjs()
    {
        ScrollIndex = AR_ObjSelecter.GetComponent<SimpleScrollSnap>().CurrentPanel;

        for (int i = 0; i < AR_ObjButtons.Length; i++)
        {
            if (i == ScrollIndex)
            {
                AR_ObjButtons[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                AR_ObjButtons[i].GetComponent<Button>().enabled = false;
            }
        }

        // Origin camera
        if (ScrollIndex == 0)
        {
            _3D_Camera.SetActive(false);
            CloseUserModels();
            WebCamManager.Instance.StopWebCam();

            changeCamera.OpenUserCamera();          
            CaptureImage.texture = AR_RT;
        }
        // User camera
        else if (ScrollIndex < 4)
        {
            _3D_Camera.SetActive(true);
            CloseUserModels();
            WebCamManager.Instance.StopWebCam();
            WebCamManager.Instance.PlayWebCam();

            SelfModel[ScrollIndex - 1].SetActive(true);

            FaceRenderer[ScrollIndex - 1].material.mainTexture = WebCamManager.Instance.backCam;

            CaptureImage.texture = SelfRT;

            Debug.Log(ScrollIndex);
        }
        // World camera
        else
        {
            _3D_Camera.SetActive(false);
            CloseUserModels();
            WebCamManager.Instance.StopWebCam();

            CaptureImage.texture = AR_RT;
            placeOnPlane.placePrefab = WorldModel[ScrollIndex - 4];
            changeCamera.OpenWorldCamera();
            Debug.Log(ScrollIndex);     
        }       

        Debug.Log(ScrollIndex);
    }

    private void CloseWorldModels()
    {
        for (int i = 0; i < WorldModel.Length; i++)
        {
            WorldModel[i].SetActive(false);
        }
    }

    private void CloseUserModels()
    {
        for (int i = 0; i < SelfModel.Length; i++)
        {
            SelfModel[i].SetActive(false);
        }
    }

    */
}