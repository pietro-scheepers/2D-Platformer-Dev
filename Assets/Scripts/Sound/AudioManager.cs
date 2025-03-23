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
    private void Start(){
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }
}
