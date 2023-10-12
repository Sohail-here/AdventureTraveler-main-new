using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFix : MonoBehaviour
{
    public GameObject[] toggles;

    /*void Start()
    {
        float height = toggles[0].transform.GetComponent<RectTransform>().rect.height;
        float width =
            (
                toggles[0].transform.GetComponent<RectTransform>().rect.width +
                toggles[1].transform.GetComponent<RectTransform>().rect.width
            ) / 2;

        float posX = (toggles[0].transform.localPosition.x + toggles[1].transform.localPosition.x) / 2;
        float posY = toggles[0].transform.localPosition.y;

        gameObject.transform.localPosition = new Vector3(posX, 0, 0);

        gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(height, width);
    }*/

    public void AllSwitch()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].GetComponent<Toggle>().isOn = true;
            }
        }
        else
        {
            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].GetComponent<Toggle>().isOn = false;
            }
        }
    }
}