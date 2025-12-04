using UnityEngine;

public class SpecialObjectManager : MonoBehaviour
{
    [Header("Objeto Especial")]
    public GameObject specialObjectPrefab;

    [Header("Spawn Settings")]
    public Transform centerPoint;
    public float spawnRadius = 12f;
    public float minSpawnRadius = 4f;

    private GameObject spawnedSpecialObject;
    private bool hasSpawned = false;

    void Start()
    {
        SpawnSpecialObjectOnce();
    }

    void SpawnSpecialObjectOnce()
    {
        if (hasSpawned) return;

        Vector3 spawnPos = GetValidSpawnPosition();
        spawnedSpecialObject = Instantiate(specialObjectPrefab, spawnPos, Quaternion.identity);

        hasSpawned = true;
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

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(centerPoint.position, spawnRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(centerPoint.position, minSpawnRadius);
    }
}

