using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
{
    public bool isInRange;
    public bool enemyInRange;
    public KeyCode interactKey;
    public GameObject system;
    public GameObject computerScene;
    public GameObject spaceship;
    public GameController gameController;
    public string name;
    public HealthBar healthBar;
    int health = 10;
    private Coroutine healthDecreaseCoroutine;

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                gameController.setSystem(name);
                spaceship.SetActive(false);
                computerScene.SetActive(true);
            }
        }

        if(enemyInRange && healthDecreaseCoroutine == null)
        {
            healthDecreaseCoroutine = StartCoroutine(DecreaseHealthCoroutine());
        }

        if (health <= 0)
        {
            if (name == "autopilot")
            {
                gameController.setAutopilotDown(true);
            }
            else if (name == "engine")
            {
                gameController.setEngineDown(true);
            }
            else if (name == "battery")
            {
                gameController.setBatteryDown(true);
            }
            else if (name == "oxygen")
            {
                gameController.setOxygenDown(true);
            }
            else
            {
                gameController.setShieldDown(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
            healthBar.setVisible(true);
            if (healthDecreaseCoroutine == null)
            {
                healthDecreaseCoroutine = StartCoroutine(DecreaseHealthCoroutine());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
            healthBar.setVisible(false);
            if (healthDecreaseCoroutine != null)
            {
                StopCoroutine(healthDecreaseCoroutine);
                healthDecreaseCoroutine = null;
            }
        }
    }

    IEnumerator DecreaseHealthCoroutine()
    {
        while (health > 0)
        {
            healthBar.SetHealth(health);
            health--;
            yield return new WaitForSeconds(2f);
        }
    }
}
