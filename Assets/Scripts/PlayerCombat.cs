using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    Weapons weapon;
    public Weapons sword;
    public Weapons hammer;
    public Weapons scythe;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        string pickUp = FindObjectOfType<GameController>().getPowerUp();
        if (pickUp != null)
        {
            if (pickUp == "hammer")
            {
                weapon = hammer;
                setHammer();
            }
            else if (pickUp == "scythe")
            {
                weapon = scythe;
                setScythe();
            }
        }
        else
        {
            weapon = sword;
            setSword();
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void swingSword()
    {
        animator.SetTrigger("swing");
        weapon.swingSword();
    }

    public void die()
    {
        animator.SetTrigger("die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("hit");
    }

    public void setSword()
    {
        hammer.gameObject.SetActive(false);
        scythe.gameObject.SetActive(false);
        sword.gameObject.SetActive(true);
    }

    public void setHammer()
    {
        hammer.gameObject.SetActive(true);
        scythe.gameObject.SetActive(false);
        sword.gameObject.SetActive(false);
    }

    public void setScythe()
    {
        hammer.gameObject.SetActive(false);
        scythe.gameObject.SetActive(true);
        sword.gameObject.SetActive(false);
    }
}
