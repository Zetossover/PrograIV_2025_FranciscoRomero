using UnityEngine;

public class GasolinaPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeManager.Instance.AddTime(
                TimeManager.Instance.gasTimeBonus
            );

            Destroy(gameObject);
        }
    }
}
