using UnityEngine;

public class BossPush : MonoBehaviour
{
    [Header("Déplacement AI")]
    public float moveSpeed = 3f;           // vitesse de poursuite du joueur

    [Header("Poussée sur le joueur")]
    public float pushForce = 25f;          // force appliquée au joueur
    public float pushDistance = 1.5f;      // distance max avant de pousser

    [Header("Fenêtre de poussée externe")]
    public float externalPushDuration = 0.2f; // durée où l'IA est suspendue après push externe
    public float maxSpeed = 8f;               // vitesse max (sécurité)

    private Rigidbody2D rb;
    private Transform player;
    private float externalPushTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player introuvable ! Assure-toi qu’il a le tag 'Player'");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Décompte du temps de "pause IA"
        if (externalPushTimer > 0f)
        {
            externalPushTimer -= Time.fixedDeltaTime;
        }

        // IA seulement si le boss n'est pas en train d'être poussé
        if (externalPushTimer <= 0f)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
        }

        // Clamp vitesse (sécurité)
        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb.linearVelocity.y, -maxSpeed, maxSpeed)
        );

        // Pousser le joueur si proche
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= pushDistance)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                float dirX = Mathf.Sign(player.position.x - transform.position.x);
                Vector2 pushDir = new Vector2(dirX, 0f); // uniquement horizontal
                playerRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    // Méthode appelée par le joueur pour pousser le boss
    public void ApplyExternalPush(Vector2 force, float duration)
    {
        rb.AddForce(force, ForceMode2D.Impulse);

        // Clamp direct après la poussée
        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rb.linearVelocity.y, -maxSpeed, maxSpeed)
        );

        // Active la pause IA
        externalPushTimer = Mathf.Max(externalPushTimer, duration > 0f ? duration : externalPushDuration);
    }
}
