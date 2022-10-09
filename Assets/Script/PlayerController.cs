using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ��Ҹ���
    public Rigidbody2D playerRb;
    // ����ٶ�
    public float playerSpeed;
    // ��Ծ��
    public float jumpForce;
    //��Ҷ���
    public Animator playerAnimator;
    // �����ײ��
    public Collider2D playerCollider;
    // ͼ������
    public LayerMask ground;
    // ���ˮƽ�ƶ�����
    private float playerMoveDire;
    // ����ٶ�
    private Vector2 playerVelocity;
    // ��ҳ���
    private float playerFacedDire;
    // �������ҷ�ת
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

    // ����ƶ�
    void Movement()
    {
        playerMoveDire = Input.GetAxis("Horizontal");
        playerFacedDire = Input.GetAxisRaw("Horizontal");
        // Debug.Log(playerMoveDire);

        // ����ƶ�
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

        // �����Ծ
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
