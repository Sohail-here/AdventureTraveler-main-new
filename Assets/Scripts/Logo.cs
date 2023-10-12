using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] CanvasGroup m_Canvas;
    void Start()
    {
        LeanTween.alphaCanvas(m_Canvas, 0, 1f).setDelay(1.5f).setOnComplete(() => gameObject.SetActive(false));
    }
}
