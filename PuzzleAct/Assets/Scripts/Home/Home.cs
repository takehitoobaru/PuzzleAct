using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private void Start()
    {
        GameManager.current.ChangeActiveScene("Home");
    }

    /// <summary>
    /// スタートボタン押下時
    /// </summary>
    public void OnClickStartButton()
    {
        GameManager.current.ChangeScene("Home", "Stage1");
    }
}