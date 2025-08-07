using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{

    private bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D topCollider;
    public Text interactUI;

   
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

   
    void Update()
    {
        if(isInRange && playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            // descendre de l'échelle
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            return;
        }
        // Si le joueur est dans la zone de la échelle et appuie sur E, il commence à grimper
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            topCollider.isTrigger = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false;
            if (interactUI != null)
            interactUI.enabled = false;
        }
    }
}
