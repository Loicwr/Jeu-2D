using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinCount;

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            // si il y a 2 scripts d'inventaire, c'est pour qu'on soit prévenu 
            Debug.LogWarning("Il y a plus d'une instance d'inventaire dans la scène");
            return;
        }
        instance = this;
    }
}
