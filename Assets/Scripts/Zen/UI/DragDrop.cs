using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject GrayLine;
    public GameObject thisScrollView;
    [SerializeField]
    public Canvas thisCanvas;

    private RectTransform rect;
    private RectTransform mThisScrollViewTrans;
    private UIAnimations mUIAnimations;
    private Vector2 beginDrag;
    // where the drop panel is
    private bool isDown = false;

    private void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
        mThisScrollViewTrans = thisScrollView.transform.GetComponent<RectTransform>();
        mUIAnimations = thisScrollView.GetComponent<UIAnimations>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDrag = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDown)
        {
            if (eventData.position.y > beginDrag.y)
            {
                Vector2 distance = eventData.delta / thisCanvas.scaleFactor;

                mThisScrollViewTrans.anchoredPosition += new Vector2(0, distance.y);
            }
        }
        else
        {
            if (eventData.position.y < beginDrag.y)
            {
                Vector2 distance = eventData.delta / thisCanvas.scaleFactor;
                mThisScrollViewTrans.anchoredPosition += new Vector2(0, distance.y);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDown)
        {
            gameObject.transform.Find("btnMoveUp").gameObject.SetActive(false);
            gameObject.transform.Find("btnMoveDown").gameObject.SetActive(true);
            mUIAnimations.TestListScrollViewUp();
            isDown = !isDown;
            GrayLine.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("btnMoveUp").gameObject.SetActive(true);
            gameObject.transform.Find("btnMoveDown").gameObject.SetActive(false);
            mUIAnimations.TestListScrollViewDown();
            isDown = !isDown;
            GrayLine.SetActive(false);
        }
    }
}
