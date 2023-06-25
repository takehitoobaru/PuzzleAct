using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    int itemID = 0; //アイテム番号

    [SerializeField]
    Text possessionText;

    void Start()
    {
        possessionText.text = Stage1Manager.current.itemPossession[itemID].ToString();
    }

    void Update()
    {
        possessionText.text = Stage1Manager.current.itemPossession[itemID].ToString();
    }

    public void OnClickItemButton()
    {
        Stage1Manager.current.selectItem = itemID;
    }
}
