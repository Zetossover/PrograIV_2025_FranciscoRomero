using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeManager.Instance.ReduceTime(
                TimeManager.Instance.damageTimePenalty
            );
            Debug.Log("Trigger con: " + other.name);
            Destroy(gameObject);
        }
    }

}
