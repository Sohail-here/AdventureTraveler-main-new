using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnchor : MonoBehaviour
{
    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 pivot;

    public bool isSet;

    private void Start()
    {
        if (isSet)
        {
            RectTransform rec = gameObject.GetComponent<RectTransform>();

            Vector2 originSize = new Vector2(rec.rect.width, rec.rect.height);
            Vector3 originPos = rec.position;

            rec.anchorMin = anchorMin;
            rec.anchorMax = anchorMax;
            rec.pivot = pivot;

            rec.sizeDelta = originSize;
            rec.position = new Vector3(originPos.x, originPos.y - rec.rect.height / 2, 0);
        }
    }
}