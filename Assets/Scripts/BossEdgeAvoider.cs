using UnityEngine;

public class BossEdgeAvoider : MonoBehaviour
{
    [SerializeField] private Transform groundCheckFront;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkDistance = 0.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // On regarde le sol juste devant le boss
        RaycastHit2D hit = Physics2D.Raycast(groundCheckFront.position, Vector2.down, checkDistance, groundLayer);

        // Si pas de sol → on stoppe le déplacement IA (mais on ne bloque pas physiquement)
        if (!hit)
        {
            StopMovement();
            Flip();
        }
    }

    private void StopMovement()
    {
        // On stoppe uniquement la vitesse horizontale de l'IA
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
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
