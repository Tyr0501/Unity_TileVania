using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    Rigidbody2D myRigidbody2d;
    CapsuleCollider2D myCapsuleCollider2D;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        transform.localScale = new Vector2(player.transform.localScale.x, 1f);
    }



    void Update()
    {
        myRigidbody2d.velocity = new Vector2(xSpeed, 0f);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

    }
}
