using UnityEngine;

public class Glissade : MonoBehaviour
{
    public float glisseForce = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Raycast vers le bas pour détecter la plateforme
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Default"));

        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red); // Pour visualiser

        if (hit.collider != null && hit.collider.CompareTag("PlateFormeMobile"))
        {
            Vector2 normal = hit.normal;
            Vector2 tangent = new Vector2(-normal.y, normal.x).normalized;

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f)
            {
                rb.AddForce(tangent * glisseForce);
            }
        }
    }
}
