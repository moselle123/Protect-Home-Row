using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectPickUp : MonoBehaviour
{
    public string weaponName;
    bool collect = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collect = true;
        }
    }

    private void Update()
    {
        if (collect && Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<GameController>().setPowerUp(weaponName);
            Destroy(this.gameObject);
        }
    }
}
