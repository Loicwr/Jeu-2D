using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    void Paused()
    {
        // Désactiver les mouvements du joueur
        PlayerMovement.instance.enabled = false; 
        // Afficher le menu de pause
        pauseMenuUI.SetActive(true);
        // Arreter le temps
        Time.timeScale = 0;
        //changer le statut du jeu
        gameIsPaused = true;
    }

    public void Resume()
    {
        // Réactiver les mouvements du joueur
        PlayerMovement.instance.enabled = true;
        // Cacher le menu de pause
        pauseMenuUI.SetActive(false);
        // Reprendre le temps
        Time.timeScale = 1;
        //changer le statut du jeu
        gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
