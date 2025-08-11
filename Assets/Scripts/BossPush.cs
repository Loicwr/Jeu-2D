using UnityEngine;

public class BossPush : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D bossRb;

    [Header("Poussée")]
    [SerializeField] private float pushDistance = 1.5f;
    [SerializeField] private float pushImpulse = 8f;
    [SerializeField] private float pushCooldown = 0.2f;

    [Header("Stabilité")]
    [Tooltip("Si activé, coupe toute vitesse verticale ascendante du joueur après la poussée.")]
    [SerializeField] private bool clampUpwardVelocityAfterPush = true;

    private float _lastPushTime = -999f;

    private void Start()
{
    if (!player)
    {
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj) player = pObj.transform;
    }

    if (!bossRb) bossRb = GetComponent<Rigidbody2D>();
}

    private void Reset()
    {
        bossRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!player) return;

        float dxWorld = player.position.x - transform.position.x;
        float distX = Mathf.Abs(dxWorld);

        if (distX <= pushDistance && Time.time - _lastPushTime >= pushCooldown)
        {
            TryPushPlayer(Mathf.Sign(dxWorld));
            _lastPushTime = Time.time;
        }
    }

    private void TryPushPlayer(float dx)
    {
        if (!player) return;
        var playerRb = player.GetComponent<Rigidbody2D>();
        if (!playerRb) return;

        if (dx == 0f) dx = 1f; // fallback
        Vector2 dir = new Vector2(dx, 0f); // 100% horizontal

        playerRb.AddForce(dir * pushImpulse, ForceMode2D.Impulse);

        if (clampUpwardVelocityAfterPush)
        {
            Vector2 v = playerRb.linearVelocity;
            if (v.y > 0f) v.y = 0f;
            playerRb.linearVelocity = v;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Visualise la zone de poussée sur l'axe X
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.left * pushDistance,
                        transform.position + Vector3.right * pushDistance);
    }
#endif
}
