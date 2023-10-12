using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

public class WaitingPurchasePanel : MonoBehaviour
{

    public UnityEvent OnFinishedTransaction;
    public UnityEvent OnFailure;

    void OnEnable()
    {
        WaitForTransaction();
    }

    async void WaitForTransaction()
    {
        await Task.Delay(5000);
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        var d = await TransactionManager.Instance.CheckTransaction(SingletonData.Instance.checkingTransaction);
        Debug.Log(d["status"] + " " + SingletonData.Instance.checkingTransaction);
        Debug.Log(d["msg"]);
        if (d == null)
        {
            OnFailure.Invoke();
            return;
        } else if (d["status"] == "true")
        {
            OnFinishedTransaction.Invoke();
            return;
        }
        
        WaitForTransaction();
    }



}
