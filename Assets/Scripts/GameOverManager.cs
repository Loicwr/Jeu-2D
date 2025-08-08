using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            // si il y a 2 scripts de vie du joueur, c'est pour qu'on soit prévenu 
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }
        instance = this;
    }

    public void OnPlayerDeath()
    {
        if (!gameOverUI) { Debug.LogError("Assigne gameOverUI"); return; }
        gameOverUI.SetActive(true);
    }
   
    
}
