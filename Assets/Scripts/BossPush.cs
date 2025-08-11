using UnityEngine;

public class BossPush : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float pushForce = 10f;
    public float pushDistance = 1.5f;

    private Rigidbody2D rb;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Recherche du joueur via son tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player introuvable ! Assure-toi qu’il a le tag 'Player'");
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        // Déplacement vers le joueur
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // Poussée si proche
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= pushDistance)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 pushDir = (player.position - transform.position).normalized;
                playerRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
