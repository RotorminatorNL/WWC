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
    [SerializeField] public TextMeshProUGUI resultText;

    private float btnClicked = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnClicked = 0;
        resultText.text = "";
        displayText.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
       if(btnClicked == guessAmount)
        {
            if(input == currentSequence)
            {
                Debug.Log("correct Sequence");
                input = "";
                btnClicked = 0;
                resultText.text = "YOU WON!";
            }
            else
            {
                Debug.Log("FAILED");
                input = "";
                displayText.text = input.ToString();
                resultText.text = "YOU FAILED!";
                btnClicked = 0;
            }
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
}
