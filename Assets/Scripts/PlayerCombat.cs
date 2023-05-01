using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    public Weapons weapon;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void swingSword()
    {
        animator.SetTrigger("swing");
        weapon.swingSword();
    }
}
