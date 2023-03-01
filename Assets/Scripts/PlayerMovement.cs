using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 18f;
    [SerializeField] float climbSpeed = 5f;
    
    [SerializeField] Vector2 deathKick = new Vector2(7f,10f);
    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D feet;
    float normalGravityScale;
    bool isAlive;

    void Start()
    {
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        feet = GetComponent<BoxCollider2D>();
        normalGravityScale = rb.gravityScale;
        
    }


    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Climb();
        Die();


    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return;}

        Instantiate(bulletPrefab, gun.position, gun.rotation);
        

        
    }

    void Die()
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && !capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Lava"))) 
        { 
            return; 
        }
        isAlive = false;
        animator.SetTrigger("dead");
        rb.velocity = deathKick;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Ground")) && !capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climb"))) {return;}
        if (value.isPressed)
        {
            //Debug.Log("jumping");
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void FlipSprite()
    {

        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * 3.2f, 3.2f);
        }
        
    }

    void Climb()
    {
        if (!feet.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            animator.SetBool("isClimbing", false);
            rb.gravityScale = normalGravityScale;
            return;
        }
        else
        {
            bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) >= Mathf.Epsilon;

            rb.gravityScale = 0;
            Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            rb.velocity = climbVelocity;
            animator.SetBool("isClimbing", playerHasVerticalSpeed);
        }
        
    }
}
