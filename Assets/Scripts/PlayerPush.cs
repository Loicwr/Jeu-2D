using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerPush : MonoBehaviour
{
    [Header("Tag de la cible (ex: 'Boss')")]
    [SerializeField] private string bossTag = "Boss";

    [Header("Poussée")]
    [SerializeField] private float pushImpulse = 8f;
    [SerializeField] private float pushCooldown = 0.2f;

    [Header("Stabilité")]
    [Tooltip("Coupe toute vitesse verticale ascendante de la cible après la poussée.")]
    [SerializeField] private bool clampUpwardVelocityAfterPush = true;

    [Tooltip("Ignorer brièvement la collision après la poussée pour éviter les doubles-impulsions.")]
    [SerializeField] private bool tempIgnoreCollision = true;
    [SerializeField] private float ignoreDuration = 0.1f;

    private float _lastPushTime = -999f;
    private Collider2D _myCol;

    private void Awake()
    {
        _myCol = GetComponent<Collider2D>();
        // Optionnel : stabilise la physique
        var rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryPush(collision);
    private void OnCollisionStay2D (Collision2D collision) => TryPush(collision);

    private void TryPush(Collision2D collision)
    {
        if (!collision.collider || !collision.collider.CompareTag(bossTag)) return;
        if (Time.time - _lastPushTime < pushCooldown) return;

        var bossRb = collision.rigidbody;
        if (!bossRb) return;

        // direction strictement horizontale (aucune composante Y)
        float dx = Mathf.Sign(collision.transform.position.x - transform.position.x);
        if (dx == 0f) dx = 1f;
        Vector2 dir = new Vector2(dx, 0f);

        bossRb.AddForce(dir * pushImpulse, ForceMode2D.Impulse);

        if (clampUpwardVelocityAfterPush)
        {
            Vector2 v = bossRb.linearVelocity;
            if (v.y > 0f) v.y = 0f;
            bossRb.linearVelocity = v;
        }

        if (tempIgnoreCollision && _myCol)
            StartCoroutine(TempIgnoreCollision(_myCol, collision.collider, ignoreDuration));

        _lastPushTime = Time.time;
    }

    private IEnumerator TempIgnoreCollision(Collider2D a, Collider2D b, float duration)
    {
        if (a && b)
        {
            Physics2D.IgnoreCollision(a, b, true);
            yield return new WaitForSeconds(duration);
            Physics2D.IgnoreCollision(a, b, false);
        }
    }
}
