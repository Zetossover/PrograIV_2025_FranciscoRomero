using UnityEngine;
using System.Collections.Generic;

public class GasolineManager : MonoBehaviour
{
    [Header("Pool Settings")]
    public GameObject gasolinePrefab;
    public int poolSize = 6;

    private List<GameObject> gasolinePool = new List<GameObject>();

    [Header("Spawn Settings")]
    public Transform centerPoint;
    public float spawnRadius = 10f;
    public float minSpawnRadius = 3f;

    private int currentActiveGasoline = 0;

    void Start()
    {
        CreatePool();
        InitialSpawn();
    }

    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(gasolinePrefab);
            obj.SetActive(false);
            gasolinePool.Add(obj);
        }
    }

    void InitialSpawn()
    {
        for (int i = 0; i < poolSize; i++)
        {
            SpawnGasoline();
        }
    }

    GameObject GetGasolineFromPool()
    {
        foreach (GameObject gas in gasolinePool)
        {
            if (!gas.activeSelf)
            {
                return gas;
            }
        }
        return null;
    }

    public void SpawnGasoline()
    {
        GameObject gas = GetGasolineFromPool();
        if (gas == null) return;

        Vector3 spawnPos = GetValidSpawnPosition();
        gas.transform.position = spawnPos;
        gas.SetActive(true);

        currentActiveGasoline++;
    }

    public void OnGasolineCollected(GameObject gas)
    {
        gas.SetActive(false);
        currentActiveGasoline--;

        SpawnGasoline(); 
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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centerPoint.position, spawnRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centerPoint.position, minSpawnRadius);
    }
}
