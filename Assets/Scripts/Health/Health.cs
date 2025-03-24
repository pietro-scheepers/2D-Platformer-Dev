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

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
        bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
              audioManager.PlaySFX(audioManager.take_damage);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                audioManager.PlaySFX(audioManager.death);
                GetComponent<PlayerMovement>().enabled = false;   
                dead = true;
            }
        }
    }
    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);
    }
}
