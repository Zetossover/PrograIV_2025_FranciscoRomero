using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Configuración del tiempo")]
    public float maxTime = 60f;      
    public float currentTime;       

    [Header("UI")]
    public TextMeshProUGUI timeText;

    [Header("Ajustes de eventos")]
    public float damageTimePenalty = 10f;  
    public float gasTimeBonus = 30f;       

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTime = maxTime;
        UpdateUI();
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        UpdateUI();
    }

    public void ReduceTime(float amount)
    {
        if (isGameOver) return;

        currentTime -= amount;
        if (currentTime < 0)
            currentTime = 0;

        UpdateUI();

        if (currentTime == 0)
        {
            GameOver();
        }
    }

    public void AddTime(float amount)
    {
        if (isGameOver) return;

        currentTime += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (timeText != null)
        {
            timeText.text = Mathf.Ceil(currentTime).ToString("0");
        }
    }

    void GameOver()
    {
        isGameOver = true;

        ScoreManager.Instance.SaveDataToLeaderBoard((msg, success) =>
        {
            SceneManager.LoadScene("Leaderboard");
        });
    }
}
