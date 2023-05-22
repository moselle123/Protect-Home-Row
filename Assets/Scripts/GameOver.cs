using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI deathReason; 
    public GameObject game; 
    public bool isLevelComplete;
    public GameObject creditsScene;

    public void SetUp(string _deathReason)
    {
        game.SetActive(false);
        gameObject.SetActive(true);
        deathReason.text = _deathReason;
    }

    public void PlayAgain()
    {
        if (isLevelComplete)
        {
            activateCredits();
        }
        SceneManager.LoadScene(1);
    }

    public void QuitToMain()
    {
        if (isLevelComplete)
        {
            activateCredits();
        }
        SceneManager.LoadScene(0);
    }

    public void activateCredits()
    {
        StartCoroutine(ActivateAndDeactivate());
    }

    private IEnumerator ActivateAndDeactivate()
    {
        creditsScene.SetActive(true);

        yield return new WaitForSeconds(20f);

        creditsScene.SetActive(false);
    }
}
