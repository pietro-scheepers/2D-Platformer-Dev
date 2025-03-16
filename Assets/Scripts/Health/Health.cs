using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float StartingHealth => startingHealth;
    [SerializeField]private Healthbar bar;
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.D))
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
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
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
