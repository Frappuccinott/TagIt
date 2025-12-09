using UnityEngine;
using UnityEngine.UI;

public class RulesManager : MonoBehaviour
{
    // UI öðeleri
    public Button CharacterControlButton;
    public Button GameInfoButton;
    public GameObject CharacterControlText;
    public GameObject GameInfoText;
    public GameObject rulespanel;

    // Butonlarýn renkleri
    public Color normalColor = Color.white;
    public Color activeColor = Color.gray;

    void Start()
    {
        // Baþlangýçta "CharacterControl" sekmesini gösterelim
        ShowCharacterControl();

        // Butonlara onClick listener ekleyelim
        CharacterControlButton.onClick.AddListener(ShowCharacterControl);
        GameInfoButton.onClick.AddListener(ShowGameInfo);

        // Butonlarýn üzerlerine gelindiðinde rengin deðiþmemesi için
        SetButtonHoverState(CharacterControlButton, normalColor);
        SetButtonHoverState(GameInfoButton, normalColor);
    }

    // "CharacterControl" sekmesini göster
    void ShowCharacterControl()
    {
        // Ýlgili metni göster
        CharacterControlText.SetActive(true);
        GameInfoText.SetActive(false);

        // Buton rengini deðiþtir
        SetButtonState(CharacterControlButton, true);
        SetButtonState(GameInfoButton, false);
    }

    // "GameInfo" sekmesini göster
    void ShowGameInfo()
    {
        // Ýlgili metni göster
        CharacterControlText.SetActive(false);
        GameInfoText.SetActive(true);

        // Buton rengini deðiþtir
        SetButtonState(CharacterControlButton, false);
        SetButtonState(GameInfoButton, true);
    }

    // Butonun aktif olup olmadýðýný kontrol et ve rengi deðiþtir
    void SetButtonState(Button button, bool isActive)
    {
        var colors = button.colors;
        colors.normalColor = isActive ? activeColor : normalColor;
        colors.selectedColor = isActive ? activeColor : normalColor;
        colors.pressedColor = isActive ? activeColor : normalColor;
        button.colors = colors;
    }

    // Buton üzerine gelindiðinde rengin deðiþmemesini saðla
    void SetButtonHoverState(Button button, Color color)
    {
        var colors = button.colors;
        colors.highlightedColor = color;  // Hover (üzerine gelme) rengini normal yapýyoruz
        button.colors = colors;
    }

    public void OpenRulesMenu()
    {
        rulespanel.SetActive(true);
    }
    public void CloseRulesMenu()
    {
        rulespanel.SetActive(false);
    }
}
