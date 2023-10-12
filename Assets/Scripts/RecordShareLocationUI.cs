using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecordShareLocationUI : MonoBehaviour
{
    [SerializeField] private Button m_SendInviteBtn;
    [SerializeField] private TextMeshProUGUI m_CodeText;

    private void Start()
    {
        m_SendInviteBtn.onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(m_CodeText.text) == false)
            {
                new NativeShare().SetSubject("AdventureTraveler").SetTitle("Journey Share").SetText($"Share code:{ m_CodeText.text}").Share();
            }
        });
    }

    public void Open(string code)
    {
        gameObject.SetActive(true);
        m_CodeText.text = code;
    }
}
