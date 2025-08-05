using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;


    private bool isJumping;
    private bool isGrounded;


    public Transform groundCheckLeft;
    public Transform groundCheckRight;


    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;


    // Calculer vitesse de mouvement
    void FixedUpdate()
    {
        // il crée une boite de collision entre les deux éléments, si sa entre en contacte avec quelque chose renvoie true 
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        // Calculer vitesse de mouvement horizontal
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Est ce que ya une demande de saut
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
        }

        // Effectuer le mouvement
        MovePlayer(horizontalMovement);

    }
    void MovePlayer(float _horizontalMovement)
    {
        // Calculer vélocité de notre cible ( personnage vers le prochain mouvement)
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, 0.05f);

        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}