using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 玩家刚体
    public Rigidbody2D playerRb;
    // 玩家速度
    public float playerSpeed;
    // 玩家水平移动方向
    private float playerMoveDire;
    // 玩家速度
    private Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerVelocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // 玩家移动
    void Movement()
    {
        playerMoveDire = Input.GetAxis("Horizontal");

        if (playerMoveDire != 0)
        {
            playerVelocity.x = playerMoveDire * playerSpeed;
            playerVelocity.y = playerRb.velocity.y;
            playerRb.velocity = playerVelocity;
            // playerRb.velocity = new Vector2 (playerMoveDire * playerSpeed, playerVelocity.y);
        }
    }
}
