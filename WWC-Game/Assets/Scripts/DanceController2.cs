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
    public AudioClip missAudioClip;   // geluid bij mis
    public AudioClip failAudioClip;   // geluid bij game over

    [Header("GameOver UI")]
    public GameObject failedScreen;   // panel met retry + exit

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;       
    public float minInterval = 0.8f;       
    public float intervalDecreaseRate = 0.01f; 

    private int lives;
    private bool isGameOver = false;

    void Start()
    {
        lives = startingLives;

        if (failedScreen != null)
            failedScreen.SetActive(false); // standaard uit

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (!isGameOver)
        {
            SpawnCircle();

            // interval iets sneller
            spawnInterval = Mathf.Max(minInterval, spawnInterval - intervalDecreaseRate);

            yield return new WaitForSeconds(spawnInterval);
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

    public void Hit(GameObject circle)
    {
        Destroy(circle);
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

    void GameOver()
    {
        isGameOver = true;
        StopAllCoroutines();

        // Verwijder actieve circles
        DanceCircle[] activeCircles = FindObjectsOfType<DanceCircle>();
        foreach (DanceCircle circle in activeCircles)
        {
            Destroy(circle.gameObject);
        }

        // Speel fail audio
        if (audioSource != null && failAudioClip != null)
            audioSource.PlayOneShot(failAudioClip);

        // Toon GameOver panel
        if (failedScreen != null)
            failedScreen.SetActive(true);
    }

    // Functie voor Retry knop
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Functie voor Exit knop
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("you just destroyed ur pc");
    }
}