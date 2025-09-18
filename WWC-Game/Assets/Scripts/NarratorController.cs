using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NarratorController : MonoBehaviour
{
    public CanvasGroup narratorCanvas;
    public Canvas mainGameCanvas;        // hoofdgame canvas
    public Text narratorText;
    public Image playerImage;
    public Image williamImage;
    public AudioSource narratorAudio;

    public float fadeSpeed = 1f;
    public float textDisplayTime = 3f; // tijd dat tekst volledig zichtbaar blijft

    void Start()
    {
        // Zet het hoofdcanvas standaard uit zodat narrator zichtbaar is
        if (mainGameCanvas != null)
            mainGameCanvas.gameObject.SetActive(false);

        StartCoroutine(RunNarrator());
    }

    IEnumerator RunNarrator()
    {
        // Start volledig transparant
        narratorCanvas.alpha = 0f;

        // Fade-in
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            narratorCanvas.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        // Speel audio
        if (narratorAudio != null && narratorAudio.clip != null)
        {
            narratorAudio.Play();
            yield return new WaitForSeconds(narratorAudio.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(textDisplayTime);
        }

        // Fade-out
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            narratorCanvas.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }

        // Zet narrator canvas uit
        narratorCanvas.gameObject.SetActive(false);

        // Zet hoofdgame canvas aan
        if (mainGameCanvas != null)
            mainGameCanvas.gameObject.SetActive(true);

        // Start het spel
        DanceController2 danceController = FindObjectOfType<DanceController2>();
        if (danceController != null)
        {
            danceController.StartGame();
        }
    }
}
