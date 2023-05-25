using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackUIMediator : UIMediator
{
    public KnapsackUIView uiview;


    private List<Item> allItemBox;


    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        this.uiview = view as KnapsackUIView;
        uiview.Close.onClick.AddListener(CloseBtn);
        allItemBox=new List<Item>();
    }

    private void CloseBtn()
    {
        this.Close();
    }

    protected override void OnShow(object arg)
    {
        base.OnShow(arg);
        KnapsackComponent com=GameFramework.Instance.GetModule<ECSModule>().world.GetComponent<KnapsackComponent>();
        int boxnum = com.boxCount;
        for(int i=0;i< boxnum;i++)
        {
            GameObject prefad = GameFramework.Instance.GetModule<AsstModule>().LoadAsset<GameObject>("ui/KnapsackUI/item");
            GameObject box=GameObject.Instantiate(prefad);
            box.transform.SetParent(uiview.Content.transform,false);
            Item item=box.GetComponent<Item>();
            item.Clear();
            allItemBox.Add(item);
        }
        List<ItemInfo> tmp=new List<ItemInfo>(com.items.Values);//把字典键转换成一个集合
        for(int i=0;i< tmp.Count;i++)
        {
            allItemBox[i].SetItemData(tmp[i]);
        }
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }
}
