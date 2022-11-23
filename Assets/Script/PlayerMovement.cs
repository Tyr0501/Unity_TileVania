using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{



    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 7f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKich = new Vector2();
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Gun;
    Vector2 moveInput;
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;
    GameSession gameSession;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        gravityScaleAtStart = myRigidbody2D.gravityScale;
        gameSession = FindObjectOfType<GameSession>();

    }

    void Update()
    {
        Fall();
        Run();
        FlipSprite();
        if (!isAlive) { return; }
        ClimbLadder();
        Die();

    }
    void Fall()
    {
        myAnimator.SetBool("isFall", false);
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                myAnimator.SetBool("isFall", true);
            }

        }
    }
    // Lắng nghe sự kiện khi di chuyển lên xuống trái phải

    void OnMove(InputValue value)
    {
        // lăng nghe sự kiện OnMove để tả về giá trị vector x,y x
        // Trả về giá trị x khi di chuyển sang trái hoặc phải +-1.0
        // trả về giá trị y khi di chuyển lên hoặc xuống +-1.0

        moveInput = value.Get<Vector2>();

    }
    // Lắng nghe sự kiện khi nhảy
    void OnJump(InputValue value)
    {

        if (!isAlive) { return; }
        //  Nếu như khi chạm vào thì trả về !false mới cho nhảy tiếp

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        // khi Hàm OnJump của input manager khởi tạo và lắng nghe
        // Sẽ lấy value.isPressed trả về true nếu như có sự kiện false nếu như không có sự kiện
        if (value.isPressed)
        {
            // new Vector2 (chiều x "Ngang" không thay đổi, chiều y "Dọc")
            // vị trí hiện tại cộng thêm vị trí khi nhảy là 7
            myRigidbody2D.velocity += new Vector2(0f, jumpSpeed);
        }

    }
    void OnFire(InputValue value)
    {

        myAnimator.SetBool("isShoot", true);
        Instantiate(Bullet, Gun.position, transform.rotation);
        StartCoroutine(ShootCoroutine());

    }
    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(0.30f);
        myAnimator.SetBool("isShoot", false);
    }

    // Di chuyển
    void Run()
    {
        // Khi có sự kiện sảy ra sẽ trả về vector x,y
        // Khởi tạo vị trí x và y
        Vector2 playerVelocity = new Vector2(moveInput.x * 10, myRigidbody2D.velocity.y);
        // lấy giá trị khi có sự thay đổi sự kiện của bản phím là -1 và 1 nhân với 10 lực đẩy của nhân vật
        myRigidbody2D.velocity = playerVelocity;
        // Khi có sự kiện di chuyển thì biến số x sẽ thay đổi
        // Lấy giá trị x lớn nhất khi dùng hàm > Mathf.Epsilon nếu trả về giá trị lớp nhất thì trả về là true
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;

        // Hình ảnh animator running hoạt động
        if (!isAlive) { return; }

        myAnimator.SetBool("isRunning", playerHorizontalSpeed);

    }


    // Lăng nghe sự kiện khi lật vị trí hình ảnh khi di chuyển sang trái hoặc phải
    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }
    // Leo bậc thang
    void ClimbLadder()
    {
        //   Nếu như nhân vật chạm vào bậc thang thì trả về lực hấp giẫn bằng 0
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody2D.gravityScale = 0f;
            Vector2 climbVelocity = new Vector2(myRigidbody2D.velocity.x, moveInput.y * climbSpeed);
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;

            myRigidbody2D.velocity = climbVelocity;
            myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        }
        else
        {
            // còn nếu như không đụng vào bậc thàng thì trả về lực hấp giẫn bằng giá trị ban đầu
            myRigidbody2D.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
    }
    void Die()
    {

        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards")) || myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            gameSession.ProcessPlayerDeath();
            myRigidbody2D.velocity = deathKich;

            myAnimator.SetBool("isRunning", false);
            myAnimator.SetTrigger("Dying");

            isAlive = false;
        }
    }

}


