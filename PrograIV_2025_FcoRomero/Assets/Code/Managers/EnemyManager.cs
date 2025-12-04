using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject[] enemyPrefabs;
    public int poolSizePerEnemy = 5;

    private List<GameObject> enemyPool = new List<GameObject>();

    [Header("Spawn Settings")]
    public Transform centerPoint;

    public float spawnRadius = 10f;     
    public float minSpawnRadius = 3f;    

    public int maxActiveEnemies = 5;

    private int currentActiveEnemies = 0;

    void Start()
    {
        CreatePool();
        InitialSpawn();
    }

    void CreatePool()
    {
        foreach (GameObject prefab in enemyPrefabs)
        {
            for (int i = 0; i < poolSizePerEnemy; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);

                EnemyMovement enemy = obj.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.manager = this;
                }

                enemyPool.Add(obj);
            }
        }
    }

    GameObject GetEnemyFromPool()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeSelf)
            {
                return enemy;
            }
        }
        return null;
    }

    void InitialSpawn()
    {
        for (int i = 0; i < maxActiveEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void OnEnemyKilled()
    {
        currentActiveEnemies--;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        GameObject enemy = GetEnemyFromPool();
        if (enemy == null) return;

        Vector3 spawnPos = GetValidSpawnPosition();

        enemy.transform.position = spawnPos;
        enemy.SetActive(true);

        currentActiveEnemies++;
    }
    public void KillAllEnemies()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (enemy.activeSelf)
            {
                enemy.SetActive(false);
            }
        }

        currentActiveEnemies = 0;
    }
    Vector3 GetValidSpawnPosition()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        float randomDist = Random.Range(minSpawnRadius, spawnRadius);

        Vector2 finalPos = randomDir * randomDist;

        return centerPoint.position + new Vector3(finalPos.x, finalPos.y, 0f);
    }

    private void OnDrawGizmos()
    {
        if (centerPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint.position, spawnRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerPoint.position, minSpawnRadius);
    }
}