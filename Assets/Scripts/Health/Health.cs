using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float StartingHealth => startingHealth;
    [SerializeField]private Healthbar bar;
    private Animator anim;
    private bool dead;
    private AudioManager audioManager;
    private bool isPlayer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isPlayer = CompareTag("Player");
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (isPlayer)bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            if (isPlayer) audioManager.PlaySFX(audioManager.take_damage);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                if (isPlayer)
                {
                    //player
                    audioManager.PlaySFX(audioManager.death);
                    if (GetComponentInParent<PlayerMovement>()!=null)GetComponent<PlayerMovement>().enabled = false; 
                }else{
                    //enemy
                    if (GetComponentInParent<EnemyPatrol>()!=null)GetComponentInParent<EnemyPatrol>().enabled = false;
                    if (GetComponentInParent<MeleeEnemy>()!=null)GetComponentInParent<MeleeEnemy>().enabled =false;
                }
                  
                dead = true;
            }
        }
    }
    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        if(isPlayer) bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);
    }
}
