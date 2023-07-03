using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlock : MonoBehaviour
{
    [SerializeField]
    float maxHeight = 9f;  //最大

    [SerializeField]
    float growSpeed = 1f;  //成長速度

    BoxCollider2D coll = default;

    public bool leftFloor = false;
    public  bool rightFloor = false;
    public bool isGrow = false; //成長するかどうか
    public bool isGrowEnd = false; //成長が終わったかどうか

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        maxHeight = transform.position.y + maxHeight;
    }

    void Update()
    {
        if (isGrow)
        {
            //最大値に達していないなら
            if (transform.position.y < maxHeight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + growSpeed);
            }
            //最大値に達したら
            else if (transform.position.y >= maxHeight)
            {
                transform.position = new Vector2(transform.position.x, maxHeight);
                isGrow = false;
                isGrowEnd = true;
            }
        }
    }

    private void FixedUpdate()
    {
        SearchFloor();
    }

    /// <summary>
    /// 登り切ったときの足場判定
    /// </summary>
    private void SearchFloor()
    {
        LayerMask ground = LayerMask.GetMask("Ground");

        float rayDistance = 0.02f;
        Vector2 centerPos = coll.bounds.center;
        Vector2 collSize = coll.bounds.size;
        float rayLeftStartX = centerPos.x - (collSize.x / 2);
        float rayRightStartX = centerPos.x + (collSize.x / 2);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(rayLeftStartX, centerPos.y), Vector2.left, rayDistance, ground);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(rayRightStartX, centerPos.y), Vector2.right, rayDistance, ground);

        Debug.DrawLine(new Vector2(rayLeftStartX, centerPos.y), new Vector2(rayLeftStartX - rayDistance, centerPos.y));
        Debug.DrawLine(new Vector2(rayRightStartX, centerPos.y), new Vector2(rayRightStartX + rayDistance, centerPos.y));

        if (hitLeft)
        {
            leftFloor = true;
        }
        else
        {
            leftFloor = false;
        }

        if (hitRight)
        {
            rightFloor = true;
        }
        else
        {
            rightFloor = false;
        }
    }


}