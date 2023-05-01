using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathReason; 
    public GameObject game; 

    public void SetUp(string _deathReason)
    {
        game.SetActive(false);
        gameObject.SetActive(true);
        deathReason.text = _deathReason;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(0);
    }
}
