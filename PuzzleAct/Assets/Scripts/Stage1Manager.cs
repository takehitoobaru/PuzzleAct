using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Manager : MonoBehaviour
{
    void Start()
    {
        GameManager.current.ChangeActiveScene("Stage1");
    }

    void Update()
    {
        
    }
}
