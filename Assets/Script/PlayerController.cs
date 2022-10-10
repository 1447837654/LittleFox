using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb; // ��Ҹ���
    public float playerSpeed; // ����ٶ�
    public float jumpForce; // ��Ծ��

    private Animator playerAnimator; //��Ҷ���
    public Collider2D playerCollider; // �����ײ��
    public LayerMask ground; // ͼ������
    private float playerMoveDire; // ���ˮƽ�ƶ�����
    private Vector2 playerVelocity; // ����ٶ�
    private float playerFacedDire; // ��ҳ���
    private Vector3 playerScale; // �������ҷ�ת

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

    // ʰȡ��Ʒ��ײ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Debug.Log("��ײ��ΪNULL");
        }

        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
