using UnityEngine;
using System.Collections;

public class BossActivator : MonoBehaviour
{
    public GameObject Boss;             // Ton boss désactivé au début
    public Transform BossSpawn;         // Point de spawn existant
    public GameObject PlateFormeMobile; // Plateforme à déverrouiller
    public float verticalOffset = 0.1f; // Décalage pour éviter la chute

    [Header("Respawn Settings")]
    public int maxRespawns = 3;          // Nombre max de vies
    private int currentRespawns = 0;     // Compteur

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentRespawns < maxRespawns)
        {
            ActivateBoss();
            ActivatePlateforme();

            // Désactiver le collider du trigger pour éviter réactivation immédiate
            Collider2D triggerCollider = GetComponent<Collider2D>();
            if (triggerCollider != null)
                triggerCollider.enabled = false;
        }
    }

    private void ActivateBoss()
    {
        if (Boss == null || BossSpawn == null) return;

        Boss.SetActive(true);
        Boss.transform.position = BossSpawn.position + Vector3.up * verticalOffset;

        Rigidbody2D rb = Boss.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.Sleep();
        }

        StartCoroutine(ReenableColliderNextFrame());
    }

    private void ActivatePlateforme()
    {
        if (PlateFormeMobile == null) return;

        Rigidbody2D rb = PlateFormeMobile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.constraints = RigidbodyConstraints2D.None;
    }

    private IEnumerator ReenableColliderNextFrame()
    {
        Collider2D col = Boss.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            yield return null;
            col.enabled = true;
        }
    }

    // Appelé par la DeathZone quand le boss meurt
    public void OnBossDeath()
    {
        currentRespawns++;

        if (currentRespawns < maxRespawns)
        {
            StartCoroutine(RespawnBossAfterDelay(1f));
        }
        else
        {
            Debug.Log("Boss mort définitivement");
        }
    }

    private IEnumerator RespawnBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateBoss();
    }
}
