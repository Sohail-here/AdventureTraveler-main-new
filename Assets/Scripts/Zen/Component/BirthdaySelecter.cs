using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DanielLochner.Assets.SimpleScrollSnap;

public class BirthdaySelecter : MonoBehaviour
{
    public GameObject Selecter;

    [Header("ScrollSnaps")]
    public GameObject Month;
    public GameObject Day;
    public GameObject Year;

    [Header("YearObjects")]
    public GameObject prefab;
    public GameObject YearContent;
    public int YearMin;
    public int YearMax;

    [Header("TitleTexts")]
    public GameObject MonthTxt;
    public GameObject DayTxt;
    public GameObject YearTxt;
    [SerializeField]
    private float titlePosY;

    private void Awake()
    {
        GenerateYear();
    }

    // Start is called before the first frame update
    void Start()
    {
        PositionInit();
    }

    /// <summary>
    /// Pos the components in the middle of window,
    /// and the title on the top of window.
    /// </summary>
    private void PositionInit()
    {
        Debug.Log("Birthday Position Init");

        float width = Screen.width;
        float height = Selecter.GetComponent<RectTransform>().rect.height;

        Month.transform.position = new Vector3(0.125f * width, height / 2, 0);
        Day.transform.position = new Vector3(0.375f * width, height / 2, 0);
        Year.transform.position = new Vector3(0.75f * width, height / 2, 0);

        MonthTxt.transform.position = new Vector3(0.125f * width, titlePosY, 0);
        DayTxt.transform.position = new Vector3(0.375f * width, titlePosY, 0);
        YearTxt.transform.position = new Vector3(0.75f * width, titlePosY, 0);
    }

    private void GenerateYear()
    {
        for (int i = YearMin; i < YearMax + 1; i++)
        {
            Debug.Log(i);

            Instantiate(prefab, YearContent.transform);
            prefab.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
        }
    }

    private string BirthdayString()
    {
        string year;
        if (Year.GetComponent<SimpleScrollSnap>().CurrentPanel == 0)
        {
            year = YearMax.ToString();
        }
        else
        {
            year = (YearMin + Year.GetComponent<SimpleScrollSnap>().CurrentPanel - 1).ToString();
        }

        string month;
        if (Month.GetComponent<SimpleScrollSnap>().CurrentPanel + 1 >= 10)
        {
            month = (Month.GetComponent<SimpleScrollSnap>().CurrentPanel + 1).ToString();
        }
        else
        {
            month = "0" + (Month.GetComponent<SimpleScrollSnap>().CurrentPanel + 1).ToString();
        }

        string day;
        if (Day.GetComponent<SimpleScrollSnap>().CurrentPanel + 1 >= 10)
        {
            day = (Day.GetComponent<SimpleScrollSnap>().CurrentPanel + 1).ToString();
        }
        else
        {
            day = "0" + (Day.GetComponent<SimpleScrollSnap>().CurrentPanel + 1).ToString();
        }

        string birthday =
            year + "-" + month + "-" + day;

        return birthday;
    }

    public void GetBirthday(GameObject target)
    {
        target.GetComponent<TMP_Text>().text = BirthdayString();
        Debug.Log(BirthdayString());
    }
}