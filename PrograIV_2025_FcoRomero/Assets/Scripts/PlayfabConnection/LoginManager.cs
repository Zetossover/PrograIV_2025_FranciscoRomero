using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("Clases")]
    [SerializeField] PlayfabLogin playfabLogin;

    [Header("Variables")]
    public string mail;
    public string password;

    [Header("Objetos")]
    [SerializeField] GameObject blockPanel;
    [SerializeField] TextMeshProUGUI textFeedBack;

    private void Start()
    {
        SetBlockPanel("Loading...", true);
        playfabLogin.LoginUser(mail, password, OnFinishAction);
    }
    void SetBlockPanel(string message, bool enable)
    {
        textFeedBack.text = message;
        blockPanel.SetActive(enable);
    }
    void OnFinishAction(string message, bool result)
    {
        if (result)
        {
            SetBlockPanel(message, false);
        }
        else
        {
            SetBlockPanel(message, true);
        }
    }

}
