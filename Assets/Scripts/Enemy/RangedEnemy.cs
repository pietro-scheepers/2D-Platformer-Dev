using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
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
    [Header("References")]
    public Transform enemy;
    private Animator anim;
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
                RangedAttack();
            }   
        }
        if (enemyPatrol!=null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack(){
        //finding available projectile
        int attackIndex = FindProjctile();
         if (attackIndex == -1)
        {
            Debug.LogWarning("No inactive projectiles available for enemy");
            return;
        }
        //trigger animation
        anim.SetTrigger("rangedAttack");;
        cooldownTimer = 0;
        //launch projectile
        GameObject projectileObject = projectiles[attackIndex];
        projectileObject.transform.position = firePoint.position;
        EnemyProjectile projectile = projectileObject.GetComponent<EnemyProjectile>();
        if (projectile!=null)
        {
            int direction = GetDirection(); 
            projectile.SetDirection(direction);
        }
    }
    private int FindProjctile(){
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
    private int GetDirection()
    {
        return (int)Mathf.Sign(enemy.localScale.x);
    }
}
