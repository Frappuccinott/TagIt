using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panelleri")]
    [SerializeField] private GameObject endgamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject rulespanel;

    [Header("Yazýlar")]
    [SerializeField] private TextMeshProUGUI kazananText;
    [SerializeField] private TextMeshProUGUI kaybedenText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI anamenuText;

    private bool isPaused = false;

    public void InitializeUI()
    {
        endgamePanel.SetActive(false);
    }

    public void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= 0f)
        {
            timerText.text = "00:00";
            return;
        }

        int minutes = Mathf.FloorToInt(timeToDisplay / 60f);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ShowGameResult(string winner, string loser, float countdownTime)
    {
        kazananText.text = $"Winner: {winner}";
        kaybedenText.text = $"Loser: {loser}";
        anamenuText.text = $"Returning to Main Menu in {countdownTime:F0} seconds.";
        //anamenu.text = $"{sonucGostermeSuresi} saniye sonra ana\r\nmenüye dönecek";

        endgamePanel.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Oyunu durdur
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Oyunu devam ettir
            pausePanel.SetActive(false);
        }
    }
    public void RefreshScene()
    {
        Time.timeScale = 1f; // Oyun hýzýný normale döndür
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
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
