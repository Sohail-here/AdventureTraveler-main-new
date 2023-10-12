using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeTest : MonoBehaviour
{
    public GameObject GlobalContent;
    public GameObject prefab;

    public Text DebugText;

    public int PrefabNums;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGlobalPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGlobalPrefabs()
    {
        for (int i = 0; i < PrefabNums; i++)
        {
            GlobalContent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 950);

            Instantiate(prefab,
                GlobalContent.transform.position + new Vector3(0, -92 - i * 950, 0),
                GlobalContent.transform.rotation,
                GlobalContent.transform);

            DebugText.text = i.ToString();
        }
    }
}