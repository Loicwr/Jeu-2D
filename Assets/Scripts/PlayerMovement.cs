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
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Est ce que ya une demande de saut
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
        }
    }

        // Calculer vitesse de mouvement
        void FixedUpdate()
         {
        // il crée une boite de collision entre les deux éléments, si sa entre en contacte avec quelque chose renvoie true 
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        // Calculer vitesse de mouvement horizontal
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

       

        // Effectuer le mouvement
        MovePlayer(horizontalMovement);

        Flip(rb.linearVelocity.x);

        // envoie la vitesse horizontal
        float characterVelocity = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", rb.linearVelocity.x);

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
}