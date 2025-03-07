using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public float speed;
    [SerializeField] public float jumpHeight; 
    private float horizontalInput;
    private bool facingRight = true;
 
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //facing left or right
        if (horizontalInput<0f && facingRight == true){
            transform.eulerAngles = new Vector3(0f,-180f ,0f);
            facingRight= false;
        }
        else if (horizontalInput>0f && facingRight == false){
            transform.eulerAngles = new Vector3(0f,0f ,0f);
            facingRight= true;
            }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(horizontalInput*speed, body.linearVelocityY); //move horizontal
    }

    void Jump(){
        body.linearVelocity = new Vector2(body.linearVelocityX, jumpHeight) ;
    }
}
