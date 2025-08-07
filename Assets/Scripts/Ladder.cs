using UnityEngine;

public class Ladder : MonoBehaviour
{

    private bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D collider;

   
    void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

   
    void Update()
    {
        if(playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            // descendre de l'échelle
            playerMovement.isClimbing = false;
            collider.isTrigger = false;
            return;
        }
        // Si le joueur est dans la zone de la échelle et appuie sur E, il commence à grimper
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            collider.isTrigger = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovement.isClimbing = false;
            collider.isTrigger = false;
        }
    }
}
