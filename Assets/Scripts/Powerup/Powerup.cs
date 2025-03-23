using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("powerup");
            collision.gameObject.GetComponent<PlayerAttack>().UnlockAttack("F");
            gameObject.SetActive(false);
        }
    }
}
