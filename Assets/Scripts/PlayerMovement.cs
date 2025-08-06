using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;


    private bool isJumping;
    private bool isGrounded;


    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;



    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    void Update()
    {
        // Est ce que ya une demande de saut
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
        }

        Flip(rb.linearVelocity.x);

        // envoie la vitesse horizontal
        float characterVelocity = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", rb.linearVelocity.x);
    }

       
        void FixedUpdate()
         {
        // il crée une boite de collision
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        // Calculer vitesse de mouvement horizontal
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
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

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f) 
        {
            spriteRenderer.flipX = false;
        } 
        else if(_velocity < -0.1f) 
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnDrawGizmos()
    {
        // Dessine le cercle de vérification du sol
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}