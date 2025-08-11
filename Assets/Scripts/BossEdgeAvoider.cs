using UnityEngine;

public class BossEdgeAvoider : MonoBehaviour
{
    [SerializeField] private Transform groundCheckFront;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance = 0.5f;

    private Rigidbody2D rb;
    private BossMovement bossMovement; // Remplace par le vrai nom de ton script d'IA

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bossMovement = GetComponent<BossMovement>(); // adapte avec le nom exact de ton script
    }

    private void Update()
    {
        bool groundAhead = Physics2D.Raycast(groundCheckFront.position, Vector2.down, checkDistance, groundLayer);

        if (!groundAhead)
        {
            // stoppe immédiatement
            rb.velocity = new Vector2(0f, rb.velocity.y);

            // désactive le mouvement IA
            if (bossMovement != null)
                bossMovement.enabled = false;

            // demi-tour
            Flip();
        }
        else
        {
            // réactive le mouvement IA si le sol est devant
            if (bossMovement != null && !bossMovement.enabled)
                bossMovement.enabled = true;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckFront != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheckFront.position, groundCheckFront.position + Vector3.down * checkDistance);
        }
    }
}
