using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Interactables : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public GameObject system;
    public GameObject computerScene;
    public GameObject spaceship;
    public GameController gameController;
    public string name;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
