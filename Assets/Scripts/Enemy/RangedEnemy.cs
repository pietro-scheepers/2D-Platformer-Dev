using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damage;
    [SerializeField] private float range;

    [Header("RangedAttack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    
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

    private void Awake(){
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer+= Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }   
        }
        if (enemyPatrol!=null){
                enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack(){
        cooldownTimer = 0;
        projectiles[FindProejctile()].transform.position = firePoint.position;
      //  projectiles[FindProejctile()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindProejctile(){
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z)
        ,0,Vector2.left,0,playerLayer);
        return hit.collider!=null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }
}
