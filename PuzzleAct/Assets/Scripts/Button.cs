using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.current.ChangeScene("Home", "Stage1");
    }
}
