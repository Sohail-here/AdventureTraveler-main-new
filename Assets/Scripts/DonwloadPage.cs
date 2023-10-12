using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DonwloadPage : MonoBehaviour
{
    [SerializeField] Image PGImg;

    private void Start()
    {
        PGImg.transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
}
