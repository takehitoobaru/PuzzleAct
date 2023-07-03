using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
