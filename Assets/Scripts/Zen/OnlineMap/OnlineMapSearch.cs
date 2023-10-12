using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class OnlineMapSearch : MonoBehaviour
{
    public static OnlineMapSearch instance;
    public OnlineMapSearch()
    {
        instance = this;
    }

    public GameObject locationName;
    public GameObject scrollView;
    public GameObject Content;
    public GameObject Prefab;

    public string searchName;

    public string googleAPIKey;

    public bool addMaker = true;
    public bool logResponse = true;
    public bool setPosition = true;
    public bool setZoom = true;

    public UnityEvent<Vector2> OnFindLocationCompleteEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(googleAPIKey))
        {
            Debug.LogWarning("Please specify Google API Key");
        }
    }

    private void OnFindLocationComplete(string result)
    {
        if (logResponse)
        {
            Debug.Log(result);
        }

        Vector2 position = OnlineMapsGoogleGeocoding.GetCoordinatesFromResult(result);

        if (position != Vector2.zero)
        {
            if (addMaker)
            {
                OnlineMapsMarkerManager.CreateItem(position, searchName);
            }

            if (setZoom)
            {
                OnlineMapsXML xml = OnlineMapsXML.Load(result);

                OnlineMapsXML bounds = xml.Find("//geometry/viewport");
                if (!bounds.isNull)
                {
                    OnlineMapsXML southwest = bounds["southwest"];
                    OnlineMapsXML northeast = bounds["northeast"];

                    Vector2 sw = OnlineMapsXML.GetVector2FromNode(southwest);
                    Vector2 ne = OnlineMapsXML.GetVector2FromNode(northeast);

                    Vector2 center;
                    int zoom;
                    OnlineMapsUtils.GetCenterPointAndZoom(new[] { sw, ne }, out center, out zoom);

                    OnlineMaps.instance.zoom = zoom;
                }
            }

            if (setPosition)
            {
                OnlineMaps.instance.position = position;
            }

            OnFindLocationCompleteEvent.Invoke(position);
        }
        else
        {
            Debug.Log("Oops... Something is wrong.");
        }
    }

    public void SearchForName()
    {
        OnlineMapsGoogleGeocoding request = new OnlineMapsGoogleGeocoding(searchName, googleAPIKey);

        request.Send();

        request.OnComplete += OnFindLocationComplete;

        scrollView.SetActive(false);
    }

    private void OnComplete(string s)
    {
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
        }

        OnlineMapsGooglePlacesAutocompleteResult[] results = OnlineMapsGooglePlacesAutocomplete.GetResults(s);

        if (results == null)
        {
            Debug.Log("Error");
            Debug.Log(s);
            return;
        }

        int j = 0;

        foreach (OnlineMapsGooglePlacesAutocompleteResult result in results)
        {
            Debug.Log(result.description);           

            if (j > 5)
            {
                Debug.Log("J > 5");
                Content.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 160 * (j - 5));
            }

            GameObject prefab = Prefab;
            
            Instantiate(prefab, Content.transform.position + new Vector3(0, 0 - (j * 155), 0), Content.transform.rotation, Content.transform);
            //prefab.transform.GetChild(0).GetComponent<TMP_Text>().text = result.description;
            prefab.transform.GetChild(0).GetComponent<Text>().text = result.description;
            
            /*
            if (result.description.Length > 24)
            {
                prefab.transform.GetChild(0).GetComponent<TMP_Text>().fontSize = 48;
            }
            else
            {
                prefab.transform.GetChild(0).GetComponent<TMP_Text>().fontSize = 72;
            }
            j++;
            */
        }
    }

    public void SearchComplete()
    {
        scrollView.SetActive(true);

        if (locationName.GetComponent<TMP_InputField>().text == "")
        {
            scrollView.SetActive(false);
        }

        OnlineMapsGooglePlacesAutocomplete.Find(
            locationName.GetComponent<TMP_InputField>().text,
            googleAPIKey, types:"geocode"
            
            ).OnComplete += OnComplete;
    }

    public void CloseSearch()
    {
        locationName.GetComponent<TMP_InputField>().text = "";
        scrollView.SetActive(false);
    }
}