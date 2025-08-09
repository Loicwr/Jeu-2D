using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public string levelToLoad;
    public GameObject settingsWindow;

    public void StartGame()
    {
        // Charge la première scène du jeu
        SceneManager.LoadScene(levelToLoad);
    }
    public void SettingsButton()
    {
       settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        // Ferme la fenêtre des paramètres
        settingsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        // Quitte l'application
        Application.Quit();
    }
}
