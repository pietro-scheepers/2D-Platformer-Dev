using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private GameObject[] fireSpells;
    [SerializeField] private GameObject[] iceSpells;

    AudioManager audioManager;

    private PlayerMovement playerMovement;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            default:
                Debug.LogError("Unknown attack type!");
                return;
        }

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
