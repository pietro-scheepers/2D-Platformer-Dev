using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField]private float healthValue;
    private AudioManager audioManager;
    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            audioManager.PlaySFX(audioManager.health_collectible);
            gameObject.SetActive(false);
        }
    }
}
