using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source --------")]
    [SerializeField]private AudioSource musicSource;
    [SerializeField]private AudioSource sfxSource;
    
    [Header("-------- Audio Clip --------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip arrow;
    public AudioClip ice;
    public AudioClip fire;
    public AudioClip fire_explosion;
    public AudioClip gravity;
    public AudioClip health_collectible;
    public AudioClip powerup;
    public AudioClip take_damage;
    public AudioClip sword_attack1;
    public AudioClip sword_attack2;
    public AudioClip sword_ranged_attack;
    public AudioClip skeleton_death;
    public AudioClip goblin_attack1;
    public AudioClip goblin_attack2;
    public AudioClip goblin_bomb;
    public AudioClip goblin_death;
    public AudioClip flying_eye_range;
    public AudioClip mushroom_range;
    public AudioClip demon_attack;
    public AudioClip demon_death;
    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
}
