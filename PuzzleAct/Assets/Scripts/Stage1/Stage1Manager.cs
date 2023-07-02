using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1Manager : MonoBehaviour
{
    public static Stage1Manager current;

    [SerializeField]
    Text timeText;    //制限時間のテキスト

    [SerializeField]
    Text hitPointText;

    public int[] itemPossession = new int[8];   //各アイテム所持数
    public int selectItem = 1; //選択アイテム
    public float limitTime = 300;    //制限時間

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else if(current != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameManager.current.ChangeActiveScene("Stage1");
        timeText.text = $"Time:{Mathf.Floor(limitTime).ToString()}";
        hitPointText.text = $"HP:{GameManager.current.hitPoint.ToString()}";
    }

    void Update()
    {
        limitTime -= Time.deltaTime;
        timeText.text = $"Time:{Mathf.Floor(limitTime).ToString()}";
        hitPointText.text = $"HP:{GameManager.current.hitPoint.ToString()}";

        if (limitTime <= 0)
        {
            GameManager.current.Damage();
            GameManager.current.ChangeScene("Stage1", "Stage1");

            if(GameManager.current.hitPoint <= 0)
            {
                GameManager.current.ChangeScene("Stage1", "result");
            }
        }
    }

    /// <summary>
    /// リセットボタン押下時
    /// </summary>
    public void OnClickResetButton()
    {
        GameManager.current.Damage();

        if (GameManager.current.hitPoint > 0)
        {
            GameManager.current.ChangeScene("Stage1", "Stage1");
        }
        else if(GameManager.current.hitPoint <= 0)
        {
            GameManager.current.ChangeScene("Stage1", "result");
        }
    }
}
