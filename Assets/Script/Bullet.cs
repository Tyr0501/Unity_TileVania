using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] int damage = 1;
    Rigidbody2D myRigidbody2d;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        myRigidbody2d = GetComponent<Rigidbody2D>();
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
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            if(enemy != null) 
                enemy.Attacked(damage);
        }
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyMovement enemy = other.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null)
                enemy.Attacked(damage);
        }

    }
}
