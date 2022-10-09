using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 玩家刚体
    public Rigidbody2D playerRb;
    // 玩家速度
    public float playerSpeed;
    // 跳跃力
    public float jumpForce;
    //玩家动画
    public Animator playerAnimator;
    // 玩家碰撞体
    public Collider2D playerCollider;
    // 图层遮罩
    public LayerMask ground;
    // 玩家水平移动方向
    private float playerMoveDire;
    // 玩家速度
    private Vector2 playerVelocity;
    // 玩家朝向
    private float playerFacedDire;
    // 控制左右翻转
    private Vector3 playerScale;

    // Start is called before the first frame update
    void Start()
    {
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
}
