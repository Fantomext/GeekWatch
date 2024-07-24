using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Tools;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider voiceSlider;
    [Space]
    [SerializeField] Slider senceSlider;

    private static string masterKey = "masterVolume";
    private static string musicKey = "musicVolume";
    private static string soundKey = "soundVolume";
    private static string voiceKey = "voiceVolume";

    private static string senceKey = "senceVolume";

    private void Start()
    {
        CheckKey(masterKey);
        CheckKey(musicKey);
        CheckKey(soundKey);
        CheckKey(voiceKey);

        if (PlayerPrefs.HasKey(senceKey))
            LoadVolume(senceKey);
        else
            SetSence();
    }

    public void SetSence()
    {
        float volume = senceSlider.value;
        PlayerPrefs.SetFloat(senceKey, volume);
    }

    public void CheckKey(string key)
    {
        if (PlayerPrefs.HasKey(key))
            LoadVolume(key);
        else
            SetVolume(key);
    }

    public void SetMaster() => SetVolume(masterKey);
    public void SetMusic() => SetVolume(musicKey);
    public void SetSound() => SetVolume(soundKey);
    public void SetVoice() => SetVolume(voiceKey);

    public void SetVolume(string key)
    {
        float volume = masterSlider.value;
        string mixerValue = "Master";

        switch (key)
        {
            case "masterVolume":
                volume = masterSlider.value;
                mixerValue = "Master";
                break;

            case "musicVolume":
                volume = musicSlider.value;
                mixerValue = "Music";
                break;

            case "soundVolume":
                volume = soundSlider.value;
                mixerValue = "Sound";
                break;

            case "voiceVolume":
                volume = voiceSlider.value;
                mixerValue = "Voice";
                break;
        }

        if (volume == 0) volume = 0.01f;
        mixer.SetFloat(mixerValue, Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat(musicKey, volume);
    }

    public void LoadVolume(string key)
    {
        musicSlider.value = PlayerPrefs.GetFloat(key);
        SetVolume(key);
    }

    public void ExitToMenu()
    {
        SceneLoader.Load(0);
    }
}
