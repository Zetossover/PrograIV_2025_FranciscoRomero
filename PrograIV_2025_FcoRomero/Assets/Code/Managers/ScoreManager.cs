using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void SaveDataToLeaderBoard(PlayfabLogin.OnEndRequestDel onEndSave)
    {
        PlayfabLogin playfabLogin = new PlayfabLogin();
        playfabLogin.AddDataToMaxScore(score, onEndSave);
        PlayerPrefs.SetInt("CurrentPoints", score);
    }

}
