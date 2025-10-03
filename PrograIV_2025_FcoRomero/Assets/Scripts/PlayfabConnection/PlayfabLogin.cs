using System;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayfabLogin : MonoBehaviour
{
    private Action<string, bool> OnFinishActionEvent;
    
    public void AnonimatoUser(Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "158731";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "FcoRomero",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    public void LoginUser(string mail, string pass, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new LoginWithEmailAddressRequest
        {
            Email = mail,
            Password = pass,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginResult, OnError);
    }

    private void OnLoginResult(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
    }

    public void RegisterUser(string mail, string pass, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;
        var request = new RegisterPlayFabUserRequest
        {
            Email = mail,
            Password = pass,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterUserResult, OnError);
    }

    private void OnError(PlayFabError error)
    {
        OnFinishActionEvent?.Invoke("Success", true);
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterUserResult(RegisterPlayFabUserResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        OnFinishActionEvent?.Invoke("Success", true);
        Debug.Log("Congratulations, you made your first successful API call!");
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.ErrorMessage.ToString()); //Transforma el error para que lo entiendan los que no hablan chatgpt
        OnFinishActionEvent?.Invoke(error.GenerateErrorReport(), false);
    }
    
}