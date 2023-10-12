using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMapCamera : MonoBehaviour
{
    private OnlineMapsCameraOrbit cameraOrbit;

    private void Start()
    {
        cameraOrbit = OnlineMapsCameraOrbit.instance;
        OnlineMaps.instance.OnUpdateBefore += OnUpdateBefore;
    }

    private void Update()
    {
        
    }

    private void OnUpdateBefore()
    {
        cameraOrbit.adjustTo = OnlineMapsCameraAdjust.averageCenter;
        cameraOrbit.rotation.x = 30;
        cameraOrbit.distance = 500;
    }
}