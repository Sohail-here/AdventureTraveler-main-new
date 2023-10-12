using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAnimations : MonoBehaviour
{
    [Header("Stick")]
    public float SpeedTime;
    public float MovePos;

    [Header("Fade")]
    public bool FadeOut;
    public float FadeTime;
    public GameObject MoveDonwBtn;
    public GameObject MoveUpBtn;
    public RectTransform SVListPanel;

    [Header("SwtichCaptureMode")]
    public TMP_Text Txt_photo;
    public TMP_Text Txt_video;
    public RawImage Rimg_rightVector;
    public RawImage Rimg_leftVector;
    public GameObject CaptureObj;

    [Header("LoadingBar")]
    public bool IsLoading;
    public float LoadingSpeed;
    public float LoadingPos;

    void Start()
    {
        if (FadeOut)
        {
            FadeToLevel(FadeTime);
        }
    }

    #region Switch capture mode

    public void A_SwtichCaptureMode()
    {
        // Set button to be uninteractable, avoid the animation being confused.
        gameObject.GetComponent<Button>().interactable = false;

        Photo_Video.Instance.SwitchCaptureMode();
        LeanTween.moveLocalX(CaptureObj, Photo_Video.Instance.isPhoto ? 118 : -118, 0.25f).setOnComplete(() =>
       {
           Txt_photo.gameObject.SetActive(Photo_Video.Instance.isPhoto);
           Txt_video.gameObject.SetActive(!Photo_Video.Instance.isPhoto);

           Rimg_leftVector.gameObject.SetActive(!Photo_Video.Instance.isPhoto);
           Rimg_rightVector.gameObject.SetActive(Photo_Video.Instance.isPhoto);

           gameObject.GetComponent<Button>().interactable = true;
       });
    }

    public void InitCaptureMode()
    {
        gameObject.GetComponent<Button>().interactable = false;
        LeanTween.moveLocalX(CaptureObj, 118, 0.25f).setOnComplete(() =>
        {
            Txt_photo.gameObject.SetActive(true);
            Txt_video.gameObject.SetActive(false);

            Rimg_leftVector.gameObject.SetActive(true);
            Rimg_rightVector.gameObject.SetActive(false);

            gameObject.GetComponent<Button>().interactable = true;
        });
    }

    #endregion

    #region Fade out

    public void FadeStart()
    {
        LeanTween.alpha(gameObject, 0, 1).setEase(LeanTweenType.linear).setOnComplete(FadeFinished);
    }

    private void FadeFinished()
    {
        LeanTween.alpha(gameObject, 1, 1).setEase(LeanTweenType.linear).setOnComplete(FadeStart);
    }

    public void FadeToLevel(float time)
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, time).setOnComplete(CloseSelf);
    }

    #endregion

    private void CloseSelf()
    {
        gameObject.SetActive(false);
    }

    public void StickLtoR()
    {
        LeanTween.moveLocalX(gameObject, MovePos, SpeedTime).setLoopOnce();
    }

    public void StickRtoL()
    {
        LeanTween.moveLocalX(gameObject, -MovePos, SpeedTime).setLoopOnce();
    }

    #region LoadingBar

    private bool isLeft = true;

    public void StartLoading()
    {
        gameObject.GetComponent<RawImage>().enabled = true;
        IsLoading = true;
        StartCoroutine(LoadingMove());
    }

    public void StopLoading()
    {
        gameObject.GetComponent<RawImage>().enabled = false;
        IsLoading = false;
        if (!isLeft)
        {
            LeanTween.moveLocalX(gameObject, -LoadingPos, LoadingSpeed).setLoopOnce();
        }
    }

    IEnumerator LoadingMove()
    {
        while (IsLoading)
        {
            if (isLeft)
            {
                LeanTween.moveLocalX(gameObject, LoadingPos, LoadingSpeed).setLoopOnce();
                yield return new WaitForSeconds(2);
                isLeft = !isLeft;
            }
            else
            {
                LeanTween.moveLocalX(gameObject, -LoadingPos, LoadingSpeed).setLoopOnce();
                yield return new WaitForSeconds(2);
                isLeft = !isLeft;
            }
        }
    }
    #endregion

    private float height;
    private float sizeDeltaY;
    private Vector2 localPos;

    public void ListScrollViewDown()
    {
        height = gameObject.GetComponent<RectTransform>().rect.height;
        sizeDeltaY = gameObject.GetComponent<RectTransform>().sizeDelta.y;
        localPos = gameObject.transform.localPosition;

        LeanTween.value(gameObject, localPos.y, -(height / 2), 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, value);
            });

        LeanTween.value(gameObject, sizeDeltaY, sizeDeltaY - height, 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
           {
               gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, value);
           });
    }

    public void ListScrollViewUp()
    {
        LeanTween.value(gameObject, -(height / 2), localPos.y, 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, value);
            });

        LeanTween.value(gameObject, sizeDeltaY - height, sizeDeltaY, 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
            {
                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, value);
            });
    }

    [Header("ScrollViewUpDown")]
    public float UpPos;
    public float DownPos;

    #region ScrollViewUpDown

    public void TestListScrollViewDown()
    {

        LeanTween.value(SVListPanel.gameObject, SVListPanel.anchoredPosition.y, -DownPos, 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
            {
                SVListPanel.anchoredPosition = new Vector2(SVListPanel.anchoredPosition.x, value);
            });

        MoveUpBtn.SetActive(true);
        MoveDonwBtn.SetActive(false);
        ////-150
        //LeanTween.value(gameObject, gameObject.GetComponent<RectTransform>().anchoredPosition.y, -1120, 0.25f)
        //    .setEase(LeanTweenType.easeInSine)
        //    .setOnUpdate((value) =>
        //    {
        //        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, value);
        //    });

        ////-880
        //LeanTween.value(gameObject, gameObject.GetComponent<RectTransform>().sizeDelta.y, -2820, 0.25f)
        //    .setEase(LeanTweenType.easeInSine)
        //    .setOnUpdate((value) =>
        //    {
        //        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, value);
        //    });
    }

    public void TestListScrollViewUp()
    {
        //-1500
        LeanTween.value(SVListPanel.gameObject, SVListPanel.anchoredPosition.y, UpPos, 0.25f)
            .setEase(LeanTweenType.easeInSine)
            .setOnUpdate((value) =>
            {
                SVListPanel.anchoredPosition = new Vector2(SVListPanel.anchoredPosition.x, value);
            });

        MoveUpBtn.SetActive(false);
        MoveDonwBtn.SetActive(true);
        //-1120
        //LeanTween.value(gameObject, gameObject.GetComponent<RectTransform>().anchoredPosition.y, -150, 0.25f)
        //    .setEase(LeanTweenType.easeInSine)
        //    .setOnUpdate((value) =>
        //    {
        //        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponent<RectTransform>().anchoredPosition.x, value);
        //    });
        ////-2820
        //LeanTween.value(gameObject, gameObject.GetComponent<RectTransform>().sizeDelta.y, -880, 0.25f)
        //    .setEase(LeanTweenType.easeInSine)
        //    .setOnUpdate((value) =>
        //    {
        //        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, value);
        //    });
    }

    #endregion
}