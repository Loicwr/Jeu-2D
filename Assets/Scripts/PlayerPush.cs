using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 10f;
    private Rigidbody2D rbPlayer;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boss"))
        {
            Rigidbody2D rbBoss = collision.collider.GetComponent<Rigidbody2D>();
            if (rbBoss != null)
            {
                // Direction de poussée = vers l’extérieur depuis le joueur
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;

                // Appliquer une force au boss
                rbBoss.AddForce(pushDirection * pushForce);
            }
        }
    }
}
