using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicScollView : MonoBehaviour
{
    public GameObject ContentPrefab;
    public GameObject[] Contents;
    public int ContentsQuantity;
    public float TopSpace;
    public float BelowSpace;
    public float PrefabsSpace;

    private GameObject Content;

    // Start is called before the first frame update
    void Start()
    {
        Content = gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject;

        GeneratePrefabs();
        
        SetPrefabsAnchor();
        ContentInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GeneratePrefabs()
    {
        for (int i = 0; i < ContentsQuantity; i++)
        {
            Instantiate(ContentPrefab, Content.transform.position, Quaternion.identity);
        }
    }

    private void SetPrefabsAnchor()
    {
        for (int i = 0; i < Contents.Length; i++)
        {
            Contents[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            Contents[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            Contents[i].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        }
    }

    private void ContentInit()
    {
        Content.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        Content.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        Content.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);

        float prefabWidth = ContentPrefab.GetComponent<RectTransform>().rect.width;
        float prefabHeight = TopSpace + (ContentPrefab.GetComponent<RectTransform>().rect.height + PrefabsSpace) * Contents.Length + BelowSpace;
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(prefabWidth, prefabHeight);
    }

    private void SetPrefabsPos()
    {
        
    }
}