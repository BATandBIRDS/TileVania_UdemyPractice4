using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (moveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipHorizontally();
    }

    void FlipHorizontally()
    {
        transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * 3.2f, 3.2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            moveSpeed = 0;
            if (animator != null)
            {
                animator.SetTrigger("explode");
            }
            
            Invoke("Explode", 1.2f);
        }
    }

    void Explode()
    {
        Destroy(gameObject);
    }
}
