using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{

    public TextMeshProUGUI output = null;
    public GameObject spaceship;
    public GameObject computerScene;

    private string remainingWord = string.Empty;
    private string currentWord = "";
    private string typed = "";
    public GameController gameController;
    string currentSystem;
    
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        currentSystem = gameController.GetSystem();
        if (currentSystem == "autopilot" && gameController.getAutopilotDown())
        {
            currentWord = "Autopilot system down!";
            SetCurrentWord();
        }
        else if (currentSystem == "oxygen" && gameController.getOxygenDown())
        {
            currentWord = "Oxygen system down! Sudo reset to initial working condition; Reset initiated in 3 2 1. Oxygen system restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "engine" && gameController.getEngineDown())
        {
            currentWord = "Engine cores down! Sudo reset to initial working condition; Reset initiated in 3 2 1. Engine restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "battery" && gameController.getBatteryDown())
        {
            currentWord = "Battery cores down! Sudo reset to initial working condition; Reset initiated in 3 2 1. Oxygen system restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "shield" && gameController.getShieldDown())
        {
            currentWord = "Shield down! Sudo reset to initial working condition; Reset initiated in 3 2 1. Shield system restarted: System status OK Protection Level: 100";
            SetCurrentWord();
        }
    }

    private void SetCurrentWord()
    {
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        output.text = "<color=grey>" + typed + "</color>" + remainingWord;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            spaceship.SetActive(true);
            computerScene.SetActive(false);
        }

        CheckInput();

        if (remainingWord.Length == 0)
        {
            output.text = "COMPLETE";
            if (currentSystem == "autopilot")
            {
                gameController.setAutopilotDown(false);
            }
            else if (currentSystem == "oxygen")
            {
                gameController.setOxygenDown(false);
            }
            else if (currentSystem == "engine")
            {
                gameController.setEngineDown(false);
            }
            else if (currentSystem == "battery")
            {
                gameController.setBatteryDown(false);
            }
            else if (currentSystem == "shield")
            {
                gameController.setShieldDown(false);
            }
            spaceship.SetActive(true);
            computerScene.SetActive(false);
        }
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            if (IsWordComplete())
            {
                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter) {
        return remainingWord.IndexOf(letter) == 0;
    } 

    private void RemoveLetter()
    {
        typed += remainingWord[0];
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}
