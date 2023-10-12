  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DragModel : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public GameObject gobj;
    public Transform canvas;

    private Vector2 _originalPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas as RectTransform, Input.mousePosition, null, out _originalPos);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas as RectTransform, Input.mousePosition, null, out pos);
        Vector2 offect = pos - _originalPos;
        transform.localPosition = offect;
    }

    private ARRaycastManager rayCastMgr;
    private Pose placementPose;

    void Start()
    {
        rayCastMgr = FindObjectOfType<ARRaycastManager>();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        rayCastMgr.Raycast(screenCenter, hits, TrackableType.Planes);
        if (Physics.Raycast(ray, out hit) && hits.Count > 0)
        {
            placementPose = hits[0].pose;
            Instantiate(gobj, new Vector3(hit.point.x, placementPose.position.y, hit.point.z), transform.rotation);
        }
        transform.localPosition = Vector2.zero;
    }
}
