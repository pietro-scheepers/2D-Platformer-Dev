using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private float direction;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit;
    private float lifetime;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D> ();
    }

    // Update is called once per frame
    private void Update()
    {
        if(hit)return;
        float movementSpeed = speed*Time.deltaTime*direction;
        transform.Translate(movementSpeed,0,0);
        lifetime += Time.deltaTime;
        if (lifetime>5)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            hit = true;
            boxCollider.enabled = false;
            anim.SetTrigger("arrow_hit");
        }
        
    }

    public void SetDirection(float _direction)
    {
        if (gameObject == null)
        {
            Debug.LogError("Projectile gameObject is null!");
            return;
        }
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D is NULL on " + gameObject.name);
            return;
        }
        boxCollider.enabled = true; 

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction){
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
