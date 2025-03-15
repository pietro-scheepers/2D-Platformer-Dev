using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] public float speed;
    [SerializeField] public float jumpHeight; 
    [SerializeField] public LayerMask groundLayer;
    private float horizontalInput;
    private bool facingRight = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    } 

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // Movement

        // Flip the character when changing direction
        if (horizontalInput < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;
        }
        else if (horizontalInput > 0f && !facingRight)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;
        }

        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        // Running animation
        anim.SetFloat("run", Mathf.Abs(horizontalInput));

        // Fall detection (only trigger falling when moving downward)
        if (body.linearVelocityY < -0.1f && !IsGrounded())
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
        else if (body.linearVelocityY > 0.1f)
        {
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        }else if (IsGrounded())
        {
            anim.SetBool("fall",false);
            anim.SetBool("jump",false);
        }

        if (Input.GetMouseButtonDown(0)) // Attack
        {
            anim.SetTrigger("hit");
        }
    }

    void FixedUpdate()
    {
        // Move the character
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocityY);
    }

    void Jump()
    {
        anim.SetBool("jump", true);
        body.linearVelocity = new Vector2(body.linearVelocityX, jumpHeight);
    }

    private bool IsGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0,Vector2.down,0.15f,groundLayer);
        return raycastHit.collider!=null;
    }
}