using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBottom : MonoBehaviour
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
        SearchFloorBottom();
    }

    /// <summary>
    /// 梯子降り終わりの時の足場確認
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="collider"></param>
    private void SearchFloorBottom()
    {
        LayerMask ground = LayerMask.GetMask("Ground");

        float rayDistance = 0.02f;
        Vector2 centerPos = coll.bounds.center;
        Vector2 collSize = coll.bounds.size;
        float rayLeftStartX = centerPos.x - (collSize.x / 2);
        float rayRightStartX = centerPos.x + (collSize.x / 2);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(rayLeftStartX, centerPos.y - 1), Vector2.left, rayDistance, ground);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(rayRightStartX, centerPos.y - 1), Vector2.right, rayDistance, ground);

        Debug.DrawLine(new Vector2(rayLeftStartX, centerPos.y - 1), new Vector2(rayLeftStartX - rayDistance, centerPos.y - 1));
        Debug.DrawLine(new Vector2(rayRightStartX, centerPos.y - 1), new Vector2(rayRightStartX + rayDistance, centerPos.y - 1));

        if (hitLeft == true)
        {
            leftFloor = true;
        }
        else
        {
            leftFloor = false;
        }

        if (hitRight == true)
        {
            rightFloor = true;
        }
        else
        {
            rightFloor = false;
        }
    }

}