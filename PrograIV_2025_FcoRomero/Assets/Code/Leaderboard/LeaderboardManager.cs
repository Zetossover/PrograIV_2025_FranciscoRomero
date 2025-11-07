using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] LeaderboardContent[] leaderboardContents;
    public int score;

    private void Start()
    {
       SaveDataToLeaderBoard();
    }
    void SaveDataToLeaderBoard()
    {
        PlayfabLogin playfabLogin = new PlayfabLogin();
        playfabLogin.AddDataToMaxScore(score, OnEndSaveScore);
    }

    private void OnEndSaveScore(string msg, bool result)
    {
        LoadLeaderboard();
    }

    void LoadLeaderboard()
    {
        PlayfabLogin playfabLogin = new PlayfabLogin();
        playfabLogin.GetDataFromMaxScore(SetContent);
    }
    void SetContent(List<LeaderboardData> leaderBoardDataList)
    {
        for (int i = 0; i < leaderboardContents.Length; i++)
        {
            if (i < leaderBoardDataList.Count)
            {
                leaderboardContents[i].gameObject.SetActive(true);
                leaderboardContents[i].SetLeaderboardContent(leaderBoardDataList[i]);
            }
        }
    }
}
