using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CombatTyper : MonoBehaviour
{
    public PlayerCombat player;

    public TextMeshProUGUI output = null;
    public TextMeshProUGUI enemyDefeated;

    public TextMeshProUGUI timerOutput;
    bool go = false;
    float currentTime;

    private string remainingWord = string.Empty;
    private string currentWord = "";
    private string typed = "";
    private List<string> words = new List<string>();
    string nextWord = "";
    int currentWordsIndex = 0;
    int nextWordLength;
    public GameController gameController;

    void Start()
    {
        currentTime = 30;
        go = true;
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
        if (go)
        {
            currentTime -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerOutput.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
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
                if (currentWordsIndex + 1 == words.Count)
                {
                    go = false;
                    enemyDefeated.gameObject.SetActive(true);
                    output.gameObject.SetActive(false);

                }
                else
                {
                    currentWordsIndex++;
                    nextWord = words[currentWordsIndex];
                    nextWordLength = nextWord.Length;
                    player.swingSword();
                }
         
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
        nextWordLength--;
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return nextWordLength == 0;
    }

    void setWindow()
    {
        words.Add("I");
        words.Add(" will");
        words.Add(" defeat");
        words.Add(" you");
        words.Add(".");
        foreach (string word in words)
        {
            currentWord += word;
        }
        nextWord = words[currentWordsIndex];
        nextWordLength = nextWord.Length;
        typed = "";
        SetCurrentWord();
    }
}
