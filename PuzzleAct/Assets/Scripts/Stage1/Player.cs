using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    float dir = 1;

    [SerializeField]
    float jumpPower = 15f;

    bool isStop = false;
    bool isGround = false;
    bool isClimb = false;
    bool isDescend = false;
    bool isTreeClimb = false;
    Rigidbody2D rb;
    CapsuleCollider2D coll;
    Transform treeTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        StartCoroutine(Run());
    }

    private void FixedUpdate()
    {
        GroundCheck();

        //梯子では重力を無視する
        if (isClimb || isDescend)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        //豆の木に追従
        if (isTreeClimb)
        {
            transform.position = treeTransform.position;
        }
    }

    /// <summary>
    ///　プレイヤーの移動
    /// </summary>
    /// <returns></returns>
    private IEnumerator Run()
    {
        while (true)
        {
            //２秒間ストップ
            if (isStop)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                yield return new WaitForSeconds(2);
                isStop = false;
            }

            //接地中かつ梯子昇降中でないなら
            if (isGround && !isClimb && !isDescend)
            {
                transform.Translate((Vector2.right * dir) * Time.deltaTime * speed);
            }
            yield return null;
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector2(dir, 1) * jumpPower;
    }

    /// <summary>
    /// 梯子の昇り
    /// </summary>
    /// <param name="pos"></param>
    private void Climb()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void Descend()
    {
        rb.velocity = Vector2.down * speed;
    }

    /// <summary>
    /// 方向転換
    /// </summary>
    private void Flip()
    {
        dir *= -1;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    /// <summary>
    /// 接地判定
    /// </summary>
    private void GroundCheck()
    {
        LayerMask ground = LayerMask.GetMask("Ground");

        float rayDistance = 0.02f;
        Vector2 centerPos = coll.bounds.center;
        Vector2 collSize = coll.bounds.size;
        float rayLeftStartX = centerPos.x - (collSize.x / 2);
        float rayRightStartX = centerPos.x + (collSize.x / 2);
        float rayStartY = centerPos.y - (collSize.y / 2);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(rayLeftStartX, rayStartY), Vector2.down, rayDistance, ground);
        RaycastHit2D hitCenter = Physics2D.Raycast(new Vector2(centerPos.x, rayStartY), Vector2.down, rayDistance, ground);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(rayRightStartX, rayStartY), Vector2.down, rayDistance, ground);

        Debug.DrawLine(new Vector2(rayLeftStartX, rayStartY), new Vector2(rayLeftStartX, rayStartY - rayDistance));
        Debug.DrawLine(new Vector2(centerPos.x, rayStartY), new Vector2(centerPos.x, rayStartY - rayDistance));
        Debug.DrawLine(new Vector2(rayRightStartX, rayStartY), new Vector2(rayRightStartX, rayStartY - rayDistance));

        if (hitLeft || hitCenter || hitRight)
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
        if (collision.tag == "Wall")
        {
            Flip();
        }
        else if (collision.tag == "Stop")
        {
            isStop = true;
        }
        else if (collision.tag == "LeftJump")
        {
            if (dir != -1)
            {
                Flip();
            }
            Jump();
        }
        else if (collision.tag == "RightJump")
        {
            if (dir != 1)
            {
                Flip();
            }
            Jump();
        }
        else if (collision.tag == "LadderBottom")
        {
            LadderBottom bottom = collision.GetComponent<LadderBottom>();

            if (isDescend == false)
            {
                isClimb = true;
                transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y);
                Climb();
            }
            else
            {
                if (dir == -1)
                {
                    if (bottom.LeftFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y);
                    }
                    else if (bottom.RightFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y);
                    }
                }
                else
                {
                    if (bottom.RightFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y);
                    }
                    else if (bottom.LeftFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y);
                    }
                }
            }
        }
        else if (collision.tag == "LadderTop")
        {
            LadderTop top = collision.GetComponent<LadderTop>();

            if (isClimb == false)
            {
                isDescend = true;
                transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y);
                Descend();
            }
            else
            {
                if (dir == -1)
                {
                    if (top.LeftFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y + 1);
                    }
                    else if (top.RightFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y + 1);
                    }
                }
                else
                {
                    if (top.RightFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y + 1);
                    }
                    else if (top.LeftFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y + 1);
                    }
                }
            }
        }
        else if (collision.tag == "TreeTop")
        {
            TreeBlock tree = collision.GetComponent<TreeBlock>();
            treeTransform = collision.transform;
            //成長中でないかつ成長が終わってない
            if (tree.isGrow == false && tree.isGrowEnd == false)
            {
                transform.position = new Vector2(collision.transform.position.x, transform.position.y);
                tree.isGrow = true;
                isTreeClimb = true;
            }
        }
        else if (collision.tag == "Tree")
        {
            TreeBlock tree = collision.GetComponent<TreeBlock>();
            //成長中でないかつ成長が終わってない
            if (tree.isGrow == false && tree.isGrowEnd == false)
            {
                tree.isGrow = true;
            }
        }
        else if(collision.tag == "Goal")
        {
            GameManager.current.ChangeScene("Stage1", "Result");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "TreeTop")
        {
            TreeBlock tree = collision.GetComponent<TreeBlock>();
            //登っているかつ成長が終わった
            if (isTreeClimb && tree.isGrowEnd)
            {
                isTreeClimb = false;
                if (dir == -1)
                {
                    if (tree.leftFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y + 1);
                    }
                    else if (tree.rightFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y + 1);
                    }
                }
                else
                {
                    if (tree.rightFloor == true)
                    {
                        transform.position = new Vector2(collision.transform.position.x + 1.1f, collision.transform.position.y + 1);
                    }
                    else if (tree.leftFloor == true)
                    {
                        Flip();
                        transform.position = new Vector2(collision.transform.position.x - 1.1f, collision.transform.position.y + 1);
                    }
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "LadderBottom")
        {
            isDescend = false;
        }
        else if(collision.tag == "LadderTop")
        {
            isClimb = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            GameManager.current.Damage();
            GameManager.current.ChangeScene("Stage1", "Stage1");
        }
    }
}
