using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnenableBoatersPage : MonoBehaviour
{
    private void OnEnable()
    {
        Account.Instance.MyBoatLoad();
    }
}
