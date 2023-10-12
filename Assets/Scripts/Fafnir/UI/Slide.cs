using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Slide : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    [Header("Content")]
    public GameObject content;

    //nums of nodes
    int nodeCount = 0;
    //width of nodes
    float nodeWidth = 0;

    //change speed
    [Header("SwitchSpeed")]
    public float speed = 12;

    //current node
    int curNode = 0;
    //target node
    int targetNode = 0;

    //Current touch pos
    float curTouchPos = 0;
    //Last touch pos
    float lastTouchPos = 0;
    //Is touch
    bool isTouch = false;

    //Switch target
    bool switchTargetIsLeft = false;
    //Is offset
    bool offsetIsLeft = false;

    //Is arrive
    bool isArrive = true;
     
    //Switch call back
    public Action<int> switchCallback = null;

    //Click
    public void OnBeginDrag(PointerEventData eventData)
    {


        isTouch = true;
        lastTouchPos = eventData.position.x;
        curTouchPos = eventData.position.x;
    }
    //Drag
    public void OnDrag(PointerEventData eventData)
    {


        curTouchPos = eventData.position.x;

        //Set touch pos
        if (lastTouchPos - curTouchPos < 0)
            switchTargetIsLeft = true;
        else if (lastTouchPos - curTouchPos > 0)
            switchTargetIsLeft = false;

        lastTouchPos = eventData.position.x;
    }
    //Untouch
    public void OnEndDrag(PointerEventData eventData)
    {


        isTouch = false;
        isArrive = false;

        UpdateNodeData();

        if (content.GetComponent<RectTransform>().anchoredPosition.x * -1 < curNode * nodeWidth)
            offsetIsLeft = true;
        else
            offsetIsLeft = false;

        if ((switchTargetIsLeft == offsetIsLeft) &&
            ((switchTargetIsLeft && (curNode - 1 >= 0)) || 
            (!switchTargetIsLeft && curNode + 1 <= nodeCount - 1)))
        {
            targetNode = switchTargetIsLeft ? curNode - 1 : curNode + 1;
            curNode = targetNode;

            switchCallback?.Invoke(targetNode);
        }

        else
        {
            targetNode = curNode;
        }
    }

    public int GetCurNodeCount()
    {
        UpdateNodeData();
        return curNode;
    }

    public int GetMaxNodeCount()
    {
        UpdateNodeData();
        return nodeCount;
    }

    public bool ToAppointNode(int appointNode)
    {
        UpdateNodeData();

        if (appointNode < 0 || appointNode >= nodeCount)
        {
            return false;
        }
        isArrive = false;

        if (appointNode != curNode)
            switchCallback?.Invoke(appointNode);

        targetNode = appointNode;
        curNode = targetNode;

        return true;
    }

    public void SubscribeCallback(Action<int> newCallback)
    {


        switchCallback = newCallback;
    }

    public void UnsubscribeCallback()
    {


        switchCallback = null;
    }

    void UpdateNodeData()
    {
        nodeCount = content.transform.childCount;
        nodeWidth = content.GetComponent<RectTransform>().rect.width / nodeCount;
    }


    void Update()
    {
        if (!isTouch && !isArrive)
        {
            Vector2 targetPos = new Vector2(
                 targetNode * nodeWidth * -1,
                 content.GetComponent<RectTransform>().anchoredPosition.y);
            content.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(content.GetComponent<RectTransform>().anchoredPosition, targetPos, Time.deltaTime * speed);

            if (Mathf.Abs(content.GetComponent<RectTransform>().anchoredPosition.x - targetPos.x) <= 0.05)
                isArrive = true;
        }
    }
}
