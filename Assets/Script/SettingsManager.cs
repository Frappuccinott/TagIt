//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.UI;
//using TMPro;

//public class SettingsManager : MonoBehaviour
//{
//    [Header("Ses")]
//    public AudioMixer audioMixer;
//    public Slider musicSlider;
//    public Slider sfxSlider; // SFX için yeni slider

//    [Header("Görüntü")]
//    public TMP_Dropdown resolutionDropdown;
//    public Toggle fullscreenToggle;

//    private Resolution[] resolutions;

//    void Start()
//    {
//        LoadAudioSettings();
//        LoadResolutionSettings();
//    }

//    // ---------------- SES ----------------
//    public void SetMusicVolume(float volume)
//    {
//        float volumeToSet = Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20;
//        if (volume == 0f) volumeToSet = -80f;

//        audioMixer.SetFloat("MusicVolume", volumeToSet);
//        PlayerPrefs.SetFloat("MusicVolume", volume);
//        PlayerPrefs.Save();
//    }

//    public void SetSFXVolume(float volume)
//    {
//        float volumeToSet = Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20;
//        if (volume == 0f) volumeToSet = -80f;

//        audioMixer.SetFloat("SFXVolume", volumeToSet);
//        PlayerPrefs.SetFloat("SFXVolume", volume);
//        PlayerPrefs.Save();
//    }

//    private void LoadAudioSettings()
//    {
//        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
//        musicSlider.value = savedMusicVolume;
//        SetMusicVolume(savedMusicVolume);

//        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
//        sfxSlider.value = savedSFXVolume;
//        SetSFXVolume(savedSFXVolume);
//    }

//    // ---------------- GÖRÜNTÜ ----------------
//    public void SetResolution(int resolutionIndex)
//    {
//        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
//            return;

//        Resolution res = resolutions[resolutionIndex];
//        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
//        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
//        PlayerPrefs.Save();
//    }

//    public void SetFullscreen(bool isFullscreen)
//    {
//        Screen.fullScreen = isFullscreen;
//        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
//        PlayerPrefs.Save();
//    }

//    private void LoadResolutionSettings()
//    {
//        resolutions = Screen.resolutions;
//        resolutionDropdown.ClearOptions();

//        int currentResolutionIndex = 0;
//        var options = new System.Collections.Generic.List<string>();

//        for (int i = 0; i < resolutions.Length; i++)
//        {
//            string option = resolutions[i].width + " x " + resolutions[i].height;
//            options.Add(option);

//            if (resolutions[i].width == Screen.currentResolution.width &&
//                resolutions[i].height == Screen.currentResolution.height)
//            {
//                currentResolutionIndex = i;
//            }
//        }

//        resolutionDropdown.AddOptions(options);

//        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
//        resolutionDropdown.value = savedIndex;
//        resolutionDropdown.RefreshShownValue();

//        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
//        fullscreenToggle.isOn = isFullscreen;
//        Screen.fullScreen = isFullscreen;

//        SetResolution(savedIndex);
//    }

//    public void ResetToDefaults()
//    {
//        // Müzik ve SFX sesleri
//        float defaultVolume = 0.75f;
//        musicSlider.value = defaultVolume;
//        SetMusicVolume(defaultVolume);

//        float defaultSFXVolume = 0.75f;
//        sfxSlider.value = defaultSFXVolume;
//        SetSFXVolume(defaultSFXVolume);

//        // Fullscreen
//        fullscreenToggle.isOn = true;
//        SetFullscreen(true);

//        // Çözünürlük - en yüksek çözünürlüğü varsayalım
//        int defaultResolutionIndex = resolutions.Length - 1;
//        resolutionDropdown.value = defaultResolutionIndex;
//        resolutionDropdown.RefreshShownValue();
//        SetResolution(defaultResolutionIndex);

//        PlayerPrefs.Save();
//    }
//}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Ses")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider; // SFX için yeni slider

    [Header("Görüntü")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    [Header("Kontroller")]
    public TMP_Dropdown controlDropdown; // Klavye / Kontrolcü seçimi için

    private Resolution[] resolutions;

    void Start()
    {
        LoadAudioSettings();
        LoadResolutionSettings();
        LoadControlSettings(); // Kontrol ayarlarını da yükle
    }

    // ---------------- SES ----------------
    public void SetMusicVolume(float volume)
    {
        float volumeToSet = Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20;
        if (volume == 0f) volumeToSet = -80f;

        audioMixer.SetFloat("MusicVolume", volumeToSet);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        float volumeToSet = Mathf.Log10(Mathf.Clamp(volume, 0.001f, 1f)) * 20;
        if (volume == 0f) volumeToSet = -80f;

        audioMixer.SetFloat("SFXVolume", volumeToSet);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        musicSlider.value = savedMusicVolume;
        SetMusicVolume(savedMusicVolume);

        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        sfxSlider.value = savedSFXVolume;
        SetSFXVolume(savedSFXVolume);
    }

    // ---------------- GÖRÜNTÜ ----------------
    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
            return;

        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadResolutionSettings()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;

        SetResolution(savedIndex);
    }

    // ---------------- KONTROLLER ----------------
    public void SetControlScheme(int index)
    {
        // 0 = Klavye, 1 = Kontrolcü
        PlayerPrefs.SetInt("ControlScheme", index);
        PlayerPrefs.Save();

        if (index == 0)
        {
            Debug.Log("Klavye kontrol şeması seçildi.");
            // Burada klavye input aktif, kontrolcü pasif
        }
        else if (index == 1)
        {
            Debug.Log("Kontrolcü kontrol şeması seçildi.");
            // Şu an için kontrolcü desteği yok
            // Gelecekte buraya kontrolcü input sistemi gelecek
        }
    }

    private void LoadControlSettings()
    {
        // Dropdown seçeneklerini kod üzerinden ayarla
        controlDropdown.ClearOptions();
        List<string> controlOptions = new List<string> { "Klavye", "Kontrolcü" };
        controlDropdown.AddOptions(controlOptions);

        int savedScheme = PlayerPrefs.GetInt("ControlScheme", 0); // Varsayılan klavye
        controlDropdown.value = savedScheme;
        controlDropdown.RefreshShownValue();

        // Event bağla
        controlDropdown.onValueChanged.AddListener(SetControlScheme);

        // İlk seçim uygula
        SetControlScheme(savedScheme);
    }

    // ---------------- RESET ----------------
    public void ResetToDefaults()
    {
        // Müzik ve SFX sesleri
        float defaultVolume = 0.75f;
        musicSlider.value = defaultVolume;
        SetMusicVolume(defaultVolume);

        float defaultSFXVolume = 0.75f;
        sfxSlider.value = defaultSFXVolume;
        SetSFXVolume(defaultSFXVolume);

        // Fullscreen
        fullscreenToggle.isOn = true;
        SetFullscreen(true);

        // Çözünürlük - en yüksek çözünürlüğü varsayalım
        int defaultResolutionIndex = resolutions.Length - 1;
        resolutionDropdown.value = defaultResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(defaultResolutionIndex);

        // Kontroller - varsayılan Klavye
        controlDropdown.value = 0;
        controlDropdown.RefreshShownValue();
        SetControlScheme(0);

        PlayerPrefs.Save();
    }
}
