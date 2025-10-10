using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayfabLogin playFabLogin;
    [SerializeField] private GameObject blockPanel;
    [SerializeField] private GameObject[] panels;
    [SerializeField] private TextMeshProUGUI textFeedBack;

    [Header("Login Inputs")]
    [SerializeField] private TMP_InputField loginEmailInput;
    [SerializeField] private TMP_InputField loginPasswordInput;

    [Header("Register Inputs")]
    [SerializeField] private TMP_InputField registerEmailInput;
    [SerializeField] private TMP_InputField registerPasswordInput;
    [SerializeField] private TMP_InputField registerConfirmPasswordInput;
    [SerializeField] private TextMeshProUGUI registerWarningText;

    [Header("Recover Inputs")]
    [SerializeField] private TMP_InputField recoverEmailInput;

    [Header("LeaderBoard")]
    public int score;
    public int lifePoints;
    public List<LeaderboardData> leaderBoard;

    private void Start()
    {
        SetPanel(LoginPanelType.Menu);
        SetBlockPanel("", false);
        if (registerWarningText != null)
        {
            registerWarningText.text = "";
        }
            
    }
    void LoadLeaderBoard()
    {
        playFabLogin.GetDataFromMaxScore(OnEndLoadLeaderBoard);
    }

    void OnEndLoadLeaderBoard(List<LeaderboardData> data)
    {
        leaderBoard = data;
    }


    public void OnClickLogin()
    {
        string mail = loginEmailInput.text;
        string pass = loginPasswordInput.text;

        if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass))
        {
            SetBlockPanel("Por favor ingresa correo y contraseña.", true);
            return;
        }

        SetBlockPanel("Cargando datos...", true);
        playFabLogin.LoginUser(mail, pass, OnFinishAction);
    }

    public void OnClickRegister()
    {
        string mail = registerEmailInput.text;
        string pass = registerPasswordInput.text;
        string confirm = registerConfirmPasswordInput.text;

        if (registerWarningText != null)
            registerWarningText.text = "";

        if (string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirm))
        {
            SetBlockPanel("Por favor completa todos los campos.", true);
            return;
        }

        if (pass != confirm)
        {
            if (registerWarningText != null)
                registerWarningText.text = "Las contraseñas no coinciden.";

            return;
        }

        SetBlockPanel("Creando cuenta...", true);
        playFabLogin.RegisterUser(mail, pass, OnFinishAction);
    }

    public void OnClickRecoverPassword()
    {
        string mail = recoverEmailInput.text;

        if (string.IsNullOrEmpty(mail))
        {
            SetBlockPanel("Ingresa tu correo para recuperar la cuenta.", true);
            return;
        }

        SetBlockPanel("Enviando correo de recuperación...", true);
        playFabLogin.RecoverAccount(mail, OnFinishAction);
    }

    public void OnErrorPanelButton()
    {
        SetBlockPanel("", false);
    }

    private void OnFinishAction(string message, bool result)
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

    private void SetBlockPanel(string message, bool enable)
    {
        textFeedBack.text = message;
        blockPanel.SetActive(enable);
    }

    void SetPanel(LoginPanelType panelType)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == (int)panelType)
            {
                panels[i].SetActive(true);
            }
            else panels[i].SetActive(false);
        }
    }
    public void LoginButton()
    {
        SetPanel(LoginPanelType.Login);
    }
    public void RegisterButton()
    {
        SetPanel(LoginPanelType.Register);
    }
    public void RecoveryButton()
    {
        SetPanel(LoginPanelType.Recovery);
    }
}
public enum LoginPanelType
{
    Menu,
    Login,
    Register,
    Recovery
}

[System.Serializable]
public class PJData
{
    public int lifePoints;
    public int score;
}

[System.Serializable]
public class LeaderboardData
{
    public string displayName;
    public int score;
    public int boardPos;
}
