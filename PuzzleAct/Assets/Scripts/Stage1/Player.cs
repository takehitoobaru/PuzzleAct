using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    float dir = 1;

    float angle;
    float jumpSpeed = 9f;
    bool isStop = false;
    bool isGround = false;
    Rigidbody2D rb;
    CapsuleCollider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        StartCoroutine(Run());
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    IEnumerator Run()
    {
        while (true)
        {
            if (isStop)
            {
                yield return new WaitForSeconds(2);
                isStop = false;
            }

            if (isGround)
            {
                rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            }
            yield return null;
        }
    }

    void Jump()
    {
        angle = Mathf.PI / 4;
        Vector2 jumpDir = new Vector2(Mathf.Cos(angle) * dir, Mathf.Sin(angle));
        rb.AddForce(jumpDir * jumpSpeed, ForceMode2D.Impulse);
    }

    void Flip()
    {
        dir *= -1;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void GroundCheck()
    {
        LayerMask ground = LayerMask.GetMask("Ground");

        float rayDistance = 0.02f;
        Vector2 centerPos = coll.bounds.center;
        Vector2 collSize = coll.bounds.size;
        float rayStartPosY = centerPos.y - collSize.y / 2;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(centerPos.x, rayStartPosY), Vector2.down, rayDistance, ground);

        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Flip();
        }
        else if(collision.tag == "Stop")
        {
            isStop = true;
        }
        else if(collision.tag == "LeftJump")
        {
            if(dir != -1)
            {
                Flip();
            }
            Jump();
        }
        else if(collision.tag == "RightJump")
        {
            if (dir != 1)
            {
                Flip();
            }
            Jump();
        }
    }
}
