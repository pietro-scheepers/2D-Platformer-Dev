using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("powerup");
            anim.SetBool("power",true);
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerAttack>().SetActiveAttack("F");
        }
    }
}
