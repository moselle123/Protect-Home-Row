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
        setWindow();
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
                output.text = "";

                spaceship.SetActive(true);
                computerScene.SetActive(false);
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
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

    void setWindow()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        currentSystem = gameController.GetSystem();
        output.text = "System status: OK";
        typed = "";
        if (currentSystem == "autopilot" && gameController.getAutopilotDown())
        {
            currentWord = "Autopilot system down!";
            SetCurrentWord();
        }
        else if (currentSystem == "oxygen" && gameController.getOxygenDown())
        {
            currentWord = "Oxygen system down! Oxygen system restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "engine" && gameController.getEngineDown())
        {
            currentWord = "Engine cores down! Engine restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "battery" && gameController.getBatteryDown())
        {
            currentWord = "Battery cores down! Battery system restarted: System status OK!";
            SetCurrentWord();
        }
        else if (currentSystem == "shield" && gameController.getShieldDown())
        {
            currentWord = "Shield down! Shield system restarted: System status OK Protection Level: 100";
            SetCurrentWord();
        }
    }

    private void OnEnable()
    {
        setWindow();
    }
}