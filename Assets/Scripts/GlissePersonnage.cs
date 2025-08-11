using UnityEngine;

public class GlissePersonnage : MonoBehaviour
{
    public float glisseForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // On vérifie qu'on touche le sol
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Sol"));

        if (hit.collider != null)
        {
            // On récupère la normale de la surface
            Vector2 surfaceNormal = hit.normal;
            Vector2 tangent = new Vector2(-surfaceNormal.y, surfaceNormal.x); // perpendiculaire

            // Appliquer la force tangentielle (glissade)
            rb.AddForce(tangent * glisseForce);
        }
    }
}

