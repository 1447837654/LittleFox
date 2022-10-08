using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��Ҹ���
    public Rigidbody2D playerRb;
    // ����ٶ�
    public float playerSpeed;
    // ���ˮƽ�ƶ�����
    private float playerMoveDire;
    // ����ٶ�
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

    // ����ƶ�
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
