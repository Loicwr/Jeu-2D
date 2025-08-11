using UnityEngine;

public class Glissade : MonoBehaviour
{
    public float glisseForceMax = 50f;
    public float glisseForceMin = 30f;
    public float angleSeuil = 5f;

    private Rigidbody2D rb;
    private Vector2 gravityDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityDirection = Physics2D.gravity.normalized;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Default"));
        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("PlateFormeMobile"))
        {
            Vector2 normal = hit.normal;
            Vector2 tangent = new Vector2(-normal.y, normal.x).normalized;

            float angle = Vector2.Angle(normal, Vector2.up);

            float signe = -Mathf.Sign(Vector2.Dot(tangent, -gravityDirection));
            Vector2 forceGlisseDirection = tangent * signe;

            float horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(horizontalInput) < 0.1f && angle > angleSeuil)
            {
                float forceGlisse = Mathf.Lerp(glisseForceMin, glisseForceMax, angle / 90f);
                rb.AddForce(forceGlisseDirection * forceGlisse);
            }
        }
    }
}
