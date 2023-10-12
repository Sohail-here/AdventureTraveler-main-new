using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    public GameObject MyCamera;

    public Camera sourceCamera;

    private Camera _myCamera;

#if UNITY_ANDROID
    private void OnEnable()
    {
        _myCamera = MyCamera.GetComponent<Camera>();
    }

    private void OnPreRender()
    {
        if (_myCamera == null)
        {
            return;
        }
        if (sourceCamera == null)
        {
            return;
        }
        _myCamera.projectionMatrix = sourceCamera.projectionMatrix;
    }
#endif

}