using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movimiento")]
    //public Transform[] wayPoints;
    //private int currentWaypoint;
    public float moveSpeed = 3f;
    [Range(0, 15)]
    public float stopDistance = 1.5f;

    [Header("Rotación")]
    public float rotationSpeed = 180f; 
    public float rotationOffset = 0f;  

    [Header("Detección")]
    [Range(0, 15)]
    public float detectionRadius = 6f;
    public LayerMask playerLayer;

    [Header("Referencias")]
    public Rigidbody2D rb;
    public Transform visual;

    [Header("Puntaje")]
    public int scoreValue = 10;

    private Transform player;

    void Update()
    {
        
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

        //if (transform.position != wayPoints[currentWaypoint].position)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypoint].position, moveSpeed * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    currentWaypoint++;
        //}
    }
    void MoveAndRotateTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        rb.MoveRotation(newAngle);

        if (visual != null)
        {
            visual.rotation = Quaternion.Euler(0, 0, newAngle);
        }
    }

    void StopCompletely()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        if (other.gameObject.layer == bulletLayer)
        {
            Die();
        }
    }

    public void Die()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(scoreValue);
        else
            Debug.LogWarning("No se pudo sumar puntaje.");

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}