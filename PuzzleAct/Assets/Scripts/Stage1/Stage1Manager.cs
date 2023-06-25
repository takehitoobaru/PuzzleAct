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
        timeText.text = Mathf.Floor(limitTime).ToString();
    }

    void Update()
    {
        limitTime -= Time.deltaTime;
        timeText.text = Mathf.Floor(limitTime).ToString();
    }
}
