using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image icon;
    public Text numTxt;

    public ItemInfo info;


    public void Clear()
    {
        icon.gameObject.SetActive(false);
        numTxt.gameObject.SetActive(false);
        this.info = null;
    }
    public void SetItemData(ItemInfo iteminfo)
    {
        icon.gameObject.SetActive(true);
        numTxt.gameObject.SetActive(true);
        this.info = iteminfo;
        icon.sprite = GameFramework.Instance.GetModule<AsstModule>().LoadAsset<Sprite>(iteminfo.Config.Icon);
        numTxt.text= iteminfo.num.ToString();
        if(iteminfo.num<=1)
        {
            numTxt.gameObject.SetActive(false);
        }
    }
}
