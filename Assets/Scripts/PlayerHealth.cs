using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealt = 100;
   public int currentHealth;

    public HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealt;
        healthBar.SetMaxHealth(maxHealt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
