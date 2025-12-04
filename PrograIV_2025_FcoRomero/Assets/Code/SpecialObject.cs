using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    private bool used = false;
    private int specialCollected = 1;

    [Header("Boss Settings")]
    public GameObject specialEnemyPrefab;
    public Transform bossSpawnPoint; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;

            EnemyManager manager = FindFirstObjectByType<EnemyManager>();
            AnalyticsManager.Instance.SpecialEvent(specialCollected);

            if (manager != null)
            {
                manager.KillAllEnemies();
            }

            if (specialEnemyPrefab != null && bossSpawnPoint != null)
            {
                Instantiate(specialEnemyPrefab, bossSpawnPoint.position, Quaternion.identity);
            }

            gameObject.SetActive(false);
        }
    }
}
