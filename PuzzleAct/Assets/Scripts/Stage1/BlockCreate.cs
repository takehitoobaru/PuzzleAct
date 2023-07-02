using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreate : MonoBehaviour
{
    private enum State
    {
        Timer,
        Stop,
        LeftJump,
        RightJump,
        Ladder,
        Tree2,
        Tree3,
        Rock,
        None
    }

    [SerializeField]
    private GameObject[] blockPrefab = default;

    private int currentState = 8;
    private bool isTouch = false;

    private void OnMouseDown()
    {
        if(currentState != (int)State.None && isTouch == false)
        {
            currentState = (int)State.None;
        }

        if (currentState == (int)State.None)
        {
            if (isTouch == true) return;

            currentState = Stage1Manager.current.selectItem;
            if (Stage1Manager.current.itemPossession[currentState] < 1) return;
            Instantiate(blockPrefab[currentState], transform.position, Quaternion.identity);
            Stage1Manager.current.itemPossession[currentState] -= 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isTouch = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }
}
