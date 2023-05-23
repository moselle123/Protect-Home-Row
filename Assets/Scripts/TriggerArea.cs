using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerArea : MonoBehaviour
{
    private bool isPlayerInside = false;
    GameObject game;
    GameObject thisEnemy;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("game");
        thisEnemy = this.gameObject;
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
            FindObjectOfType<GameController>().setCombat(thisEnemy);
        }
    }
}
