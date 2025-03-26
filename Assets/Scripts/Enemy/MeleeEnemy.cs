using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private float range;
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    
    //references
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private AudioManager audioManager;
    private string enemyName;
    private void Awake(){
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        enemyName = GetComponent<EnemyTag>().GetEnemyName();
    }

    private void Update()
    {
        cooldownTimer+= Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                int attack = Random.Range(0, 2);
                PlayMeeleSound(attack);
                if ((attack==0) || HasOneAttack())
                {
                    anim.SetTrigger("meeleAttack1");
                }
                else{
                    anim.SetTrigger("meeleAttack2");
                }
            }   
        }
        if (enemyPatrol!=null){
                enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool HasOneAttack()
    {
        return enemyName =="Demon_Slime" || enemyName == "Frost_Guardian";
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z)
        ,0,Vector2.left,0,playerLayer);

        if (hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider!=null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        if (PlayerInSight()){
            playerHealth.TakeDamage(_damage);
        }
    }
    private void PlayMeeleSound(int attack){
        if (enemyName=="Skeleton")
        {
            switch (attack)
            {
                case 0: audioManager.PlaySFX(audioManager.sword_attack1);break;
                case 1: audioManager.PlaySFX(audioManager.sword_attack2);break;
            }   
        }else if (enemyName=="Goblin"){
            switch (attack)
            {
                case 0: audioManager.PlaySFX(audioManager.sword_attack2);break;
                case 1: audioManager.PlaySFX(audioManager.goblin_attack2);break;
            }
        }
        else if (enemyName=="FlyingEye"){
            switch (attack)
            {
                case 0: audioManager.PlaySFX(audioManager.goblin_attack2);break;
                case 1: audioManager.PlaySFX(audioManager.sword_attack2);break;
            }
        }
        else if (enemyName=="Mushroom"){
            audioManager.PlaySFX(audioManager.goblin_attack1);
        }
        else if (enemyName=="Demon_Slime"){
            audioManager.PlaySFX(audioManager.demon_attack);
        }else if (enemyName =="Frost_Guardian"){
            audioManager.PlaySFX(audioManager.demon_attack);
        }
    }
}
