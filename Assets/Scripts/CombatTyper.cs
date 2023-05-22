using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class CombatTyper : MonoBehaviour
{
    public PlayerCombat player;
    public EnemyGFX enemy;

    public HealthBar enemyHealth;
    public HealthBar playerHealth;

    public TextMeshProUGUI output = null;
    public TextMeshProUGUI enemyDefeated;

    public TextMeshProUGUI timerOutput;
    bool go = false;
    float currentTime;
    bool setUp = false;

    private string remainingWord = string.Empty;
    private string currentWord = "";
    private string typed = "";
    private List<string> words = new List<string>();
    string nextWord = "";
    int currentWordsIndex = 0;
    int nextWordLength;
    public GameController gameController;

    public GameObject game;
    public GameObject combatScene;

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
            if (!setUp)
            {
                setWindow();
            } 
            currentTime -= Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            timerOutput.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        if (currentTime <= 0)
        {
            FindObjectOfType<GameController>().GameOver("You ran out of time to defeat your enemy!");
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
                    StartCoroutine(BackToLevel());
                    enemyDefeated.gameObject.SetActive(true);
                    enemy.die();
                    output.gameObject.SetActive(false);
                }
                else
                {
                    currentWordsIndex++;
                    nextWord = words[currentWordsIndex];
                    nextWordLength = nextWord.Length;
                    player.swingSword();
                    int enemyHealthNow = (int) enemyHealth.getHealth() - 1;
                    enemyHealth.SetHealth(enemyHealthNow);
                }
         
            }
        }
        else
        {
            enemy.shoot();
            int playerHealthNow = (int)playerHealth.getHealth() - 1;
            playerHealth.SetHealth(playerHealthNow);
            if (playerHealthNow == 0)
            {
                
                player.die();
                StartCoroutine(Die());
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
        enemyHealth.SetMaxHealth(words.Count -  1);
        playerHealth.SetMaxHealth(5);
        enemy.setStopped();
        currentTime = 15;
        go = true;
        setUp = true;
    }

    IEnumerator BackToLevel()
    {
        yield return new WaitForSeconds(2.5f);
        setUp = false;
        game.SetActive(true);
        combatScene.SetActive(false);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        setUp = false;
        FindObjectOfType<GameController>().GameOver("You were killed by your enemy!");
    }
}
