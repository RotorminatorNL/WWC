using UnityEngine;
using TMPro;

public class Minigame : MonoBehaviour
{
    private double startTimer = 5;
    public double timer;
    public int currentAction; //0 = Friendly soldier, 1 = charging Horse, 2 = Horse in combat
    private int fails = 0;
    private int kills = 0;

    public GameObject friendlyMan;
    public GameObject chargingHorse;
    public GameObject horseInCombat;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject timerTextHolder;
    public TMP_Text timerText;
    public GameObject killsTextHolder;
    public TMP_Text killsText;
    public GameObject failsTextHolder;
    public TMP_Text failsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = startTimer; 
        NextAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            DoNothing();
        }

        if (fails >= 3)
        {
            YouLose();
        }

        
        if (kills >= 5)
        {
            YouWin();
        }

        timerText.text = timer.ToString("F1");
        
    }

    void DoNothing()
    {
        if (currentAction == 0)
        {
            timer = startTimer;
        }
        else if (currentAction == 1)
        {
            YouLose();
        }
        else
        {
            timer = startTimer;
            fails++;
        }

        UpdateUI();
        NextAction();
    }

    public void Attack()
    {
        if (currentAction == 2)
        {
            startTimer -= 0.5;
            timer = startTimer;
            kills++;
        } 
        else if (currentAction == 0)
        {
            timer = startTimer;
            fails++;
        }
        else
        {
            YouLose();
        }

        UpdateUI();
        NextAction();
    }

    public void Dodge()
    {
        if (currentAction == 1)
        {
            timer = startTimer;
        }
        else
        {
            fails++;
            timer = startTimer;
        }

        UpdateUI();
        NextAction();
    }

    private void NextAction()
    {
        currentAction = Random.Range(0,3);

        if (currentAction == 0)
        {
            friendlyMan.SetActive(true);
            horseInCombat.SetActive(false);
            chargingHorse.SetActive(false);
        }
        else if (currentAction == 1)
        {
            friendlyMan.SetActive(false);
            horseInCombat.SetActive(false);
            chargingHorse.SetActive(true);
        }
        else
        {
            friendlyMan.SetActive(false);
            horseInCombat.SetActive(true);
            chargingHorse.SetActive(false);
        }


    }

    private void YouWin()
    {
        winScreen.SetActive(true);
        timer = 100000;
        timerTextHolder.SetActive(false);
        killsTextHolder.SetActive(false);
        failsTextHolder.SetActive(false);
    }
    
    private void YouLose()
    {
        loseScreen.SetActive(true);
        timer = 100000;
        timerTextHolder.SetActive(false);
        killsTextHolder.SetActive(false);
        failsTextHolder.SetActive(false);
    }

    public void Restart()
    {
        startTimer = 5;
        loseScreen.SetActive(false);
        timer = startTimer;
 
        timerTextHolder.SetActive(true);
        killsTextHolder.SetActive(true);
        failsTextHolder.SetActive(true);
        fails = 0;
        kills = 0;

        NextAction();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(fails == 0)
        {
            failsText.text = "Fails: x x x";
        }
        else if(fails == 1)
        {
            failsText.text = "Fails: o x x";
        }
        else
        {
            failsText.text = "Fails: o o x";
        }

        if (kills == 0)
        {
            killsText.text = "Kills: x x x x x";
        }
        else if (kills == 1)
        {
            killsText.text = "Kills: o x x x x";
        }
        else if (kills == 2)
        {
            killsText.text = "Kills: o o x x x";
        }
        else if (kills == 3)
        {
            killsText.text = "Kills: o o o x x";
        }
        else
        {
            killsText.text = "Kills: o o o o x";
        }
    }
}
