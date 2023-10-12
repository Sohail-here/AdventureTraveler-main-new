using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;
using TMPro;

public class BirthdayCell : FancyCell<BirthdayItemData, BirthdayContext>
{
    [SerializeField] Animator animator = default;
    [SerializeField] TMP_Text message = default;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }

    public override void UpdateContent(BirthdayItemData itemData)
    {
        message.text = itemData.Message;
    }

    float currentPosition = 0;

    public override void UpdatePosition(float position)
    {
        currentPosition = position;


        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, 0, position);
        }

        animator.speed = 0;
    }

    void OnEnable() => UpdatePosition(currentPosition);
}