using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    Text resultText = default;

    private void Start()
    {
        GameManager.current.ChangeActiveScene("Result");

        if (GameManager.current.hitPoint >= 1)
        {
            resultText.text = "Game Clear";
        }
        else
        {
            resultText.text = "Game Over";
        }

        GameManager.current.hitPoint = 5;
    }

    public void OnClickHomeButton()
    {
        GameManager.current.ChangeScene("Result", "Home");
    }
}