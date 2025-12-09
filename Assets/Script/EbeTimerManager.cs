using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EbeTimerManager : MonoBehaviour
{
    [Header("Oyuncular")]
    [SerializeField] private GameObject kaktus;
    [SerializeField] private GameObject mantar;

    [Header("Oyun Ayarlarý")]
    [SerializeField] private float oyunSuresi = 120f;
    [SerializeField] private float ebelemeMenzili = 1.0f;
    [SerializeField] private float sonucGostermeSuresi = 10f;
    [SerializeField] private GameObject exclamationMarkPrefab;

    private float timeRemaining;
    private bool timerIsRunning = true;
    private bool ebelemeBekliyor = false;
    private bool oyunBitti = false;

    private GameObject ebeOlanOyuncu;
    private GameObject exclamationMark;
    private Coroutine exclamationMarkBlinkCoroutine;
    private UIManager uiManager;
    private MonoBehaviour[] kontrolScripts = new MonoBehaviour[2]; // 0: kaktus, 1: mantar

    // Renkler
    private static readonly Color parlakKirmizi = new Color(1f, 0.180f, 0.211f);
    private static readonly Color parlakYesil = new Color(0.423f, 1f, 0.180f);
    private static readonly Color saydam = new(0f, 0f, 0f, 0f);

    private void Awake()
    {
        // Kontrol scriptlerini önceden cache'le
        kontrolScripts[0] = kaktus.GetComponent<Cacmove>();
        kontrolScripts[1] = mantar.GetComponent<Mushmove>();
    }

    private void Start()
    {
        ebeOlanOyuncu = Random.value > 0.5f ? kaktus : mantar;
        timeRemaining = oyunSuresi;

        uiManager = Object.FindFirstObjectByType<UIManager>();
        uiManager?.InitializeUI();

        InstantiateExclamationMark(ebeOlanOyuncu);
    }

    private void Update()
    {
        if (oyunBitti) return;

        UpdateTimer();

        if (ebelemeBekliyor || !timerIsRunning) return;

        CheckEbelemeMesafesi();
    }

    private void UpdateTimer()
    {
        if (!timerIsRunning) return;

        timeRemaining -= Time.deltaTime;
        uiManager?.DisplayTime(timeRemaining);

        if (timeRemaining <= 0f)
        {
            timerIsRunning = false;
            EndGame();
        }
    }

    private void CheckEbelemeMesafesi()
    {
        GameObject digerOyuncu = ebeOlanOyuncu == kaktus ? mantar : kaktus;
        float mesafe = Vector3.Distance(ebeOlanOyuncu.transform.position, digerOyuncu.transform.position);

        if (mesafe <= ebelemeMenzili)
        {
            StartCoroutine(EbelemeGecikmesi(digerOyuncu));
        }
    }

    //private void InstantiateExclamationMark(GameObject oyuncu)
    //{
    //    if (exclamationMark != null)
    //    {
    //        Destroy(exclamationMark);
    //    }

    //    exclamationMark = Instantiate(exclamationMarkPrefab,
    //                               oyuncu.transform.position + Vector3.up * 1.7f,
    //                               Quaternion.identity,
    //                               oyuncu.transform);
    //    exclamationMark.SetActive(true);
    //    SetExclamationColor(parlakKirmizi);
    //}

    private void InstantiateExclamationMark(GameObject oyuncu)
    {
        if (exclamationMark != null)
        {
            Destroy(exclamationMark);
        }

        Vector3 spawnPos = oyuncu.transform.position + Vector3.up * 1.7f;
        exclamationMark = Instantiate(exclamationMarkPrefab, spawnPos, Quaternion.identity, oyuncu.transform);

        // Kameraya baksýn
        exclamationMark.transform.forward = Camera.main.transform.forward;

        exclamationMark.SetActive(true);
        SetExclamationColor(parlakKirmizi);
    }


    private IEnumerator EbelemeGecikmesi(GameObject yeniEbe)
    {
        ebelemeBekliyor = true;

        // Kontrol scriptini devre dýþý býrak
        int scriptIndex = yeniEbe == kaktus ? 0 : 1;
        if (kontrolScripts[scriptIndex] != null)
            kontrolScripts[scriptIndex].enabled = false;

        SwitchExclamationMark(yeniEbe);

        yield return new WaitForSeconds(2f);

        // Kontrol scriptini tekrar aktif et
        if (kontrolScripts[scriptIndex] != null)
            kontrolScripts[scriptIndex].enabled = true;

        ebeOlanOyuncu = yeniEbe;
        ebelemeBekliyor = false;
    }

    private void SwitchExclamationMark(GameObject yeniEbe)
    {
        // Eski coroutine'i durdur
        if (exclamationMarkBlinkCoroutine != null)
        {
            StopCoroutine(exclamationMarkBlinkCoroutine);
        }

        InstantiateExclamationMark(yeniEbe);
        exclamationMarkBlinkCoroutine = StartCoroutine(BlinkExclamationMark());
    }

    private IEnumerator BlinkExclamationMark()
    {
        Renderer exMarkRenderer = exclamationMark.GetComponent<Renderer>();
        float blinkDuration = 0.2f;
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < 2f)
        {
            exMarkRenderer.material.color = isVisible ? parlakYesil : saydam;
            isVisible = !isVisible;
            elapsedTime += blinkDuration;
            yield return new WaitForSeconds(blinkDuration);
        }

        SetExclamationColor(parlakKirmizi);
        exclamationMarkBlinkCoroutine = null;
    }

    private void SetExclamationColor(Color color)
    {
        if (exclamationMark != null)
        {
            exclamationMark.GetComponent<Renderer>().material.color = color;
        }
    }

    private void EndGame()
    {
        oyunBitti = true;
        string winner = ebeOlanOyuncu == kaktus ? mantar.name : kaktus.name;
        uiManager?.ShowGameResult(winner, ebeOlanOyuncu.name, sonucGostermeSuresi);
        StartCoroutine(LoadMainMenuDelayed());
    }

    private IEnumerator LoadMainMenuDelayed()
    {
        yield return new WaitForSeconds(sonucGostermeSuresi);
        SceneManager.LoadScene(0);
    }
}