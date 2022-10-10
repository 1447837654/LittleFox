using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb; // 玩家刚体
    public float playerSpeed; // 玩家速度
    public float jumpForce; // 跳跃力

    private Animator playerAnimator; //玩家动画
    public Collider2D playerCollider; // 玩家碰撞体
    public LayerMask ground; // 图层遮罩
    private float playerMoveDire; // 玩家水平移动方向
    private Vector2 playerVelocity; // 玩家速度
    private float playerFacedDire; // 玩家朝向
    private Vector3 playerScale; // 控制左右翻转

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        playerVelocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y);
        playerScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    // 玩家移动
    void Movement()
    {
        playerMoveDire = Input.GetAxis("Horizontal");
        playerFacedDire = Input.GetAxisRaw("Horizontal");
        // Debug.Log(playerMoveDire);

        // 玩家移动
        if (playerMoveDire != 0)
        {
            playerVelocity.x = playerMoveDire * playerSpeed;
            playerVelocity.y = playerRb.velocity.y;
            playerRb.velocity = playerVelocity;

            playerAnimator.SetFloat("Running", Mathf.Abs(playerFacedDire));
        }

        if (playerFacedDire != 0)
        {
            playerScale.x = playerFacedDire;
            transform.localScale = playerScale;
        }

        // 玩家跳跃
        if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
        {
            playerVelocity.x = playerRb.velocity.x;
            playerVelocity.y = jumpForce;
            playerRb.velocity = playerVelocity;

            playerAnimator.SetBool("Jumping", true);
        }

        if (playerRb.velocity.y < 0)
        {
            playerAnimator.SetBool("Idle", false);
            playerAnimator.SetBool("Jumping", false);
            playerAnimator.SetBool("Falling", true);
        }

        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("Falling", false);
            playerAnimator.SetBool("Idle", true);
        }
    }

    // 拾取物品碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log("碰撞体为NULL");
        }

        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
