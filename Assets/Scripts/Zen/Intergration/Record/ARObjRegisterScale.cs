using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObjRegisterScale : MonoBehaviour
{
    private void OnEnable()
    {
        AR_Controller.Instance.RegisterSacleObj(this.transform);
    }
}
