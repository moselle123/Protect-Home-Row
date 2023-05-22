using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerArea : MonoBehaviour
{
    private bool isPlayerInside = false;
    GameObject game;
    GameObject combatScene;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("game");
        combatScene = GameObject.FindGameObjectWithTag("combatScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            game.SetActive(false);
            combatScene.SetActive(true);
        }
    }
}
