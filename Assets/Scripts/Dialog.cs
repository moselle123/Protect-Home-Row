using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI output;
    public GameObject dialogObjects;

    int counter = 0;
    bool intro = true;
    bool shield = false;
    string[] introText = {"We need to fix the shield quickly or enemies will start flooding in.", "I'd say we have about 2 minutes to get back to safety we have to hold off the attack until then.", "First things first, get to the shield control system and type out the commands to fix it."};
    string[] shieldText = {"Now you need to defeat the enemies.", "To fight enemies walk up to them and press F.", "when you defeat them they will drop powerful weapons. ", "To collect your weapon press E, you'll get a time boost for each weapon you collect."};

    private void Start()
    {
        FindObjectOfType<GameController>().PauseGame();
        dialogObjects.SetActive(true);
        output.text = "Oh no! Theres been a breach in the protective shield, we're under attack!";
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && intro)
        {
            output.text = introText[counter];
            if (counter == introText.Length - 1)
            {
                dialogObjects.SetActive(false);
                FindObjectOfType<GameController>().ResumeGame();
                intro = false;
                counter = 0;
            }

            counter++;
        }

        if (shield)
        {
            FindObjectOfType<GameController>().PauseGame();
            dialogObjects.SetActive(true);
            counter = 0;
            output.text = "Well done, you fixed the shield!";
            if (Input.GetKeyDown("space") && shield)
            {
                output.text = shieldText[counter];
                if (counter == introText.Length - 1)
                {
                    dialogObjects.SetActive(false);
                    FindObjectOfType<GameController>().ResumeGame();
                    shield = false;
                    counter = 0;
                }

                counter++;
            }
        }
    }
    public void setShield()
    {
        shield = true;
    }
}
