using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goals : MonoBehaviour
{
    public static Goals goals;

    public Goals()
    {
        goals = this;
    }

    public GameObject UnlockedContent;
    public GameObject AwardsContent;
    public GameObject NoAwards;

    #region Unlocked Objects
    [Header("Unlocked")]
    [SerializeField]
    private GameObject EagerBeaver;
    [SerializeField]
    private GameObject TheExplorer;
    [SerializeField]
    private GameObject HorizonSummit;
    [SerializeField]
    private GameObject GoGetter;
    [SerializeField]
    private GameObject ThePassageOfSilver;
    [SerializeField]
    private GameObject TheExtraMile;
    [SerializeField]
    private GameObject WanderLuster;
    [SerializeField]
    private GameObject TheVibrantNotch;
    [SerializeField]
    private GameObject TravelBug;
    [SerializeField]
    private GameObject ThurlodgePass;
    [SerializeField]
    private GameObject HerculeanRing;
    [SerializeField]
    private GameObject AdventureTraveler;
    #endregion

    #region Award Objects
    [Header("Awards")]
    [SerializeField]
    private GameObject Get_EagerBeaver;
    [SerializeField]
    private GameObject Get_TheExplorer;
    [SerializeField]
    private GameObject Get_HorizonSummit;
    [SerializeField]
    private GameObject Get_GoGetter;
    [SerializeField]
    private GameObject Get_ThePassageOfSilver;
    [SerializeField]
    private GameObject Get_TheExtraMile;
    [SerializeField]
    private GameObject Get_WanderLuster;
    [SerializeField]
    private GameObject Get_TheVibrantNotch;
    [SerializeField]
    private GameObject Get_TravelBug;
    [SerializeField]
    private GameObject Get_ThurlodgePass;
    [SerializeField]
    private GameObject Get_HerculeanRing;
    [SerializeField]
    private GameObject Get_AdventureTraveler;
    #endregion

    #region Award Detail Pages
    [Header("AwardDetails")]
    [SerializeField]
    private GameObject DetailEagerBeaver;
    [SerializeField]
    private GameObject DetailTheExplorer;
    [SerializeField]
    private GameObject DetailHorizonSummit;
    [SerializeField]
    private GameObject DetailGoGetter;
    [SerializeField]
    private GameObject DetailThePassageOfSilver;
    [SerializeField]
    private GameObject DetailTheExtraMile;
    [SerializeField]
    private GameObject DetailWanderLuster;
    [SerializeField]
    private GameObject DetailTheVibrantNotch;
    [SerializeField]
    private GameObject DetailTravelBug;
    [SerializeField]
    private GameObject DetailThurlodgePass;
    [SerializeField]
    private GameObject DetailHerculeanRing;
    [SerializeField]
    private GameObject DetailAdventureTraveler;
    #endregion

    #region Progress Objects
    [Header("ProgressObjects")]
    public GameObject Progress_EagerBeaver;
    public GameObject Progress_JourneyDirector;
    public GameObject Progress_LuminaryIcon;
    public GameObject Progress_AviatorTimer;
    public GameObject Progress_TurboSimmer;
    public GameObject Progress_BrewedPassion;
    public GameObject Progress_WisdomTree;
    public GameObject Progress_Marathon;
    public GameObject Progress_EdgeCutter;
    #endregion

    private void GenerateUnlocked()
    {
        if (!UnDestroyData.awardData.is_unblock_eager_beaver)
        {
            GameObject obj = Instantiate(EagerBeaver, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailEagerBeaver); });
        }

        if (!UnDestroyData.awardData.is_unblock_the_explorer)
        {
            GameObject obj = Instantiate(TheExplorer, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheExplorer); });
        }

        if (!UnDestroyData.awardData.is_unblock_horizon_summit)
        {
            GameObject obj = Instantiate(HorizonSummit, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailHorizonSummit); });
        }

        if (!UnDestroyData.awardData.is_unblock_go_getter)
        {
            GameObject obj = Instantiate(GoGetter, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailGoGetter); });
        }

        if (!UnDestroyData.awardData.is_unblock_passage_of_sliver)
        {
            GameObject obj = Instantiate(ThePassageOfSilver, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailThePassageOfSilver); });
        }

        if (!UnDestroyData.awardData.is_unblock_extra_mile)
        {
            GameObject obj = Instantiate(TheExtraMile, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheExtraMile); });
        }

        if (!UnDestroyData.awardData.is_unblock_wander_luster)
        {
            GameObject obj = Instantiate(WanderLuster, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailWanderLuster); });
        }

        if (!UnDestroyData.awardData.is_unblock_vibrant_notch)
        {
            GameObject obj = Instantiate(TheVibrantNotch, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheVibrantNotch); });
        }

        if (!UnDestroyData.awardData.is_unblock_travel_bug)
        {
            GameObject obj = Instantiate(TravelBug, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTravelBug); });
        }

        if (!UnDestroyData.awardData.is_unblock_thurlodge_pass)
        {
            GameObject obj = Instantiate(ThurlodgePass, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailThurlodgePass); });
        }

        if (!UnDestroyData.awardData.is_unblock_herculean_ring)
        {
            GameObject obj = Instantiate(HerculeanRing, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailHerculeanRing); });
        }

        if (!UnDestroyData.awardData.is_unblock_adventure_traveller)
        {
            GameObject obj = Instantiate(AdventureTraveler, UnlockedContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailAdventureTraveler); });
        }
    }

    private void SortUnlocked()
    {
        int contentCount = UnlockedContent.transform.childCount;

        if (contentCount > 9)
        {
            UnlockedContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 2380);
        }
        else if (contentCount > 6)
        {
            UnlockedContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1840);
        }
        else
        {
            UnlockedContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1265);
        }

        for (int i = 0; i < contentCount; i++)
        {
            float deltaX = (i % 3) * 415;
            float deltaY = (i / 3) * 570;

            UnlockedContent.transform.GetChild(i).gameObject.transform.localPosition += new Vector3(deltaX, -deltaY, 0);
        }
    }

    private void GenerateAward()
    {
        if (UnDestroyData.awardData.is_unblock_eager_beaver)
        {
            GameObject obj = Instantiate(Get_EagerBeaver, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailEagerBeaver); });
            DetailEagerBeaver.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_the_explorer)
        {
            GameObject obj = Instantiate(Get_TheExplorer, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheExplorer); });
            DetailTheExplorer.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_horizon_summit)
        {
            GameObject obj = Instantiate(Get_HorizonSummit, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailHorizonSummit); });
            DetailHorizonSummit.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }


        if (UnDestroyData.awardData.is_unblock_go_getter)
        {
            GameObject obj = Instantiate(Get_GoGetter, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailGoGetter); });
            DetailGoGetter.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_passage_of_sliver)
        {
            GameObject obj = Instantiate(Get_ThePassageOfSilver, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailThePassageOfSilver); });
            DetailThePassageOfSilver.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_extra_mile)
        {
            GameObject obj = Instantiate(Get_TheExtraMile, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheExtraMile); });
            DetailTheExtraMile.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_wander_luster)
        {
            GameObject obj = Instantiate(Get_WanderLuster, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailWanderLuster); });
            DetailWanderLuster.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_vibrant_notch)
        {
            GameObject obj = Instantiate(Get_TheVibrantNotch, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTheVibrantNotch); });
            DetailTheVibrantNotch.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_travel_bug)
        {
            GameObject obj = Instantiate(Get_TravelBug, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailTravelBug); });
            DetailTravelBug.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_thurlodge_pass)
        {
            GameObject obj = Instantiate(Get_ThurlodgePass, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailThurlodgePass); });
            DetailThurlodgePass.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_herculean_ring)
        {
            GameObject obj = Instantiate(Get_HerculeanRing, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailHerculeanRing); });
            DetailHerculeanRing.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }

        if (UnDestroyData.awardData.is_unblock_adventure_traveller)
        {
            GameObject obj = Instantiate(Get_AdventureTraveler, AwardsContent.transform);
            obj.GetComponent<Button>().onClick.AddListener(delegate () { OpenDetail(DetailAdventureTraveler); });
            DetailAdventureTraveler.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            NoAwards.SetActive(false);
        }
    }

    private void SortAwards()
    {
        int contentCount = AwardsContent.transform.childCount;

        Debug.Log(contentCount);

        if (contentCount > 3)
        {
            AwardsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            Debug.Log("Content Count > 3");
        }
        /*else
        {
            AwardsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(530 * (contentCount - 2), 0);
            Debug.Log("Content Count <= 3");
        }*/

        for (int i = 0; i < contentCount; i++)
        {
            Debug.Log("Test i " + i);

            float deltaX = i * 530;

            AwardsContent.transform.GetChild(i).gameObject.transform.localPosition += new Vector3(deltaX, 0, 0);
        }
    }

    private void LoadProgress()
    {
        Progress_EagerBeaver.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_eager_beaver + "/" + UnDestroyData.progressData.total_eager_beaver;
        Progress_EagerBeaver.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_eager_beaver / (float)UnDestroyData.progressData.total_eager_beaver;

        Progress_JourneyDirector.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_journey_director + "/" + UnDestroyData.progressData.total_journey_director;
        Progress_JourneyDirector.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_journey_director / (float)UnDestroyData.progressData.total_journey_director;

        Progress_LuminaryIcon.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_luminary_icon + "/" + UnDestroyData.progressData.total_luminary_icon;
        Progress_LuminaryIcon.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_luminary_icon / (float)UnDestroyData.progressData.total_luminary_icon;

        Progress_AviatorTimer.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_aviator_timer + "/" + UnDestroyData.progressData.total_aviator_timer;
        Progress_AviatorTimer.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_aviator_timer / (float)UnDestroyData.progressData.total_aviator_timer;

        Progress_TurboSimmer.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_turbo_simmer + "/" + UnDestroyData.progressData.total_turbo_simmer;
        Progress_TurboSimmer.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_turbo_simmer / (float)UnDestroyData.progressData.total_turbo_simmer;

        Progress_BrewedPassion.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_brewed_passion + "/" + UnDestroyData.progressData.total_brewed_passion;
        Progress_BrewedPassion.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_brewed_passion / (float)UnDestroyData.progressData.total_brewed_passion;

        Progress_WisdomTree.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_wisdom_tree + "/" + UnDestroyData.progressData.total_wisdom_tree;
        Progress_WisdomTree.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_wisdom_tree / (float)UnDestroyData.progressData.total_wisdom_tree;

        Progress_Marathon.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_marathon + "/" + UnDestroyData.progressData.total_marathon;
        Progress_Marathon.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_marathon / (float)UnDestroyData.progressData.total_marathon;

        Progress_EdgeCutter.transform.GetChild(3).
            transform.GetChild(1).
            GetComponent<TMP_Text>().text = UnDestroyData.progressData.done_edge_cutter + "/" + UnDestroyData.progressData.total_edge_cutter;
        Progress_EdgeCutter.transform.GetChild(3).
            transform.GetChild(0).
            transform.GetChild(0).
            GetComponent<Image>().fillAmount = (float)UnDestroyData.progressData.done_edge_cutter / (float)UnDestroyData.progressData.total_edge_cutter;
    }

    private void OpenDetail(GameObject detail)
    {
        detail.SetActive(true);
        Debug.Log("OpenDetail" + detail.name);
    }

    public void LoadGoals(System.Action onComplete)
    {
        StartCoroutine(IE_LoadGoals(onComplete));
    }

    IEnumerator IE_LoadGoals(System.Action onComplete)
    {
        GoalsInitial();

        yield return urlGetAward.getAward.urlGET_GetAward();
        yield return urlGetProgress.getProgress.urlGET_GetProgress();
        GenerateUnlocked();
        //SortUnlocked();
        GenerateAward();
        //SortAwards();
        LoadProgress();
        AwardDetail.instance.LoadDetailObjects();
        onComplete();
    }

    public void GoalsInitial()
    {
        foreach (Transform child in UnlockedContent.transform)
        {
            Destroy(child.gameObject);
        }

        //UnlockedContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1302, 798);

        foreach (Transform child in AwardsContent.transform)
        {
            Destroy(child.gameObject);
        }

        //AwardsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1302, 2380);
    }
}