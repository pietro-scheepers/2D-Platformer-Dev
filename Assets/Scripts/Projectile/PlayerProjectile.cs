using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private float direction;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit;
    private float lifetime;
    public enum ProjectileType { Arrow, Fire, Ice }
    private ProjectileType projectileType;
    private AudioManager audioManager;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D> ();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            switch (projectileType)
            {
                case ProjectileType.Arrow:
                    anim.SetTrigger("arrow_hit");
                    DamageEnemy(collision,5);
                    break;
                case ProjectileType.Fire:
                    anim.SetTrigger("fire_hit");
                    DamageEnemy(collision,20);
                    break;
                case ProjectileType.Ice:
                    anim.SetTrigger("ice_hit");
                    DamageEnemy(collision,20);
                    break;
            }
        }
    }
    private void DamageEnemy(Collider2D collision,int _damage){
        if(collision.CompareTag("Enemy"))collision.GetComponent<Health>().TakeDamage(_damage);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        if (gameObject.CompareTag("IceAttack"))
        {
            projectileType = ProjectileType.Ice;
        }else if (gameObject.CompareTag("FireAttack")){
            projectileType = ProjectileType.Fire;
        }else if (gameObject.CompareTag("ArrowAttack")){
            projectileType = ProjectileType.Arrow;
        }

        hit = false;
        boxCollider.enabled = true; 

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction){
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }
    
    private void Explosion(){
        if (gameObject.CompareTag("FireAttack"))
        {
            audioManager.PlaySFX(audioManager.fire_explosion);
        }
    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
