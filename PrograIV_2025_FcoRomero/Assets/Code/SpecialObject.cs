using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    private bool used = false;

    [Header("Boss Settings")]
    public GameObject specialEnemyPrefab;
    public Transform bossSpawnPoint; // Posición fija donde aparece

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;

            EnemyManager manager = FindFirstObjectByType<EnemyManager>();

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
