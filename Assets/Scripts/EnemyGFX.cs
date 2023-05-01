using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    EnemyFire enemyFire;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyFire = GetComponent<EnemyFire>();
        animator.SetBool("isMoving", true);
    }
    public void setMoving()
    {
        animator.SetBool("isMoving", true);
    }

    public void setStopped()
    {
        animator.SetBool("isMoving", false);
    }

    public void setLeft()
    {
        spriteRenderer.flipX = true;
    }

    public void setRight()
    {
        spriteRenderer.flipX = false;
    }

    public void attack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void stopAttack()
    {
        animator.SetBool("isAttacking", false);
    }

    public void shoot()
    {
        animator.SetTrigger("shoot");
        enemyFire.Shoot();
    }
}
