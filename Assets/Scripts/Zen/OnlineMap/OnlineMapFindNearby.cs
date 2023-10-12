using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMapFindNearby : MonoBehaviour
{
    public string API_Key;

    // Unit : km
    [SerializeField]
    private int Radius;
    [SerializeField]
    private string Types;

    public void SearchNearby()
    {
        OnlineMapsGooglePlaces.FindNearby(
                API_Key,
                new OnlineMapsGooglePlaces.NearbyParams(OnlineMaps.instance.position.x, OnlineMaps.instance.position.y, Radius)
                {
                    types = Types
                }
            ).OnComplete += OnComplete;
    }

    private void OnComplete(string s)
    {
        OnlineMapsGooglePlacesResult[] results = OnlineMapsGooglePlaces.GetResults(s);

        if (results == null)
        {
            Debug.Log("Error");
            Debug.Log(s);
            return;
        }

        List<OnlineMapsMarker> markers = new List<OnlineMapsMarker>();

        foreach (OnlineMapsGooglePlacesResult result in results)
        {
            Debug.Log("resultName--" + result.name + "--resultName");
            Debug.Log("location--" + result.location.ToString() + "--location");

            OnlineMapsMarker marker = OnlineMapsMarkerManager.CreateItem(result.location, result.name);
            markers.Add(marker);
        }

        Vector2 center;
        int zoom;
        OnlineMapsUtils.GetCenterPointAndZoom(markers.ToArray(), out center, out zoom);

        OnlineMaps.instance.position = center;
        OnlineMaps.instance.zoom = zoom + 1;
    }
}
