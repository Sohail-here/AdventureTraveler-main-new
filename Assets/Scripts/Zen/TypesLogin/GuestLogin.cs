using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuestLogin : MonoBehaviour
{
    public Button GuestBtn;
    private void Awake()
    {
        GuestBtn.onClick.AddListener(OnGuestLogin);
    }

    public void OnGuestLogin()
    {
        UnDestroyData.IsGusetLogin = true;
        SceneManager.LoadScene("Main");
    }
}
