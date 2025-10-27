using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;

    [Header("Rotaci�n")]
    public float rotationSpeed = 180f; // grados por segundo
    public float rotationOffset = 0f;  // ajuste visual del sprite

    [Header("Detecci�n")]
    public float detectionRadius = 6f;
    public LayerMask playerLayer;

    [Header("Referencias")]
    public Rigidbody2D rb;
    public Transform visual; // el sprite o hijo del enemigo

    private Transform player;

    void Update()
    {
        // Buscar al jugador dentro del radio de detecci�n
        Collider2D target = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (target != null)
        {
            player = target.transform;
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                MoveAndRotateTowardsPlayer();
            }
            else
            {
                StopCompletely();
            }
        }
        else
        {
            StopCompletely();
        }
    }

    void MoveAndRotateTowardsPlayer()
    {
        // Direcci�n hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Movimiento lineal
        rb.linearVelocity = direction * moveSpeed;

        // Calcular �ngulo en Z con offset
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        // Rotaci�n suave
        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        // Aplicar rotaci�n al Rigidbody
        rb.MoveRotation(newAngle);

        // Mantener el visual alineado
        if (visual != null)
            visual.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    void StopCompletely()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    void OnDrawGizmosSelected()
    {
        // Radio de detecci�n
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Radio de parada
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}