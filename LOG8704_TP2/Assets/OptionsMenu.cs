using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        // Musique
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = MusicManager.Instance.GetVolume();
            musicVolumeSlider.onValueChanged.AddListener((v) =>
            {
                MusicManager.Instance.SetVolume(v);
            });
        }

        // Effets sonores
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = SFXManager.Instance.GetSFXVolume();
            sfxVolumeSlider.onValueChanged.AddListener((v) =>
            {
                SFXManager.Instance.SetSFXVolume(v);
            });
        }
    }
}
