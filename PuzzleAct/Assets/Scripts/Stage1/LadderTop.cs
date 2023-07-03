using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : MonoBehaviour
{
    public bool LeftFloor => leftFloor;
    public bool RightFloor => rightFloor;

    private bool leftFloor = false;
    private bool rightFloor = false;
    private BoxCollider2D coll = default;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        SearchFloorTop();
    }

    /// <summary>
    /// 梯子昇り終わりの時の足場確認
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="collider"></param>
    private void SearchFloorTop()
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