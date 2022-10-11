using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb; // 玩家刚体
    public float playerSpeed; // 玩家速度
    public float jumpForce; // 跳跃力
    public TextMeshProUGUI cherryText; // 玩家获得的樱桃数量文本
    public TextMeshProUGUI gemText; // 玩家获得的钻石数量文本

    private Animator playerAnimator; //玩家动画
    public Collider2D playerCollider; // 玩家碰撞体
    public LayerMask ground; // 图层遮罩
    private float playerMoveDire; // 玩家水平移动方向
    private Vector2 playerVelocity; // 玩家速度
    private float playerFacedDire; // 玩家朝向
    private Vector3 playerScale; // 控制左右翻转
    private int cherryNum; // 玩家获取的樱桃数量
    private int gemNum; // 玩家获取的钻石数量

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        playerVelocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y);
        playerScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        cherryNum = 0;
        cherryText.text = "Cherry:" + cherryNum;
        gemNum = 0;
        gemText.text = "Gem:" + gemNum;
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
        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
            && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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

        // 玩家匍匐
        if (Input.GetAxisRaw("Vertical") == -1f && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("Crouching", true);
            playerAnimator.SetBool("Idle", false);
        }
        else if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerAnimator.SetBool("Falling", false);
            playerAnimator.SetBool("Crouching", false);
            playerAnimator.SetBool("Idle", true);
        }
    }

    // 拾取物品碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            Debug.Log("碰撞体为NULL");
        }

        if (collision.CompareTag("Item"))
        {
            if (collision.gameObject.name.Equals("Cherry"))
            {
                cherryNum++;
                cherryText.text = "Cherry:" + cherryNum;
            }
            else if (collision.gameObject.name.Equals("Gem"))
            {
                gemNum++;
                gemText.text = "Gem:" + gemNum;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy") && playerAnimator.GetBool("Falling"))
        {
            playerVelocity.x = playerRb.velocity.x;
            playerVelocity.y = jumpForce;
            playerRb.velocity = playerVelocity;

            playerAnimator.SetBool("Falling", false);
            playerAnimator.SetBool("Jumping", true);

            Destroy(collision.gameObject);
        }
    }
}
