using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class pnlPurchaseHistoryPanel : MonoBehaviour
{
    public GameObject receiptPrefab;
    public List<GameObject> instantiatedReceipts;
    public Transform contentParent;
    public JSONObject receiptData;
    public bool running = false;

    public GameObject LoadingBar;
    public GameObject NoPurchaseMsg;

    private async void OnEnable()
    {
        NoPurchaseMsg.SetActive(true);

        if (running)
        {
            Debug.Log("Receipt retrieval can only be done once.");
            return;
        }
        running = true;
        ClearReceipts();

        try
        {
            var res = await TransactionManager.Instance.GetReceipts();
            if (res.IsSuccessful && res.Content != null)
            {
                NoPurchaseMsg.SetActive(false);

                var res_json = new JSONObject(res.Content);
                if (res_json.HasField("msg"))
                {
                    var msg_json = res_json["msg"];
                    receiptData = msg_json;

                    Debug.Log("GetReceipts Data : " + receiptData);
                    foreach (var receipt in msg_json.list)
                    {
                        await Task.Delay(500);
                        var tempReceipt = Instantiate(receiptPrefab, contentParent);
                        var purchasePanel = tempReceipt.GetComponent<ReceiptContentPanel>();
                        purchasePanel.Populate(receipt);
                        instantiatedReceipts.Add(tempReceipt);
                    }
                }
            }

            LoadingBar.GetComponent<UIAnimations>().StopLoading();
        } catch
        {
            Debug.LogError("There was a problem retrieving the receipts.");
        }
        running = false;
    }

    public void ClearReceipts()
    {
        for(int i = 0; i < instantiatedReceipts.Count; i++)
        {
            var ins = instantiatedReceipts[i];
            Destroy(ins);
        }
        instantiatedReceipts.Clear();
        receiptData = null;
    }
}
