using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YoranMinigame : MonoBehaviour
{

    [Header("Spaces settings")]
    public string currentSequence = "1010196124";
    public string input;
    public int guessAmount = 6;
    [SerializeField] public TextMeshProUGUI displayText;
    public TextMeshProUGUI hintText;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] lossSounds;
    public AudioClip winSound;
    public AudioClip hintSound;
    public AudioClip introAudio;

    public GameObject winScreen;
    public GameObject loseScreen;
    public Button[] buttons;

    

    private float btnClicked = 0;
    bool hasLost = false;
    bool hasWon = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnClicked = 0;
        displayText.text = "";
        audioSource = GetComponent<AudioSource>();
        
        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }

        if (introAudio != null)
        {
            audioSource.PlayOneShot(introAudio);
            StartCoroutine(EnableButtonsAfterAudio());
        }
        else
        {
            EnableButtons();
        }
    }



    // Update is called once per frame
    void Update()
    {
       if(btnClicked == guessAmount && !hasLost && !hasWon)
        {
            if(input == currentSequence)
            {
                Win();
                audioSource.PlayOneShot(winSound);
                hasWon = true;
            }
            else
            {
                Lose();
                int index = Random.Range(0, lossSounds.Length);
                audioSource.PlayOneShot(lossSounds[index]);
                hasLost = true;
            }
        }
    }

    private IEnumerator EnableButtonsAfterAudio()
    {
        // Wait for the audio to finish
        yield return new WaitForSeconds(introAudio.length);
        EnableButtons();
    }

    private void EnableButtons()
    {
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
        }
    }

    public void SpaceSelected(string selectedSpace)
    {
        switch (selectedSpace)
        {
            case "C": //Clears guess
                btnClicked = 0;
                input = "";
                displayText.text = input.ToString();
                break;

            default: //space clicked
                btnClicked++;
                input += selectedSpace;
                displayText.text = input.ToString();
                break;
        }
    }

    private void Win()
    {
        winScreen.SetActive(true);
    }

    private void Lose()
    {
        loseScreen.SetActive(true);
        
    }

    public void Restart()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        hasLost = false;
        hasWon = false;
        btnClicked = 0;
        input = "";
        displayText.text = input.ToString();
    }

    public void Hint()
    {
        audioSource.PlayOneShot(hintSound);
        StartCoroutine(ShowHint());
    }

    private IEnumerator ShowHint()
    {
        hintText.text = "10\n10\n19\n6\n12\n4";
        yield return new WaitForSeconds(1.5f);
        hintText.text = "";
    }
}
