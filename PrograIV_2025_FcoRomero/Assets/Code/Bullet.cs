using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int damage;

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
}
