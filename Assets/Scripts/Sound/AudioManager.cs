using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("-------- Audio Source --------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

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
    private float volume = 1f;
    private const string VolumeKey = "Volume"; // Key for PlayerPrefs

    private void Awake()
    {
        // Singleton pattern to persist AudioManager across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolume();
    }
   
  

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void UpdateVolume(float newVolume)
    {
        volume = newVolume;
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
        ApplyVolume();
    }

    private void LoadVolume()
    {
        volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
    }
}
