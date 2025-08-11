using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BossPush : MonoBehaviour
{
    [Header("Références (auto si vide)")]
    [SerializeField] private Transform player;    // laissé vide si scènes séparées
    [SerializeField] private Rigidbody2D bossRb;  // auto via GetComponent

    [Header("Recherche du Player")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool retryFindPlayer = true;
    [SerializeField] private float retryEverySeconds = 1f;

    [Header("Poussée")]
    [SerializeField] private float pushDistanceX = 1.5f; // distance horizontale
    [SerializeField] private float pushImpulse = 8f;
    [SerializeField] private float pushCooldown = 0.2f;

    [Header("Stabilité")]
    [Tooltip("Coupe toute vitesse verticale ascendante du joueur après la poussée.")]
    [SerializeField] private bool clampUpwardVelocityAfterPush = true;

    private float _lastPushTime = -999f;
    private Coroutine _retryCo;

    private void Awake()
    {
        if (!bossRb) bossRb = GetComponent<Rigidbody2D>();
        bossRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        bossRb.freezeRotation = true;
    }

    private void OnEnable()
    {
        // Tente de trouver immédiatement si déjà chargé
        TryFindPlayerOnce();

        // Si le player est chargé plus tard (autre scène), on retentera à chaque chargement de scène
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Optionnel : tentative périodique
        if (retryFindPlayer && _retryCo == null)
            _retryCo = StartCoroutine(RetryFindPlayerLoop());
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (_retryCo != null)
        {
            StopCoroutine(_retryCo);
            _retryCo = null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!player) TryFindPlayerOnce();
    }

    private IEnumerator RetryFindPlayerLoop()
    {
        var wait = new WaitForSeconds(retryEverySeconds);
        while (!player)
        {
            TryFindPlayerOnce();
            yield return wait;
        }
        _retryCo = null; // stop once found
    }

    private void TryFindPlayerOnce()
    {
        if (player) return;
        GameObject p = GameObject.FindGameObjectWithTag(playerTag);
        if (p) player = p.transform;
    }

    private void Update()
    {
        if (!player) return;

        float dxWorld = player.position.x - transform.position.x;
        float distX = Mathf.Abs(dxWorld);

        if (distX <= pushDistanceX && Time.time - _lastPushTime >= pushCooldown)
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

        if (dx == 0f) dx = 1f;
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
        Gizmos.DrawLine(transform.position + Vector3.left * pushDistanceX,
                        transform.position + Vector3.right * pushDistanceX);
    }
#endif
}
