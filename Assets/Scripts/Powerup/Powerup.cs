using UnityEngine;

public class Powerup : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("powerup");
            audioManager.PlaySFX(audioManager.powerup);
            collision.gameObject.GetComponent<PlayerAttack>().UnlockAttack("F");
            gameObject.SetActive(false);
        }
    }
}
