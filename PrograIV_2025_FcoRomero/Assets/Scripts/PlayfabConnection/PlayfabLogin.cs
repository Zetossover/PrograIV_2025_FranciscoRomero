using System;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayfabLogin : MonoBehaviour
{
    private Action<string, bool> OnFinishActionEvent;

    public delegate void OnEndRequestDel(string msg, bool result);
    OnEndRequestDel OnEndRequestEvent;
    public delegate void OnLoadRequestDel(string data, bool result);
    OnLoadRequestDel OnEndLoadRequestEvent;
    public delegate void OnLoadLeaderBoard(List<LeaderboardData> leaderBoard);
    OnLoadLeaderBoard OnEndLoadLeaderBoardEvent;

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
    public void SaveData(string data, string dataKey)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {dataKey, data},
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnEndSaveData, OnError);
    }
    public void LoadData(string dataKey, Action<string> OnLoaded)
    {
        var request = new GetUserDataRequest();

        PlayFabClientAPI.GetUserData(request, result =>
        {
            if (result.Data != null && result.Data.ContainsKey(dataKey))
            {
                string data = result.Data[dataKey].Value;
                OnLoaded(data);
            }
            else
            {
                Debug.Log("Not Key Found");
                OnLoaded(default);
            }
        }, OnError);
    }
    public void RecoverAccount(string mail, Action<string, bool> onFinishAction)
    {
        OnFinishActionEvent = onFinishAction;

        var request = new SendAccountRecoveryEmailRequest
        {
            Email = mail,
            TitleId = PlayFabSettings.staticSettings.TitleId
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverSuccess, OnError);
    }
    public void AddDataToMaxScore(int score, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new System.Collections.Generic.List<PlayFab.ClientModels.StatisticUpdate>
            {
                new PlayFab.ClientModels.StatisticUpdate
                {
                    StatisticName = "MaxScore", // nombre de tu leaderboard
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnStatisticsResult, OnError);
    }
    public void GetDataFromMaxScore(OnLoadLeaderBoard onLoadLeaderBoard)
    {
        OnEndLoadLeaderBoardEvent = onLoadLeaderBoard;
        var request = new GetLeaderboardRequest
        {
            StatisticName = "MaxScore", // nombre de tu leaderboard
            StartPosition = 0,             // posición inicial (0 = primer lugar)
            MaxResultsCount = 10           // cantidad máxima de resultados
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardLoad, OnError);
    }
    private void OnLeaderBoardLoad(GetLeaderboardResult result)
    {
        List<LeaderboardData> dataList = new List<LeaderboardData>();
        foreach (var item in result.Leaderboard)
        {
            LeaderboardData newData = new LeaderboardData()
            {
                displayName = item.DisplayName,
                score = item.StatValue,
                boardPos = item.Position
            };
            dataList.Add(newData);
        }
        OnEndLoadLeaderBoardEvent?.Invoke(dataList);
    }
    public void SetDisplayName(string displayName, OnEndRequestDel onEndRequest)
    {
        OnEndRequestEvent = onEndRequest;
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnEndRequestDisplayName, OnError);
    }

    private void OnRecoverSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Correo de recuperación enviado.");
        OnFinishActionEvent?.Invoke("Correo de recuperación enviado.", true);
    }
    private void OnEndSaveData(UpdateUserDataResult result)
    {
        Debug.Log("DataIsSaved");
    }
    private void OnStatisticsResult(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
    }
    private void OnEndRequestDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Succes");
        OnEndRequestEvent?.Invoke("Succes", true);
        OnEndRequestEvent = null;
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