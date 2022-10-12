using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public Transform leftPoint; // 青蛙移动左边界
    public Transform rightPoint; // 青蛙移动右边界
    public float speed; // 青蛙速度值
    public float jumpForce; // 青蛙跳跃速度

    private Rigidbody2D frogRb; // 青蛙刚体组件
    private Collider2D frogCollider; // 青蛙碰撞体
    private Animator frogAnimator; // 青蛙动画控制器
    private bool faceLeft; // 青蛙面向左
    private Vector2 frogVelocity; // 青蛙速度
    private float leftX; // 移动左边界
    private float rightX; // 移动右边界
    private bool isMove; // 是否移动

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
