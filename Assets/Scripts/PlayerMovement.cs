using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] public float speed;
    [SerializeField] public float jumpHeight; 
    private float horizontalInput;
    private bool facingRight = true;
    private bool isGrounded = true;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } 

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal"); //movement

        //facing left or right
        if (horizontalInput<0f && facingRight == true){
            transform.eulerAngles = new Vector3(0f,-180f ,0f);
            facingRight= false;
        }
        else if (horizontalInput>0f && facingRight == false){
            transform.eulerAngles = new Vector3(0f,0f ,0f);
            facingRight= true;
            }

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {

            Jump();
        }

        if (Mathf.Abs(horizontalInput)>0.1f){ //check running
            anim.SetFloat("run",1f);
        }else if (Mathf.Abs(horizontalInput)<0.1f) anim.SetFloat("run",0f);

        //checking when the player is falling
        if (body.linearVelocityY < 0f && isGrounded == false)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump",false);
        }
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocityY); //move horizontal
    }

    void Jump(){
        isGrounded = false;
        anim.SetBool("jump",true);
        body.linearVelocity = new Vector2(body.linearVelocityX, jumpHeight) ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            anim.SetBool("fall",false);
        }
    }
}
