using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private int _lifeEnemy;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private AudioSource audioHitE;

    Rigidbody2D myRigidbody2D;
    [SerializeField] BoxCollider2D myBoxBottomCollider2D;
    [SerializeField] BoxCollider2D myBoxMidCollider2D;
    Color _colorNormal;
    void Start()
    {
        _colorNormal = spriteRenderer.color;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!myBoxBottomCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            FlipEnemyFacing();
        }

        //if (myBoxMidCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //{
        //    moveSpeed = -moveSpeed;
        //    FlipEnemyFacing();
        //}


        myRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Debug.Log("Tag");
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
       
    }
    public void Attacked(int damage)
    {
        if(_lifeEnemy > 0)
        {
            _lifeEnemy -= damage;
            StartCoroutine(ChangeColorBeginAttacked());
            audioHitE.Play();
            if (_lifeEnemy <= 0)
            {
                Die();
            }
        }
    } 
    IEnumerator ChangeColorBeginAttacked()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = _colorNormal;

    }
    private void Die()
    {
        Destroy(gameObject);
    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody2D.velocity.x)), 1f);
    }
}
