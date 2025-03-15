using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCooldown;
    [SerializeField]private Transform firePoint;
    [SerializeField]private GameObject[] arrows;
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
        if (Input.GetMouseButtonDown(0) && cooldownTimer>attackCooldown && playerMovement.CanAttack())
        {
            Attack();
            cooldownTimer += Time.deltaTime;
        }
    }

    private void Attack()
    {
        anim.SetTrigger("arrow_attack");
        cooldownTimer = 0;

        int arrowIndex = FindArrow();
        if(arrowIndex == -1){
            return;
        }
        if (arrows[arrowIndex] == null)
        {
            Debug.LogError("Arrow at index " + arrowIndex + " is null!");
            return;
        }

        arrows[arrowIndex].transform.position = firePoint.position;
        
        Projectile projectile = arrows[arrowIndex].GetComponent<Projectile>();
        if (projectile == null)
        {
            Debug.LogError("Projectile component missing on arrow at index " + arrowIndex);
            return;
        }
        int direction = playerMovement.GetDirection()?1:-1;
        projectile.SetDirection(direction);
    }

    private int FindArrow(){
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }    
        Debug.LogError("No inactive arrows available! Returning -1.");
        return -1;
    }
}
