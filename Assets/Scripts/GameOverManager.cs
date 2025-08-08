using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }
        gameOverUI.SetActive(true);
    }
   
    public void RetryButton()
    {
        // Recharge la scène 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replace le jour au spawn
        // Réactive les mouvements du joueur + qu'on lui rende sa vie 
        gameOverUI.SetActive(false);
    }
    public void MainMenuButton()
    {

    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
