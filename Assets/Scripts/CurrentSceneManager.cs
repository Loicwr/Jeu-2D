using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;

    public static CurrentSceneManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            // si il y a 2 scripts de vie du joueur, c'est pour qu'on soit prévenu 
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager  dans la scène");
            return;
        }
        instance = this;
    }
}
