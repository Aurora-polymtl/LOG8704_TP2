using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip pickupSound;
    public AudioClip successSound;
    public AudioClip failSound;
    public AudioClip endGameSound;

    private float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void PlayClick() => audioSource.PlayOneShot(clickSound, sfxVolume);
    public void PlayPickup() => audioSource.PlayOneShot(pickupSound, sfxVolume);
    public void PlaySuccess() => audioSource.PlayOneShot(successSound, sfxVolume);
    public void PlayFail() => audioSource.PlayOneShot(failSound, sfxVolume);
    public void PlayEndGame() => audioSource.PlayOneShot(endGameSound, sfxVolume);

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    private void Start()
    {
        sfxVolume = GetSFXVolume();
    }
}

