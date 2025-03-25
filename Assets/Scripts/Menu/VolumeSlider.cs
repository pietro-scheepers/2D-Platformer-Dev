using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Scrollbar scrollbar;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        scrollbar.value = savedVolume;
        scrollbar.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.UpdateVolume(value);
        }
    }
}
