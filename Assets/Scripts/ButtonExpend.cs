using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonExpend : MonoSingleton<ButtonExpend>
{
    public List<Button> Btns = new List<Button>();

    [ContextMenu("GetAllBtn!!")]
    private void FindAllBtn()
    {
        Btns = new List<Button>();
        Btns.AddRange(FindObjectsOfType<Button>(true));
        foreach (var btn in Btns)
        {
            btn.transition = Selectable.Transition.None;
        }
    }

    private void Start()
    {
        foreach (var btn in Btns)
        {
            try
            {
                if (btn == null)
                    continue;

                btn.transition = Selectable.Transition.None;
                btn.onClick.AddListener(() =>
                {
                    DOTween.Sequence().Append(btn.transform.DOScale(new Vector3(0.65f, 0.65f, 1), 0.1f))
                    .Append(btn.transform.DOScale(Vector3.one, 0.1f))
                    .OnComplete(() => { btn.transform.localScale = Vector3.one; });
                });
            }
            catch (System.Exception e)
            {
                Debug.Log(e);
            }
        }
    }

    public void AddBtn(Button btn)
    {
        btn.transition = Selectable.Transition.None;
        btn.onClick.AddListener(() =>
        {
            DOTween.Sequence().Append(btn.transform.DOScale(new Vector3(0.85f, 0.85f, 1), 0.1f))
            .Append(btn.transform.DOScale(Vector3.one, 0.1f))
            .OnComplete(() => { btn.transform.localScale = Vector3.one; });
        });
    }
}
