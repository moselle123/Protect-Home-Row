using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("entered");
            if (Input.GetKeyDown(KeyCode.F))
            {
                FindObjectOfType<GameController>().PauseGame();
                SceneManager.LoadScene("Combat");
            }
        }
    }
}