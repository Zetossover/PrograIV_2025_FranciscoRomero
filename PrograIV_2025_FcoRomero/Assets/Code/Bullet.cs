using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Referencias")]
    public SpriteRenderer spriteRenderer;

    [Header("Stats")]
    public int damage = 1;

    [Header("Layers")]
    public LayerMask enemyLayer;
    public LayerMask playerLayer;

    // -----------------------------
    //      SISTEMA DE SPRITE
    // -----------------------------

    public void SetSprite(Sprite newSprite)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = newSprite;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    // -----------------------------
    //   DETECCIÓN DE COLISIONES
    // -----------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {
        int otherLayer = other.gameObject.layer;

        // 📌 1. Golpea al Player → reducir tiempo
        if (((1 << otherLayer) & playerLayer) != 0)
        {
            Debug.Log("🔴 Bala enemiga golpeó al Player");

            if (TimeManager.Instance != null)
            {
                TimeManager.Instance.ReduceTime(TimeManager.Instance.damageTimePenalty);
            }

            Destroy(gameObject);
            return;
        }

        // 📌 2. Golpea a un Enemigo → sumar puntaje + hacer daño + destruir enemigo
        if (((1 << otherLayer) & enemyLayer) != 0)
        {
            Debug.Log("🟢 Bala golpeó un enemigo");

            EnemyMovement enemy = other.GetComponent<EnemyMovement>();

            if (enemy != null)
            {
                // Si tienes sistema de vida luego aquí puedes hacer:
                // enemy.ApplyDamage(damage);

                // Por ahora:
                ScoreManager.Instance?.AddScore(enemy.scoreValue);

                enemy.Die();
            }

            Destroy(gameObject);
            return;
        }
    }
}
