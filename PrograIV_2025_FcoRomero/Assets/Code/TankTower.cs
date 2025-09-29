using UnityEngine;

public class TankTower : MonoBehaviour
{
    public float radioApuntar = 10f;
    public float radioAtaque = 9;
    public LayerMask detectionLayer;

    Transform target;
    Vector2 direccion;

    void Update()
    {
        Detect();

        if (target != null)
        {
            ApuntarEnemy();
        }
    }

    void ApuntarEnemy()
    {
        direccion = target.position - transform.position;
        if (radioApuntar >= direccion.magnitude)
        {
            transform.up = direccion;
        }
    }

    void Detect()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radioApuntar, detectionLayer);
        foreach (var hit in hits)
        {
            target = hit.transform;
            break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioApuntar);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }
}
