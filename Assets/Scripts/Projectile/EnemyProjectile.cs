using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]private float speed;
    private float direction;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool hit;
    private float lifetime;
    public enum ProjectileType { SwordAttack, FireWormAttack, GoblinBomb,MushroomAttack,FlyingEyeAttack}
    private ProjectileType projectileType;
    private AudioManager audioManager;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D> ();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            hit = true;
            boxCollider.enabled = false;
            switch (projectileType)
            {
                case ProjectileType.GoblinBomb:
                    anim.SetTrigger("goblinBomb_hit");
                    DamagePlayer(collision,15);
                    break;
                case ProjectileType.FlyingEyeAttack:
                    anim.SetTrigger("flyingAttack_hit");
                    DamagePlayer(collision,10);
                    break;
                case ProjectileType.FireWormAttack:
                    anim.SetTrigger("fireWormAttack_hit");
                    DamagePlayer(collision,20);
                    break;
                case ProjectileType.SwordAttack:
                    anim.SetTrigger("sword_hit");
                    DamagePlayer(collision,20);
                    break;
                case ProjectileType.MushroomAttack:
                    anim.SetTrigger("mushroomAttack_hit");
                    DamagePlayer(collision,10);
                    break;
            }
        }
    }
    private void DamagePlayer(Collider2D collision,int _damage){
        if(collision.CompareTag("Player"))collision.GetComponent<Health>().TakeDamage(_damage);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        if(gameObject.CompareTag("SwordAttack")){
            projectileType = ProjectileType.SwordAttack;
        }
        else if(gameObject.CompareTag("MushroomAttack")){
            projectileType = ProjectileType.MushroomAttack;
        }
        else if(gameObject.CompareTag("FlyingEyeAttack")){
            projectileType = ProjectileType.FlyingEyeAttack;
        }
        else if(gameObject.CompareTag("FireWormAttack")){
            projectileType = ProjectileType.FireWormAttack;
        }
        else if(gameObject.CompareTag("GoblinBomb")){
            projectileType = ProjectileType.GoblinBomb;
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
        if (gameObject.CompareTag("FireWormAttack") || gameObject.CompareTag("GoblinBomb")) 
        {
            audioManager.PlaySFX(audioManager.fire_explosion);
        }else if (gameObject.CompareTag("SwordAttack")){
            //play sword hit
        }
        else if (gameObject.CompareTag("FlyingEyeAttack")){
            //play flying eye hit
        }
        else if (gameObject.CompareTag("MushroomAttack")){
            //play mushroom hit
        }

    }
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
