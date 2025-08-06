using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;


    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }
        public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            // Flash effect
            graphics.color = new Color(1f, 1f, 1f, 0f); // semi-transparent
            yield return new WaitForSeconds(1f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }
}
