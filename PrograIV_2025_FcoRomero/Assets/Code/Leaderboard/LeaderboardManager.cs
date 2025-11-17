using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] LeaderboardContent[] leaderboardContents;
    [SerializeField] CanvasAnimation canvasAnim;
    public int score;

    private void Start()
    {
        score = PlayerPrefs.GetInt("CurrentPoints");
        StartCoroutine(TimerLoad());
    }
    IEnumerator TimerLoad()
    {
        yield return canvasAnim.AnimPanelCoroutine(true);
        yield return canvasAnim.ShowPointsCoroutine(score);
        LoadLeaderboard();
    }
    void LoadLeaderboard()
    {
        PlayfabLogin playfabmanager = new PlayfabLogin();
        playfabmanager.GetDataFromMaxScore(SetContent);
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
            else leaderboardContents[i].gameObject.SetActive(false);
        }
    }
}
