using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float bulletSpeed = 10f;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        
        rb.velocity = new Vector2(xSpeed / 3.2f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //other.GetComponent<Animator>().SetTrigger("explode");
            
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
