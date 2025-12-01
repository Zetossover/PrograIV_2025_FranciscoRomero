using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 3f;
    [Range(0, 15)] public float stopDistance = 1.5f;

    [Header("Rotación")]
    public float rotationSpeed = 180f;
    public float rotationOffset = 0f;

    [Header("Detección")]
    [Range(0, 15)] public float detectionRadius = 6f;
    public LayerMask playerLayer;

    [Header("Referencias")]
    public Rigidbody2D rb;
    public Transform visual;

    [Header("Stats del enemigo")]
    public int maxLife = 3;
    private int currentLife;
    public int scoreValue = 10;

    public string id;

    [HideInInspector] public EnemyManager manager;

    private Transform player;

    void OnEnable()
    {
        // Resetear vida cuando aparece
        currentLife = maxLife;
    }

    void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (target != null)
        {
            player = target.transform;
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stopDistance)
                MoveAndRotateTowardsPlayer();
            else
                StopCompletely();
        }
        else
        {
            StopCompletely();
        }
    }

    // ============================================================
    //                        MOVIMIENTO
    // ============================================================

    void MoveAndRotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        rb.MoveRotation(newAngle);

        if (visual != null)
            visual.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    void StopCompletely()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // ============================================================
    //                        DAÑO RECIBIDO
    // ============================================================

    public void TakeDamage(int dmg)
    {
        currentLife -= dmg;

        if (currentLife <= 0)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int bulletLayer = LayerMask.NameToLayer("Bullet");

        if (other.gameObject.layer == bulletLayer)
        {
            // Obtener daño de la bala
            Bullet bullet = other.GetComponent<Bullet>();
            int dmg = (bullet != null) ? bullet.damage : 1;

            TakeDamage(dmg);

            other.gameObject.SetActive(false); // devolver bala al pool si tienes
        }
    }

    // ============================================================
    //                        MUERTE + MANAGER
    // ============================================================

    public void Die()
    {
        // Sumar puntaje si existe ScoreManager
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(scoreValue);

        // Avisar al EnemyManager
        if (manager != null)
            manager.OnEnemyKilled();

        // Desactivar objeto (pooling)
        gameObject.SetActive(false);

        AnalyticsManager.Instance.EnemyKilled(id);
    }

    // Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}