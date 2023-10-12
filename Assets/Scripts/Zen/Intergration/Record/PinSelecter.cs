using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanielLochner.Assets.SimpleScrollSnap;

public class PinSelecter : MonoSingleton<PinSelecter>
{
    public SimpleScrollSnap SV_Pins;
    public GameObject SV_Pins_Content;

    public void Selecting()
    {
        SV_Pins_Content.transform.GetChild(SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel).
            transform.GetChild(0).gameObject.SetActive(true);
        SV_Pins_Content.transform.GetChild(SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel).
            transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Changed()
    {
        SV_Pins_Content.transform.GetChild(SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel).
            transform.GetChild(0).gameObject.SetActive(false);
        SV_Pins_Content.transform.GetChild(SV_Pins.GetComponent<SimpleScrollSnap>().CurrentPanel).
            transform.GetChild(1).gameObject.SetActive(false);       
    }
}