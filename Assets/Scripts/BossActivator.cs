using UnityEngine;
using System.Collections;

public class BossActivator : MonoBehaviour
{
    public GameObject Boss;             // Ton boss désactivé au début
    public Transform BossSpawn;         // Point de spawn du boss
    public GameObject PlateFormeMobile; // Plateforme à déverrouiller
    public float verticalOffset = 0.1f; // Décalage pour ne pas faire tomber le boss dans la plateforme

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        if (Boss != null && BossSpawn != null)
        {
            ActivateBoss();
        }

        if (PlateFormeMobile != null)
        {
            ActivatePlateforme();
        }

        // Désactiver juste le collider du trigger
        Collider2D triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }
}

    private void ActivateBoss()
    {
        Boss.SetActive(true);
        Boss.transform.position = BossSpawn.position + Vector3.up * verticalOffset;

        Rigidbody2D rb = Boss.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.Sleep(); // Réinitialise le corps physique
        }

        StartCoroutine(ReenableColliderNextFrame());
    }

    private void ActivatePlateforme()
    {
        Rigidbody2D rb = PlateFormeMobile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.None; // Libère tous les axes
        }
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
}
