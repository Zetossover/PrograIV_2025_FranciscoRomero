using UnityEngine;

public class LeaderboardContent : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text_Number;
    [SerializeField] TMPro.TextMeshProUGUI text_DisplayName;
    [SerializeField] TMPro.TextMeshProUGUI text_Points;

    public void SetLeaderboardContent(LeaderboardData leaderboardData)
    {
        text_Number.text = (leaderboardData.boardPos + 1).ToString();
        text_DisplayName.text = leaderboardData.displayName;
        text_Points.text = leaderboardData.score.ToString();
    }
}
