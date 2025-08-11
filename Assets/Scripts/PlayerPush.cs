using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 12f;     // force appliquée au boss
    public float pushWindow = 0.3f;   // durée de pause IA du boss après un push

    Rigidbody2D rbPlayer;

    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Boss")) return;

        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(h) < 0.1f) return;

        // Direction horizontale seulement (pousser dans le sens de l'input)
        Vector2 pushDir = new Vector2(Mathf.Sign(h), 0f);

        // 1) Essaye sur l'objet du collider
        BossPush bossPush = collision.collider.GetComponent<BossPush>();
        // 2) Si null, remonte dans la hiérarchie (bossPush sur le parent)
        if (bossPush == null) bossPush = collision.collider.GetComponentInParent<BossPush>();
        // 3) En dernier recours, passe par le Rigidbody touché
        if (bossPush == null && collision.rigidbody != null)
            bossPush = collision.rigidbody.GetComponent<BossPush>();

        if (bossPush != null)
        {
            // Appelle la méthode qui APPLIQUE la force + met en pause l’IA
            bossPush.ApplyExternalPush(pushDir * pushForce, pushWindow);
        }
        else
        {
            // Fallback absolu (si aucun script BossPush n'est trouvé)
            Rigidbody2D rbBoss = collision.rigidbody ?? collision.collider.GetComponent<Rigidbody2D>();
            if (rbBoss != null)
                rbBoss.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }
}
