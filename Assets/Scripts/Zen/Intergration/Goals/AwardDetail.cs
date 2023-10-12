using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AwardDetail : MonoBehaviour
{
    public static AwardDetail instance;

    public AwardDetail()
    {
        instance = this;
    }

    [Header("AviatorTimer--------")]
    public GameObject[] AviatorTimerUnlocked;
    public GameObject[] AviatorTimerFill;
    public GameObject[] AviatorText;
    [Header("BrewedPassion--------")]
    public GameObject[] BrewedPassionUnlocked;
    public GameObject[] BrewedPassionFill;
    public GameObject[] BrewedPassionText;
    [Header("EagerBeaver--------")]
    public GameObject[] EagerBeaverUnlocked;
    public GameObject[] EagerBeaverFill;
    public GameObject[] EagerBeaverText;
    [Header("EdgeCutter--------")]
    public GameObject[] EdgeCutterUnlocked;
    public GameObject[] EdgeCutterFill;
    public GameObject[] EdgeCutterText;
    [Header("JourneyDirector--------")]
    public GameObject[] JourneyDirectorUnlocked;
    public GameObject[] JourneyDirectorFill;
    public GameObject[] JourneyText;
    [Header("LuminaryIcon--------")]
    public GameObject[] LuminaryIconUnlocked;
    public GameObject[] LuminaryIconFill;
    public GameObject[] LuminaryText;
    [Header("Marathon--------")]
    public GameObject[] MarathonUnlocked;
    public GameObject[] MarathonFill;
    public GameObject[] MarathonText;
    [Header("TurboSimmer--------")]
    public GameObject[] TurboSimmerUnlocked;
    public GameObject[] TurboSimmerFill;
    public GameObject[] TurboSimmerText;
    [Header("AviatorTimer--------")]
    public GameObject[] WisdomTreeUnlocked;
    public GameObject[] WisdomTreeFill;
    public GameObject[] WisdomTreeText;

    public void LoadDetailObjects()
    {
        if (UnDestroyData.progressData.is_done_aviator_timer)
        {
            for (int i = 0; i < AviatorTimerUnlocked.Length; i++)
            {
                AviatorTimerUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < AviatorTimerUnlocked.Length; i++)
        {
            AviatorTimerFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_aviator_timer / (float)UnDestroyData.progressData.total_aviator_timer;
            AviatorText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_aviator_timer + "/" + UnDestroyData.progressData.total_aviator_timer;
        }

        if (UnDestroyData.progressData.is_done_brewed_passion)
        {
            for (int i = 0; i < BrewedPassionUnlocked.Length; i++)
            {
                BrewedPassionUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < BrewedPassionUnlocked.Length; i++)
        {
            BrewedPassionFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_brewed_passion / (float)UnDestroyData.progressData.total_brewed_passion;
            BrewedPassionText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_brewed_passion + "/" + UnDestroyData.progressData.total_brewed_passion;
        }

        Debug.Log("-------------eager-----------" + UnDestroyData.progressData.is_done_eager_beaver);
        if (UnDestroyData.progressData.is_done_eager_beaver)
        {
            for (int i = 0; i < EagerBeaverUnlocked.Length; i++)
            {
                Debug.Log("EagerBeaverUnlockedSetTrue");
                EagerBeaverUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < EagerBeaverUnlocked.Length; i++)
        {
            EagerBeaverFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_eager_beaver / (float)UnDestroyData.progressData.total_eager_beaver;
            EagerBeaverText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_eager_beaver + "/" + UnDestroyData.progressData.total_eager_beaver;
        }

        if (UnDestroyData.progressData.is_done_edge_cutter)
        {
            for (int i = 0; i < EagerBeaverUnlocked.Length; i++)
            {
                EdgeCutterUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < EdgeCutterUnlocked.Length; i++)
        {
            EdgeCutterFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_edge_cutter / (float)UnDestroyData.progressData.total_edge_cutter;
            EdgeCutterText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_edge_cutter + "/" + UnDestroyData.progressData.total_edge_cutter;
        }

        if (UnDestroyData.progressData.is_done_journey_director)
        {
            for (int i = 0; i < JourneyDirectorUnlocked.Length; i++)
            {
                JourneyDirectorUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < JourneyDirectorUnlocked.Length; i++)
        {
            JourneyDirectorFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_journey_director / (float)UnDestroyData.progressData.total_journey_director;
            JourneyText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_journey_director + "/" + UnDestroyData.progressData.total_journey_director;
        }

        if (UnDestroyData.progressData.is_done_luminary_icon)
        {
            for (int i = 0; i < LuminaryIconUnlocked.Length; i++)
            {
                LuminaryIconUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < LuminaryIconUnlocked.Length; i++)
        {
            LuminaryIconFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_luminary_icon / (float)UnDestroyData.progressData.total_luminary_icon;
            LuminaryText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_luminary_icon + "/" + UnDestroyData.progressData.total_luminary_icon;
        }

        if (UnDestroyData.progressData.is_done_marathon)
        {
            for (int i = 0; i < MarathonUnlocked.Length; i++)
            {
                MarathonUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < MarathonUnlocked.Length; i++)
        {
            MarathonFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_marathon / (float)UnDestroyData.progressData.total_marathon;
            MarathonText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_marathon + "/" + UnDestroyData.progressData.total_marathon;
        }

        if (UnDestroyData.progressData.is_done_turbo_simmer)
        {
            for (int i = 0; i < TurboSimmerUnlocked.Length; i++)
            {
                TurboSimmerUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < TurboSimmerUnlocked.Length; i++)
        {
            TurboSimmerFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_turbo_simmer / (float)UnDestroyData.progressData.total_turbo_simmer;
            TurboSimmerText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_turbo_simmer + "/" + UnDestroyData.progressData.total_turbo_simmer;
        }

        if (UnDestroyData.progressData.is_done_wisdom_tree)
        {
            for (int i = 0; i < WisdomTreeUnlocked.Length; i++)
            {
                WisdomTreeUnlocked[i].SetActive(true);
            }
        }
        for (int i = 0; i < WisdomTreeUnlocked.Length; i++)
        {
            WisdomTreeFill[i].GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_wisdom_tree / (float)UnDestroyData.progressData.total_wisdom_tree;
            WisdomTreeText[i].GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_wisdom_tree + "/" + UnDestroyData.progressData.total_wisdom_tree;
        }
    }
}