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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

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
                break;
            case 2:
                animTrigger = "fire_attack";
                selectedPool = fireSpells;
                break;
            case 3:
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
            Debug.LogWarning("No inactive projectiles available for " + WeaponWheelController.weaponID);
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

    public void UnlockAttack(string attackType){
        WeaponWheelController.instance.UnlockButton(attackType);
    }
}
