using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelRemove : MonoBehaviour
{
    private void Update()
    {
        PanelMove(gameObject);
    }

    public void PanelMove(GameObject panel)
    {
        Vector2 touchPos;

        if (Input.touchCount > 0)
        {
            Debug.Log("Touch 1");

            touchPos = Input.GetTouch(0).deltaPosition;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

                panel.transform.Translate(touchPos.x * 0.1f, 0, 0);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                if (touchPos.x > (Screen.width / 3))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}