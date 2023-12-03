using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static bool IsEasyMode = false;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle isEasyModeToggle;

    private void Start()
    {
        if(PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        if (PlayerPrefs.HasKey("isEasyMode"))
        {
            LoadIsEasyMode();
        }
    }

    public void SetMasterVolume()
    {
        float volume = masterVolumeSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicVolumeSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxVolumeSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void ToggleEasyMode()
    {
        if(isEasyModeToggle.isOn)
        {
            IsEasyMode = true;
        }
        else
        {
            IsEasyMode = false;
        }
        PlayerPrefs.SetInt("isEasyMode", BoolToInt(IsEasyMode));
    }

    private int BoolToInt(bool val)
    {
        return val ? 1 : 0;
    }

    private bool IntToBool(int val)
    {
        return val == 1;
    }

    private void LoadIsEasyMode()
    {
        IsEasyMode = IntToBool(PlayerPrefs.GetInt("isEasyMode"));
        isEasyModeToggle.isOn = IsEasyMode;
    }
}