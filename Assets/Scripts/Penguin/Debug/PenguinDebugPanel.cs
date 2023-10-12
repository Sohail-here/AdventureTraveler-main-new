using RestSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenguinDebugPanel : MonoSingleton<PenguinDebugPanel>
{
    public RectTransform RootPanel;
    public Text FpsText;
    private int count;
    private float deltaTime;

    public Dropdown UIDize;
    public Toggle UseTestRecord;
    public Button Btn_AddImage;
    public Button Btn_AddVideo;
    public Button Btn_DelAllJourneys;

    [Header("Face")]
    public Text Size_Text;
    public Scrollbar SizeBar;

    public Text TilingX_Text;
    public Text TilingY_Text;
    public Text OffsetX_Text;
    public Text OffsetY_Text;
    public Scrollbar TilingX;
    public Scrollbar TilingY;
    public Scrollbar OffsetX;
    public Scrollbar OffsetY;
    public Button FaceApplyBtn;
    public Transform FaceSizeTrans;
    public Material FaceMat;
    Vector2 faceScale = Vector2.one;
    Vector2 faceOffset = Vector2.zero;

    public Text OffsetText;
    public Scrollbar OffsetSc;

    float faceSize = 0.2f;

    bool isFaceSet = true;

    private void Start()
    {
        UIDize.onValueChanged.AddListener(i =>
        {
            float h = float.Parse(UIDize.options[i].text);
            int setH = 800 - (int)(h * 0.01f * 800);
            RootPanel.offsetMin = new Vector2(0, setH);
        });

        Btn_DelAllJourneys.onClick.AddListener(DelAllJourneys);

        FaceApplyBtn.onClick.AddListener(() =>
        {
            if (FaceMat != null)
            {
                FaceMat.mainTextureScale = faceScale;
                FaceMat.mainTextureOffset = faceOffset;
            }

            if (FaceSizeTrans != null)
                FaceSizeTrans.localScale = Vector3.one * faceSize;
        });


        TilingX.onValueChanged.AddListener((v) => { var f = (v - 0.5f) * 5; f = (float)System.Math.Round(f, 2); faceScale = new Vector2(f, faceScale.y); TilingX_Text.text = "TilingX:" + f; });
        TilingY.onValueChanged.AddListener((v) => { var f = (v - 0.5f) * 5; f = (float)System.Math.Round(f, 2); faceScale = new Vector2(faceScale.x, f); TilingY_Text.text = "TilingY:" + f; });
        OffsetX.onValueChanged.AddListener((v) => { var f = (v - 0.5f) * 5; f = (float)System.Math.Round(f, 2); faceOffset = new Vector2(f, faceOffset.y); OffsetX_Text.text = "OffsetX:" + f; });
        OffsetY.onValueChanged.AddListener((v) => { var f = (v - 0.5f) * 5; f = (float)System.Math.Round(f, 2); faceOffset = new Vector2(faceOffset.x, f); OffsetY_Text.text = "OffsetY:" + f; });
        OffsetSc.onValueChanged.AddListener((v) =>
        {
            v = (float)System.Math.Round(v * 550, 2);
            if (AR_Controller.Instance != null)
            {
                AR_Controller.Instance.SetOffsetBase(v);
            }
            OffsetText.text = "OffsetSc:" + v;
        });
        SizeBar.onValueChanged.AddListener((v) => { faceSize = v; Size_Text.text = "Size : " + v; });
    }
    static int mpIndex = 1, imgIndex = 1;
    public void Register(RecordManager record)
    {
        Btn_AddImage.onClick.AddListener(() =>
        {
            //record.UploadFile("Iamge_type1", $"Test00{imgIndex}.png");
            imgIndex++;
        });

        Btn_AddVideo.onClick.AddListener(() =>
        {
            //record.UploadFile("Iamge_type2", $"Test00{mpIndex}.mp4");
            mpIndex++;
        });
    }

    public void RegisterFaceSize(Transform trans)
    {
        FaceSizeTrans = trans;
        SizeBar.value = trans.localScale.x;
        Size_Text.text = "Size : " + SizeBar.value;
    }

    public void RegisterFaceUV(Material mat)
    {
        FaceMat = mat;
        faceScale = mat.mainTextureScale;
        faceOffset = mat.mainTextureOffset;
        TilingX.value = mat.mainTextureScale.x;
        TilingY.value = mat.mainTextureScale.y;
        OffsetX.value = mat.mainTextureOffset.x;
        OffsetY.value = mat.mainTextureOffset.y;
    }

    void Update()
    {
        UpdateFPS();
    }

    void UpdateFPS()
    {
        count++;
        deltaTime += Time.deltaTime;

        if (count % 60 == 0)
        {
            count = 1;
            var fps = 60f / deltaTime;
            deltaTime = 0;
            FpsText.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }

    async void DelAllJourneys()
    {
        var client = new RestClient("https://api.i911adventure.com/");
        var request = new RestRequest($"/delete_all_journey/", Method.DELETE);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "JWT " + UnDestroyData.token);

        var response =  await client.ExecuteAsync(request);
        ULog.Penguin.Log($"{response.StatusCode} : {response.Content}");
    }


}
