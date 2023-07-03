using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    private float dir = 1; 
    private CircleCollider2D coll = default;

    private void Start()
    {
        coll = GetComponent<CircleCollider2D>();

        StartCoroutine(Run());
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    /// <summary>
    ///　enemyの移動
    /// </summary>
    /// <returns></returns>
    private IEnumerator Run()
    {
        while (true)
        {
            transform.Translate(Vector2.right * dir * Time.deltaTime * speed);
            yield return null;
        }
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
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(rayRightStartX, rayStartY), Vector2.down, rayDistance, ground);

        Debug.DrawLine(new Vector2(rayLeftStartX, rayStartY), new Vector2(rayLeftStartX, rayStartY - rayDistance));
        Debug.DrawLine(new Vector2(rayRightStartX, rayStartY), new Vector2(rayRightStartX, rayStartY - rayDistance));

        if(dir == -1 && hitLeft == false)
        {
            Flip();
        }
        else if(dir == 1 && hitRight == false)
        {
            Flip();
        }
    }

    /// <summary>
    /// 方向転換
    /// </summary>
    private void Flip()
    {
        dir *= -1;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Flip();
        }
    }
}