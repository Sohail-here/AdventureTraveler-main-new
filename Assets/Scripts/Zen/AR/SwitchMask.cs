using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using DanielLochner.Assets.SimpleScrollSnap;

public class SwitchMask : MonoBehaviour
{
    public GameObject[] Masks;
    public GameObject ArSessionOrigin;

    public void MaskSnap()
    {
        //ArCamera.SetActive(false);
        ArSessionOrigin.GetComponent<ARFaceManager>().facePrefab = Masks[gameObject.GetComponent<SimpleScrollSnap>().CurrentPanel];
        //ArCamera.SetActive(true);
    }
}
