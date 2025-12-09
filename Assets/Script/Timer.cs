//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;
//using System.Collections;

//public class Timer : MonoBehaviour
//{
//    [Header("Oyuncular")]
//    public GameObject kaktus;
//    public GameObject mantar;

//    [Header("UI Bileþenleri")]
//    public TextMeshProUGUI kazananText;
//    public TextMeshProUGUI kaybedenText;
//    public TextMeshProUGUI timerText;
//    public TextMeshProUGUI menuanaText;

//    [Header("Oyun Ayarlarý")]
//    public float oyunSuresi = 120f;     // Süre saniye cinsinden
//    public float sonucGostermeSuresi = 10f;

//    private float timeRemaining;
//    private bool timerIsRunning = true;

//    private string winner = "";
//    private string loser = "";

//    public GameObject endgame;

//    void Start()
//    {
//        kazananText.gameObject.SetActive(false);
//        kaybedenText.gameObject.SetActive(false);
//        menuanaText.gameObject.SetActive(false);

//        timeRemaining = oyunSuresi;
//        endgame.SetActive(false);
//    }

//    void Update()
//    {
//        if (!timerIsRunning) return;

//        if (timeRemaining > 0f)
//        {
//            timeRemaining -= Time.deltaTime;
//            DisplayTime(timeRemaining);
//        }
//        else
//        {
//            timeRemaining = 0f;
//            timerIsRunning = false;

//            Debug.Log("Süre bitti!");
//            GameOver(GetCurrentEbe());
//            ShowResults();
//        }
//    }

//    void DisplayTime(float timeToDisplay)
//    {
//        if (timeToDisplay <= 0f)
//        {
//            timerText.text = "00:00";
//            return;
//        }

//        int minutes = Mathf.FloorToInt(timeToDisplay / 60f);
//        int seconds = Mathf.FloorToInt(timeToDisplay % 60f);
//        timerText.text = $"{minutes:00}:{seconds:00}";
//    }

//    void GameOver(GameObject lastEbePlayer)
//    {
//        string lastEbe = lastEbePlayer.name;

//        if (lastEbe == kaktus.name)
//        {
//            winner = mantar.name;
//            loser = kaktus.name;
//        }
//        else
//        {
//            winner = kaktus.name;
//            loser = mantar.name;
//        }
//    }

//    void ShowResults()
//    {
//        kazananText.text = $"Kazanan: {winner}";
//        kaybedenText.text = $"Kaybeden: {loser}";

//        kazananText.gameObject.SetActive(true);
//        kaybedenText.gameObject.SetActive(true);
//        menuanaText.gameObject.SetActive(true);

//        StartCoroutine(ReturnAfterDelay());
//        endgame.SetActive(true);
//    }

//    IEnumerator ReturnAfterDelay()
//    {
//        yield return new WaitForSeconds(sonucGostermeSuresi);
//        SceneManager.LoadScene(0);
//    }
//}
