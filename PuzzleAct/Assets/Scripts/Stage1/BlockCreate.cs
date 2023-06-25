using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreate : MonoBehaviour
{
    enum State
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
    Sprite[] sprites;

    [SerializeField]
    int currentState = 0;

    SpriteRenderer spriteRenderer;
    BoxCollider2D coll;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        spriteRenderer.sprite = sprites[currentState];
    }

    private void OnMouseDown()
    {
        Debug.Log("block");
        if (currentState == (int)State.None)
        {
            currentState = Stage1Manager.current.selectItem;
        }
    }

}
