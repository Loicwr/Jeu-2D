using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f; 
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public float invincibilityFlashdelay = 0.15f;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            // si il y a 2 scripts de vie du joueur, c'est pour qu'on soit prévenu 
            Debug.LogWarning("Il y a plus d'une instance vie du joueur dans la scène");
            return;
        }
        instance = this;
    }

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
            TakeDamage(60);
        }
    }

    public void HealPlayer(int amount)
    {
        if((currentHealth + amount) > maxHealth)
        {
            currentHealth = maxHealth; // ne pas dépasser la vie maximale
        } else
        {
            currentHealth += amount;
        }
            
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // verifier si le joueur est toujours vivant 
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die()
    {
        // bloquer les mouvements du personnage 
        PlayerMovement.instance.enabled = false;

        // jouer l'animation d'élimination 
        PlayerMovement.instance.animator.SetTrigger("Die");

        // empêcher les interactions physique avec les autres éléments de la scène  
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;

        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        
        PlayerMovement.instance.enabled = true;

      
        PlayerMovement.instance.animator.SetTrigger("Respawn");

       
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

    }


    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            // Flash effect
            graphics.color = new Color(1f, 1f, 1f, 0f); // semi-transparent
            yield return new WaitForSeconds(invincibilityFlashdelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashdelay);
        }
    }
    public IEnumerator HandleInvincibilityDelay() 
    {
     yield return new WaitForSeconds(invincibilityTimeAfterHit); 
        isInvincible = false;  
    }
}
