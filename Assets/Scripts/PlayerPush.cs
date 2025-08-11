using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [Header("Cibles à pousser (tag du boss, par ex. 'Boss')")]
    [SerializeField] private string bossTag = "Boss";
    [SerializeField] private float pushImpulse = 8f;
    [SerializeField] private float pushCooldown = 0.2f;

    [Header("Stabilité")]
    [Tooltip("Si activé, coupe toute vitesse verticale ascendante de la cible après la poussée.")]
    [SerializeField] private bool clampUpwardVelocityAfterPush = true;

    private float _lastPushTime = -999f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryPush(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Optionnel, utile si plusieurs petits contacts se produisent
        TryPush(collision);
    }

    private void TryPush(Collision2D collision)
    {
        if (!collision.collider || !collision.collider.CompareTag(bossTag)) return;
        if (Time.time - _lastPushTime < pushCooldown) return;

        Rigidbody2D bossRb = collision.rigidbody;
        if (!bossRb) return;

        // direction strictement horizontale (aucune composante Y)
        float dx = Mathf.Sign(collision.transform.position.x - transform.position.x);
        if (dx == 0f) dx = 1f; // fallback

        Vector2 dir = new Vector2(dx, 0f);
        bossRb.AddForce(dir * pushImpulse, ForceMode2D.Impulse);

        if (clampUpwardVelocityAfterPush)
        {
            Vector2 v = bossRb.linearVelocity;
            if (v.y > 0f) v.y = 0f; // empêche tout "lift-off"
            bossRb.linearVelocity = v;
        }

        _lastPushTime = Time.time;
    }
}
