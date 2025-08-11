using UnityEngine;

public class GlisseSurPlateformeMobile : MonoBehaviour
{
    public float glisseForce = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Raycast vers le bas pour détecter la plateforme sous le personnage
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        if (hit.collider != null && hit.collider.gameObject.name == "PlateformeMobile")
        {
            // Récupère la normale de la plateforme
            Vector2 normal = hit.normal;
            // Vecteur tangent à la surface = direction de la glissade
            Vector2 tangent = new Vector2(-normal.y, normal.x);

            // Appliquer la glissade uniquement si le joueur ne bouge pas
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f)
            {
                rb.AddForce(tangent * glisseForce);
            }
        }
    }
}

