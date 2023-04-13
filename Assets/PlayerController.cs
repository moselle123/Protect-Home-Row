using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isDown", true);
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);
            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.y, 0));
                }
            }
            //left and right
            if (movementInput.x < 0)
            {
                animator.SetBool("isSide", true);
                animator.SetBool("isDown", false);
                animator.SetBool("isUp", false);
                spriteRenderer.flipX = false;
            }
            else if (movementInput.x > 0)
            {
                animator.SetBool("isSide", true);
                spriteRenderer.flipX = true;
                animator.SetBool("isDown", false);
                animator.SetBool("isUp", false);
            }
            else if (movementInput.y < 0)
            {
                animator.SetBool("isDown", true);
                animator.SetBool("isSide", false);
                animator.SetBool("isUp", false);
            }
            else if (movementInput.y > 0)
            {
                animator.SetBool("isUp", true);
                animator.SetBool("isDown", false);
                animator.SetBool("isSide", false);
            }
            animator.SetBool("isMoving", success);

        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {//check for potential colisions
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(movementInput, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
