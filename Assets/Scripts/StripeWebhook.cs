using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class SlackAttachment
{
    public string title;
    public string text;
    public string color;

    public SlackAttachment(string title, string text, Color color)
    {
        this.title = title;
        this.text = text;
        this.color = this.GetHex(color);
    }

    string GetHex(Color color)
    {
        int r = (int)(color.r * 255f);
        int g = (int)(color.g * 255f);
        int b = (int)(color.b * 255f);
        return string.Format("#{0}{1}{2}", r.ToString("X2"), g.ToString("X2"), b.ToString("X2"));
    }
}

[System.Serializable]
public class SlackMessage
{
    public string text;
    public SlackAttachment[] attachments;

    public SlackMessage(string text)
    {
        this.text = text;
        this.attachments = new SlackAttachment[0] { };
    }

    public SlackMessage(string text, string attachmentTitle, string attachmentText, Color attachmentColor)
    {
        this.text = text;
        this.attachments = new SlackAttachment[1] { new SlackAttachment(attachmentTitle, attachmentText, attachmentColor) };
    }
}

public static class StripeWebhook
{

    const string hooksUrl = "https://hooks.slack.com/services/{your webhook information}";

    public static IEnumerator IPost(SlackMessage msg)
    {
        yield return 0;

        UnityWebRequest request = new UnityWebRequest(hooksUrl, "POST");
        request.SetRequestHeader("Content-Type", "application/json");

        var json = JsonUtility.ToJson(msg);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.LogWarning(request.error);

    }

    public static IEnumerator IPost(string text, string attachmentTitle, string attachmentText, Color attachmentColor)
    {
        var msg = new SlackMessage(text, attachmentTitle, attachmentText, attachmentColor);
        return IPost(msg);
    }

}
