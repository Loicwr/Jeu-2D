using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushForce = 10f;
    public float maxBossSpeed = 5f;
    private Rigidbody2D rbPlayer;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boss"))
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                Rigidbody2D rbBoss = collision.collider.GetComponent<Rigidbody2D>();
                if (rbBoss != null && rbBoss.linearVelocity.magnitude < maxBossSpeed)
                {
                    Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                    rbBoss.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
