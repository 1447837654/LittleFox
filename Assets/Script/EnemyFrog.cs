using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public Transform leftPoint; // �����ƶ���߽�
    public Transform rightPoint; // �����ƶ��ұ߽�
    public float speed; // �����ٶ�ֵ
    public float jumpForce; // ������Ծ�ٶ�

    private Rigidbody2D frogRb; // ���ܸ������
    private Collider2D frogCollider; // ������ײ��
    private Animator frogAnimator; // ���ܶ���������
    private bool faceLeft; // ����������
    private Vector2 frogVelocity; // �����ٶ�
    private float leftX; // �ƶ���߽�
    private float rightX; // �ƶ��ұ߽�
    private bool isMove; // �Ƿ��ƶ�

    // Start is called before the first frame update
    void Start()
    {
        frogRb = GetComponent<Rigidbody2D>();
        frogCollider = GetComponent<Collider2D>();
        frogAnimator = GetComponent<Animator>();
        faceLeft = true;
        frogVelocity = new Vector2(frogRb.velocity.x, frogRb.velocity.y);
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        isMove = false;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Movement();
            SwitchAnimation();
        }
    }

    void Movement()
    {
        if (faceLeft)
        {
            frogVelocity.x = -speed;
            frogVelocity.y = frogRb.velocity.y;
            frogRb.velocity = frogVelocity;

            if (transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            frogVelocity.x = speed;
            frogVelocity.y = frogRb.velocity.y;
            frogRb.velocity = frogVelocity;

            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    void SwitchAnimation()
    {
        if (frogAnimator.GetBool("Falling") && frogCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            frogAnimator.SetBool("Falling", false);
            frogAnimator.SetBool("Idle", true);

            frogVelocity.x = 0;
            frogVelocity.y = frogRb.velocity.y;
            frogRb.velocity = frogVelocity;

            isMove = false;
        }
        else if (frogCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            frogVelocity.x = frogRb.velocity.x;
            frogVelocity.y = jumpForce;
            frogRb.velocity = frogVelocity;

            frogAnimator.SetBool("Idle", false);
            frogAnimator.SetBool("Jumping", true);
        }
        else if (frogAnimator.GetBool("Jumping") && frogRb.velocity.y < 0)
        {
            frogAnimator.SetBool("Jumping", false);
            frogAnimator.SetBool("Falling", true);
        }
    }

    void setMove()
    {
        isMove = true;
    }

    void setNotMove()
    {
        isMove = false;
    }
}
