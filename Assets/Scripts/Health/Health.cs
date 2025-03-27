using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float StartingHealth => startingHealth;
    [SerializeField]private Healthbar bar;
    [SerializeField]private EnemyHealthBar enemy_bar;
    private Animator anim;
    private bool dead;
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private AudioManager audioManager;
    private bool isPlayer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        isPlayer = CompareTag("Player");
        if (!isPlayer){enemy_bar.SetHealth(StartingHealth,startingHealth);        Debug.Log(enemy_bar);
}
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (isPlayer){bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);}
        else{
            enemy_bar.SetHealth(currentHealth,startingHealth);
        }
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
                    if (GetComponent<PlayerMovement>()!=null)GetComponent<PlayerMovement>().enabled = false; 
                }else{
                    //enemy
                    foreach (Behaviour component in components)
                    {
                        component.enabled = false;
                    }
                    PlayDeathEffect();
                }
                  
                dead = true;
            }
        }
    }

    private void PlayDeathEffect()
    {
        string enemyName = GetComponent<EnemyTag>().GetEnemyName();
        switch (enemyName)
        {
            case "Skeleton": audioManager.PlaySFX(audioManager.skeleton_death);break;
            case "Goblin": audioManager.PlaySFX(audioManager.goblin_death);break;            
            case "FlyingEye": audioManager.PlaySFX(audioManager.goblin_death);break;
            case "Mushroom": audioManager.PlaySFX(audioManager.goblin_death);break;            
            case "Demon_Slime": audioManager.PlaySFX(audioManager.demon_death);break;
            case "Frost_Guardian": audioManager.PlaySFX(audioManager.demon_death);break;
        }
    }
    public void Deactivate(){
        gameObject.SetActive(false);
    }
    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        if(isPlayer) bar.fillAmount = bar.Map(currentHealth,0,startingHealth,0,1);
    }
}
