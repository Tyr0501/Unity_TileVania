using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;


    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] float climbSpeed = 5f;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider2D;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value) {
      
        Debug.Log(myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")));
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if(value.isPressed ) {
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        
        Vector2 playerVelocity = new Vector2(moveInput.x *10,myRigidbody2D.velocity.y);

        myRigidbody2D.velocity = playerVelocity;
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHorizontalSpeed);

    }
    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }
    void ClimbLadder() {
        if (!myCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; }

        Vector2 climbVelocity = new Vector2 (myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
     
        myRigidbody2D.velocity = climbVelocity;
           bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

       
        myAnimator.SetBool("isClimbing", playerHorizontalSpeed);
    }
}


