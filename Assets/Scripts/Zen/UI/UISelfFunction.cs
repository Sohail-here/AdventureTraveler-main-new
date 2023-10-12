using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DanielLochner.Assets.SimpleScrollSnap;

public class UISelfFunction : MonoBehaviour
{
    public GameObject LoginPanel;
    #region Toggle

    /// <summary>
    /// The paremeter "obj" will setActive when the toggle is switched
    /// </summary>
    /// <param name="obj"></param>
    public void ToggleSwitchObj(GameObject obj)
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            if (UnDestroyData.IsGusetLogin)
            {
                LoginPanel.SetActive(obj.name != "Activity");
                obj.SetActive(obj.name == "Activity");
            }
            else
            {
                obj.SetActive(true);
            }
        }
        else
        {
            obj.SetActive(false);
        }
    }

    public void UnToggleSwitchObj(GameObject obj)
    {
        if (!gameObject.GetComponent<Toggle>().isOn)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }

    public void ToggleSwitchText(GameObject obj)
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            obj.GetComponent<TMP_Text>().text = gameObject.name;
        }
    }

    #endregion

    public void ButtonEffect()
    {

    }

    #region GPS

    public GameObject warnPanel;
    public Text warnText;

    public void GetGpsAuth(GameObject GpsObj)
    {
        if (!Input.location.isEnabledByUser)
        {
            warnPanel.SetActive(true);
        }
        else
        {
            ToggleSwitchObj(GpsObj);
        }
    }

    #endregion

    public void ScrollSnapSelecter(GameObject Content)
    {
        Debug.Log(Content.transform.GetChild(gameObject.GetComponent<SimpleScrollSnap>().CurrentPanel).name);
        UnDestroyData.journeyData.category = Content.transform.GetChild(gameObject.GetComponent<SimpleScrollSnap>().CurrentPanel).name;
    }

    public void SelfAnimationPlay(string animationName)
    {
        gameObject.GetComponent<Animator>().Play(animationName);
    }

    public void SearchName()
    {
        //name = gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text;
        name = gameObject.transform.GetChild(0).GetComponent<Text>().text;
        OnlineMapSearch.instance.searchName = name;
        OnlineMapSearch.instance.SearchForName();
    }

    public void OpenTarget(GameObject target)
    {
        if (UnDestroyData.IsGusetLogin && target.name != "Activity")
        {
            LoginPanel.SetActive(true);
        }
        else
        {
            target.SetActive(true);
        }
    }

    public void CloseTarget(GameObject target)
    {
        target.SetActive(false);
    }

    #region Scene loads

    public void RefreshHome()
    {
        if (!UnDestroyData.IsGusetLogin && gameObject.GetComponent<Toggle>().isOn)
        {
            Home.Instance.Refresh();
        }
    }

    public void OpenGoals()
    {
        if (!UnDestroyData.IsGusetLogin && gameObject.GetComponent<Toggle>().isOn)
        {
            gameObject.GetComponent<Toggle>().interactable = false;
            Goals.goals.LoadGoals(() => gameObject.GetComponent<Toggle>().interactable = true);
        }
    }

    #endregion

    #region Language

    public void SwitchToEnglish()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            LanguageManager.Instance.Mode = "English";
        }
    }

    public void SwitchToSpanish()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            LanguageManager.Instance.Mode = "Spanish";
        }
    }

    public void SwitchToFrench()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            LanguageManager.Instance.Mode = "French";
        }
    }

    #endregion
}