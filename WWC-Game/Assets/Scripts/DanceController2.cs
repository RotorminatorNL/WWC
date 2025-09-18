using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DanceController2 : MonoBehaviour
{
    [Header("Gameplay")]
    public GameObject circlePrefab;
    public RectTransform spawnArea;
    public Image flashScreen;

    [Header("Lives")]
    public Image[] hearts;           
    public int startingLives = 3;

    [Header("Audio")]
    public AudioSource audioSource;  
    public AudioClip missAudioClip;   
    public AudioClip failAudioClip;  
    public AudioClip winAudioClip;  
    public AudioClip hitAudioClip;  

    [Header("UI Panels")]
    public GameObject failedScreen;   
    public GameObject winScreen;      

    [Header("Progress")]
    public Text progressText;         

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;       
    public float minInterval = 0.8f;       
    public float intervalDecreaseRate = 0.01f; 

    private int lives;
    private bool isGameOver = false;
    private int hitCount = 0;
    private int winTarget = 21;

    void Start()
    {
        lives = startingLives;

        if (failedScreen != null) failedScreen.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);

        UpdateProgressText();

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (!isGameOver)
        {

            int extraCircles = 0;

            if (hitCount >= winTarget * 0.5f) 
                extraCircles = Random.Range(0, 2); 

            if (hitCount >= winTarget * 0.75f) 
                extraCircles = Random.Range(1, 3); 

            SpawnCircle();
            for (int i = 0; i < extraCircles; i++)
            {
                SpawnCircle();
            }


            spawnInterval = Mathf.Max(minInterval, spawnInterval - intervalDecreaseRate);

            float difficultyFactor = 1f - (float)hitCount / winTarget; 
            float adjustedInterval = Mathf.Max(minInterval, spawnInterval * difficultyFactor);

            yield return new WaitForSeconds(adjustedInterval);
        }
    }

    void SpawnCircle()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(-spawnArea.rect.width / 2, spawnArea.rect.width / 2),
            Random.Range(-spawnArea.rect.height / 2, spawnArea.rect.height / 2)
        );

        GameObject circle = Instantiate(circlePrefab, spawnArea);
        circle.GetComponent<RectTransform>().anchoredPosition = randomPos;
        circle.GetComponent<Button>().onClick.AddListener(() => circle.GetComponent<DanceCircle>().OnClick());
    }

    // public void Hit(GameObject circle)
    // {
    //     Destroy(circle);
    //     hitCount++;

    //     UpdateProgressText();

    //     if (hitCount >= winTarget)
    //     {
    //         WinGame();
    //     }
    // }
    public void Hit(GameObject circle)
    {
        Destroy(circle);
        hitCount++;

        if (audioSource != null && hitAudioClip != null)
            audioSource.PlayOneShot(hitAudioClip);

        UpdateProgressText();

        if (hitCount >= winTarget)
        {
            WinGame();
        }
    }

    public void Miss(GameObject circle)
    {
        Destroy(circle);
        StartCoroutine(FlashRed());

        if (audioSource != null && missAudioClip != null)
            audioSource.PlayOneShot(missAudioClip);

        if (lives > 0)
        {
            lives--;
            hearts[lives].enabled = false; 
        }

        if (lives <= 0)
        {
            GameOver();
        }
    }

    IEnumerator FlashRed()
    {
        flashScreen.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.3f);
        flashScreen.color = new Color(1, 0, 0, 0);
    }

    void UpdateProgressText()
    {
        if (progressText != null)
            progressText.text = hitCount + "/" + winTarget;
    }

    void GameOver()
    {
        isGameOver = true;
        StopAllCoroutines();

        DanceCircle[] activeCircles = FindObjectsOfType<DanceCircle>();
        foreach (DanceCircle circle in activeCircles)
        {
            Destroy(circle.gameObject);
        }

        if (audioSource != null && failAudioClip != null)
            audioSource.PlayOneShot(failAudioClip);

        if (failedScreen != null)
            failedScreen.SetActive(true);
    }

    void WinGame()
    {
        isGameOver = true;
        StopAllCoroutines();

        DanceCircle[] activeCircles = FindObjectsOfType<DanceCircle>();
        foreach (DanceCircle circle in activeCircles)
        {
            Destroy(circle.gameObject);
        }

        if (audioSource != null && winAudioClip != null)
            audioSource.PlayOneShot(winAudioClip);

        if (winScreen != null)
            winScreen.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #else
        Application.Quit();
        #endif
    }
}
