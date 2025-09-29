using UnityEngine;

public class Paused : MonoBehaviour
{
    public GameObject pauseButton;

    private bool isPaused = false;

    public void Pause()
    {
        isPaused = !isPaused;

        pauseButton.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
