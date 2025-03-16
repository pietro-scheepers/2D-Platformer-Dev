using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private GameObject[] fireSpells;
    [SerializeField] private GameObject[] iceSpells;

    private PlayerMovement playerMovement;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private string activeAttack = "A"; // "A" for Arrow, "F" Fire, "I" Ice

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            Attack();
        }
    }

    private void Attack()
    {
        string animTrigger = "";
        GameObject[] selectedPool = null;

        switch (activeAttack)
        {
            case "A":
                animTrigger = "arrow_attack";
                selectedPool = arrows;
                break;
            case "F":
                animTrigger = "fire_attack";
                selectedPool = fireSpells;
                break;
            case "I":
                animTrigger = "ice_attack";
                selectedPool = iceSpells;
                break;
            default:
                Debug.LogError("Unknown attack type!");
                return;
        }

        // Trigger animation
        anim.SetTrigger(animTrigger);
        cooldownTimer = 0;

        // Find available projectile
        int attackIndex = FindInactiveProjectile(selectedPool);

        if (attackIndex == -1)
        {
            Debug.LogWarning("No inactive projectiles available for " + activeAttack);
            return;
        }

        // Launch projectile
        GameObject projectileObject = selectedPool[attackIndex];
        projectileObject.transform.position = firePoint.position;

        Projectile projectile = projectileObject.GetComponent<Projectile>();
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

    public void SetActiveAttack(string attackType)
    {
        activeAttack = attackType;
    }
}
