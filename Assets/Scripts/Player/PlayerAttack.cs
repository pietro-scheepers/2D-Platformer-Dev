using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
     [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField]private float levitationAttackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int levitation_damage;
    
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private GameObject[] fireSpells;
    [SerializeField] private GameObject[] iceSpells;
       
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float range;
    
    [Header("Enemy Layer")]
    [SerializeField] private LayerMask enemyLayer;
    AudioManager audioManager;

    private PlayerMovement playerMovement;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    
    private float levitationCooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        levitationCooldownTimer+=Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack() && !WeaponWheelController.isWeaponWheelOpen)
        {
            Attack();
        }
    }

    private void Attack()
    {
        string animTrigger = "";
        GameObject[] selectedPool = null;

        switch (WeaponWheelController.weaponID)
        {
            case 1:
                animTrigger = "arrow_attack";
                selectedPool = arrows;
                audioManager.PlaySFX(audioManager.arrow);
                break;
            case 2:
                animTrigger = "fire_attack";
                selectedPool = fireSpells;
                audioManager.PlaySFX(audioManager.fire);
                break;
            case 3:
                animTrigger = "ice_attack";
                selectedPool = iceSpells;
                audioManager.PlaySFX(audioManager.ice);
                break;
            case 4:
                if (levitationCooldownTimer > levitationAttackCooldown){
                    LevitateAttack();
                }
                break;
            default:
                Debug.LogError("Unknown attack type!");
                return;
        }
        if(WeaponWheelController.weaponID!=4){
            // Find available projectile
            int attackIndex = FindInactiveProjectile(selectedPool);

            if (attackIndex == -1)
            {
                Debug.LogWarning("No inactive projectiles available for " + WeaponWheelController.weaponID);
                return;
            }
            // Trigger animation
            anim.SetTrigger(animTrigger);
            cooldownTimer = 0;

            // Launch projectile
            GameObject projectileObject = selectedPool[attackIndex];
            projectileObject.transform.position = firePoint.position;
            PlayerProjectile projectile = projectileObject.GetComponent<PlayerProjectile>();
            if (projectile != null)
            {
                int direction = playerMovement.GetDirection() ? 1 : -1;
                projectile.SetDirection(direction);
            }
            else
            {
                Debug.LogError("Projectile component missing on prefab.");
            }
        }
    }

    private int FindInactiveProjectile(GameObject[] pool)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1; // No available projectile
    }

    public void UnlockAttack(string attackType){
        WeaponWheelController.instance.UnlockButton(attackType);
    }
    private void LevitateAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            enemyLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            levitationCooldownTimer = 0;
            anim.SetTrigger("levitate");
            GameObject enemy = hit.collider.gameObject;
            if (enemy.GetComponentInParent<EnemyPatrol>()!=null)enemy.GetComponentInParent<EnemyPatrol>().enabled = false;
            if (enemy.GetComponent<MeleeEnemy>()!=null)enemy.GetComponent<MeleeEnemy>().enabled =false;
            if (enemy.GetComponent<RangedEnemy>()!=null)enemy.GetComponent<RangedEnemy>().enabled =false;
           
        }
    }
    private void LevitateAttackEffect() //called as animation event
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            enemyLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            audioManager.PlaySFX(audioManager.gravity);
            GameObject enemy = hit.collider.gameObject;
            StartCoroutine(FakeLevitate(enemy)); 
        }
    }
    private IEnumerator FakeLevitate(GameObject enemy)
    {
        Health health = enemy.GetComponent<Health>();
        float floatTime = 6f;
        float time = 0f;

        Vector3 originalPos = enemy.transform.position;
        float liftSpeed = 6f;

        Vector3 targetLiftPos = enemy.transform.position + Vector3.up * liftSpeed;

        while (time < floatTime)
        {
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetLiftPos, Time.deltaTime * 3f);
            health.TakeDamage(levitation_damage);
            time += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }

        float dropSpeed = 6f;
        float groundY = originalPos.y;
        while (enemy.transform.position.y > groundY)
        {
            enemy.transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            yield return null;
        }
        enemy.transform.position = new Vector3(enemy.transform.position.x, groundY, enemy.transform.position.z);
        if (enemy.GetComponentInParent<EnemyPatrol>()!=null)enemy.GetComponentInParent<EnemyPatrol>().enabled = true;
        if (enemy.GetComponent<MeleeEnemy>()!=null)enemy.GetComponent<MeleeEnemy>().enabled =true;
        if (enemy.GetComponent<RangedEnemy>()!=null)enemy.GetComponent<RangedEnemy>().enabled =true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,
        new Vector3(boxCollider.bounds.size.x*range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }
}
