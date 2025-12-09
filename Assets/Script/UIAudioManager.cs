using UnityEngine;
using UnityEngine.UI;

public class UIAudioManager : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    void Awake()
    {
        // AudioSource yoksa otomatik ekle
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Start()
    {
        // Baþlangýçta tüm butonlarý bul (aktif ve gizli)
        CheckForNewButtons();

        // Her 2 saniyede bir yeni butonlarý kontrol et (isteðe baðlý)
        InvokeRepeating("CheckForNewButtons", 0f, 2f);
    }

    void CheckForNewButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button button in buttons)
        {
            // Ayný listener'ý tekrar eklememek için kontrol
            button.onClick.RemoveListener(PlayButtonClickSound); // Önce kaldýr
            button.onClick.AddListener(PlayButtonClickSound);    // Sonra ekle
        }
    }

    void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
            Debug.Log("Ses çalýndý!"); // Test için
        }
        else
        {
            Debug.LogWarning("Ses çalýnamadý: AudioSource veya AudioClip eksik!");
        }
    }
}