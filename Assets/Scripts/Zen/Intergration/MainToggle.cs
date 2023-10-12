using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainToggle : MonoSingleton<MainToggle>
{
    [Header("Toggles")]
    public Toggle tgl_Home;
    public Toggle tgl_Activity;
    public Toggle tgl_Record;
    public Toggle tgl_Goals;
    public Toggle tgl_Account;

    [Header("Home")]
    public GameObject isOnTxtHome;
    public GameObject isOnObjHome;

    [Header("Acrivity")]
    public GameObject isOnTxtActivity;
    public GameObject isOnObjActivity;

    [Header("Record")]
    public GameObject isOnTxtRecord;
    public GameObject isOnObjRecord;

    [Header("Goals")]
    public GameObject isOnTxtGoals;
    public GameObject isOnObjGoals;
    public GameObject DefaultPanel;

    [Header("Account")]
    public GameObject isOnTxtAccount;
    public GameObject isOnObjAccount;

    [Header("Universal")]
    public GameObject MainMap;
    public GameObject MapCaream;
    public GameObject LoginPanel;

    void Start()
    {
        tgl_Home.onValueChanged.AddListener((bool value) => SwitchHome(tgl_Home, value));
        tgl_Activity.onValueChanged.AddListener((bool value) => SwitchActivity(tgl_Activity, value));
        tgl_Record.onValueChanged.AddListener((bool value) => SwitchRecord(tgl_Record, value));
        tgl_Goals.onValueChanged.AddListener((bool value) => SwitchGoals(tgl_Goals, value));
        tgl_Account.onValueChanged.AddListener((bool value) => SwitchAccount(tgl_Account, value));
    }

    private void SwitchHome(Toggle home, bool value)
    {
        ToggleSwitchObj(home, isOnTxtHome);
        ToggleSwitchObj(home, isOnObjHome);
        UnToggleSwitchObj(home, MainMap);
        UnToggleSwitchObj(home, MapCaream);

        tgl_Goals.interactable = true;

        if (!UnDestroyData.IsGusetLogin && home.isOn)
        {
            Home.Instance.Refresh();
        }
    }

    private void SwitchActivity(Toggle activity, bool value)
    {
        ToggleSwitchObj(activity, isOnTxtActivity);
        ToggleSwitchObj(activity, isOnObjActivity);
        //ToggleSwitchObj(activity, MainMap);
        //ToggleSwitchObj(activity, MapCaream);

        if (tgl_Activity.isOn) ActivityManager.Instance.Refresh();

        tgl_Goals.interactable = true;
    }

    private void SwitchRecord(Toggle record, bool value)
    {
        ToggleSwitchObj(record, isOnTxtRecord);
        ToggleSwitchObj(record, isOnObjRecord);
        //ToggleSwitchObj(record, MainMap);
        //ToggleSwitchObj(record, MapCaream);

        if (value)
            RecordManager.Instance?.CancelRecord();
        tgl_Goals.interactable = true;
    }

    public void SwitchGoals(Toggle goals, bool value)
    {
        DefaultPanel.SetActive(true);

        ToggleSwitchObj(goals, isOnTxtGoals);
        ToggleSwitchObj(goals, isOnObjGoals);
        UnToggleSwitchObj(goals, MainMap);

        if (!UnDestroyData.IsGusetLogin && goals.isOn)
        {
            goals.interactable = false;
            Goals.goals.LoadGoals(() => goals.interactable = true);
        }
    }

    private void SwitchAccount(Toggle account, bool value)
    {
        ToggleSwitchObj(account, isOnTxtAccount);
        ToggleSwitchObj(account, isOnObjAccount);
        UnToggleSwitchObj(account, MainMap);
        UnToggleSwitchObj(account, MapCaream);
        tgl_Goals.interactable = true;
    }

    private void ToggleSwitchObj(Toggle toggle, GameObject obj)
    {
        if (toggle.isOn)
        {
            if (UnDestroyData.IsGusetLogin)
            {
                LoginPanel.SetActive(obj.name != "Activity");
                obj.SetActive(obj.name == "Activity");
            }
            else
            {
                if (obj.activeInHierarchy == false)
                    obj.SetActive(true);
            }
        }
        else
        {
            if (obj.activeInHierarchy)
                obj.SetActive(false);
        }
    }

    private void UnToggleSwitchObj(Toggle toggle, GameObject obj)
    {
        if (!toggle.isOn)
        {
            if (obj.activeInHierarchy == false)
                obj.SetActive(true);
        }
        else
        {
            if (obj.activeInHierarchy)
                obj.SetActive(false);
        }
    }
}