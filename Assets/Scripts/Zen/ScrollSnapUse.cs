using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;

public class ScrollSnapUse : MonoBehaviour
{
    public void GetCurrentPanel()
    {
        Debug.Log(gameObject.GetComponent<SimpleScrollSnap>().CurrentPanel);
    }
}