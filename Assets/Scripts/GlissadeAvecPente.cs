using UnityEngine;

public class GlissadeAvecPente : MonoBehaviour
{
    public float forceMaxGlisse = 20f;     // Force max quand immobile
    public float forceMinGlisse = 5f;      // Force min quand marche
    public float angleSeuil = 5f;          // Angle minimal pour commencer à glisser (en degrés)

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f);

        Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("PlateFormeMobile"))
        {
            Vector2 normal = hit.normal;
            Vector2 tangent = new Vector2(-normal.y, normal.x).normalized;

            // Calcul de l'angle entre la normale et la verticale (Vector2.up)
            float angle = Vector2.Angle(normal, Vector2.up);

            if (angle > angleSeuil)
            {
                // Calcul de la force de glissade selon l'angle (linéaire)
                float forceSelonAngle = Mathf.InverseLerp(angleSeuil, 90f, angle);

                // Détecte si le joueur bouge horizontalement
                bool joueurBouge = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f;

                // Force finale (plus faible si le joueur bouge)
                float forceFinale = joueurBouge ? forceMinGlisse : forceMaxGlisse;

                // Applique la force en fonction de l'angle et déplacement du joueur
                rb.AddForce(tangent * forceFinale * forceSelonAngle);
            }
        }
    }
}
