using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script needs to be placed on the UI parent where you want to instantiate prefab.
/// </summary>
public class BirthdayManager : MonoSingleton<BirthdayManager>
{
    [SerializeField] BirthdayScrollView monthScroll;
    [SerializeField] BirthdayScrollView dayScroll;
    [SerializeField] BirthdayScrollView yearScroll;

    [SerializeField] Button btn_Done;

    TMP_Text birthdayTtxt;

    public void CloseBirhdayObj()
    {
        Destroy(gameObject);
    }

    public void BirthdayDone()
    {
        //Debug.Log("Month-- " + (monthScroll.SelectIndex + 1).ToString());
        //Debug.Log("Day-- " + (dayScroll.SelectIndex + 1).ToString());
        //Debug.Log("Year-- " + (yearScroll.SelectIndex + 1900).ToString());

        string birth = (yearScroll.SelectIndex + 1900).ToString() + "-" + (monthScroll.SelectIndex + 1).ToString() + "-" + (dayScroll.SelectIndex + 1).ToString();
        birthdayTtxt = GameObject.Find("ttxt_Birthday").GetComponent<TMP_Text>();
        birthdayTtxt.text = birth;
    }

    public void MonthSelecting()
    {
        Debug.Log("MonthSelecting");
        monthScroll.IsSelecting = true;
        SetBtn_Done();
    }

    public void DaySelecting()
    {
        Debug.Log("DaySelecting");
        dayScroll.IsSelecting = true;
        SetBtn_Done();
    }

    public void YearSelecting()
    {
        Debug.Log("YearSelecting");
        yearScroll.IsSelecting = true;
        SetBtn_Done();
    }

    public void SetBtn_Done()
    {
        if (!monthScroll.IsSelecting && !dayScroll.IsSelecting && !yearScroll.IsSelecting)
        {
            btn_Done.interactable = true;
        }
        else
        {
            btn_Done.interactable = false;
        }
    }
}