using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DanceController : MonoBehaviour
{
    public GameObject circlePrefab;
    public RectTransform spawnArea;
    public Image flashScreen;

    public Image[] hearts;           
    public AudioSource audioSource;  

    public float spawnInterval = 2f;       
    public float minInterval = 0.8f;       
    public float intervalDecreaseRate = 0.01f; 

    private int lives = 3;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (lives > 0)
        {
            SpawnCircle();

            spawnInterval = Mathf.Max(minInterval, spawnInterval - intervalDecreaseRate);

            yield return new WaitForSeconds(spawnInterval);
        }

        GameOver();
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

        if (audioSource != null) audioSource.Play();

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
        Debug.Log("Game Over!");
        StopAllCoroutines(); 

        /// hier straks de UI toevoegen van You are ded, retry or exit game.
    }

    /// nog iets verzinnen voor als ze het hebben gehaald.
}
