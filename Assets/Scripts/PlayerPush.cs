using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 12f;          // force que le joueur applique au boss
    public float maxBossSpeed = 5f;        // limite de vitesse du boss
    public float pushWindow = 0.2f;        // durée où l'AI du boss est suspendue

    private Rigidbody2D rbPlayer;

    void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Boss")) return;

        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(h) < 0.1f) return;

        Rigidbody2D rbBoss = collision.rigidbody ?? collision.collider.GetComponent<Rigidbody2D>();
        if (rbBoss == null) return;

        // Limite optionnelle
        if (rbBoss.linearVelocity.magnitude >= maxBossSpeed) return;

        // Poussée horizontale seulement
        Vector2 pushDir = new Vector2(Mathf.Sign(h), 0f);

        // Fenêtre de push : on passe par le script du boss
        BossPush bossPush = collision.collider.GetComponent<BossPush>();
        if (bossPush != null)
        {
            bossPush.ApplyExternalPush(pushDir * pushForce, pushWindow);
        }
        else
        {
            // Fallback si pas de script BossPush
            rbBoss.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }
}
