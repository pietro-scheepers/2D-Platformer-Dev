using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]private float speed;
    private Animator anim;
    private bool grounded;
    private void Awake()
    {
        //Grab reference for rigidbody and animator frm object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput*speed,body.linearVelocity.y);
        
        //Flip player when moving left or right
        if (horizontalInput>0.01f)
        {
            transform.localScale = new Vector3(2,2,2);
            
        }
        else if (horizontalInput<-0.01f){
            transform.localScale = new Vector3(-2,2,2);
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded",grounded);
    }
    private void Jump(){
        body.linearVelocity = new Vector2(body.linearVelocity.x,speed/2);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
