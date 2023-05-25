using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 商店Mediator
/// </summary>
public class ShopViewMediator : UIMediator
{
    public ShopView View;
    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        Debug.Log("我是ShopUI的Oninit");
        View = view as ShopView;
        View.Close.onClick.AddListener(CloseHandler);
    }

    private void CloseHandler()
    {
        this.Close();
    }

    protected override void OnShow(object arg)
    {
        base.OnShow(arg);
        ShopComponent com=GameFramework.Instance.GetModule<ECSModule>().world.GetComponent<ShopComponent>();
        //int num=com

    }
}
