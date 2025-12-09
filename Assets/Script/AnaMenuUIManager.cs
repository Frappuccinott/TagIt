using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuUIManager : MonoBehaviour
{
    public GameObject playpanel;
    public GameObject creditsgspanel;
    public GameObject settingsPanel;

    public void PlayPanel()
    {
        playpanel.SetActive(true);
    }   
    public void PlayBackMenu()
    {
        playpanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Winter()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Winter");
    }
    public void Desert()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Desert");
    }
    public void Forest()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Forest");
    }
    public void GamePark()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GamePark");
    }
    public void OpenCreditsMenu()
    {
        creditsgspanel.SetActive(true);
    }
    public void CloseCreditsMenu()
    {
        creditsgspanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
