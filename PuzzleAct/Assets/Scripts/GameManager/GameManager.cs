using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager current;  //シングルトン用

    public int hitPoint = 5;

    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
        else if(current != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// シーンを切り替える
    /// </summary>
    /// <param name="now"></param>
    /// <param name="next"></param>
    public void ChangeScene(string now,string next)
    {        
        //現在のシーンを削除
        SceneManager.UnloadSceneAsync(now);

        //次のシーンを読み込み
        SceneManager.LoadScene(next, LoadSceneMode.Additive);
    }

    /// <summary>
    /// アクティブシーンを変える
    /// </summary>
    /// <param name="name"></param>
    public void ChangeActiveScene(string name)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        SceneManager.SetActiveScene(scene);
    }

    /// <summary>
    /// HP減少
    /// </summary>
    public void Damage()
    {
        hitPoint--;
    }
}
