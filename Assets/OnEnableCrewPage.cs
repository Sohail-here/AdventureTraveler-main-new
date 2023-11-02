using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableCrewPage : MonoBehaviour
{
    private void OnEnable()
    {
        Account.Instance.MyCrewLoad();
    }
}
