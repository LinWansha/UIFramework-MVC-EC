using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfoUIMediator : UIMediator
{
    HeroInfoUIView heroInfoUIView;
    public bool a=false;
    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        heroInfoUIView=view as HeroInfoUIView;
    }
    protected override void OnShow(object arg)
    {
        base.OnShow(arg);
        PlayerComponent component = GameFramework.Instance.GetModule<ECSModule>().world.GetComponent<PlayerComponent>();//ECS模块持有世界实体，实体有添加，获取，删除组件的方法
        heroInfoUIView.coinTxt.text = "金币:" + component.playerInfo.coin;
        heroInfoUIView.lvTxt.text = "LV." + component.playerInfo.lv;
        heroInfoUIView.nameTxt.text = component.playerInfo.name;
        heroInfoUIView.slider.value = component.playerInfo.exp;
        heroInfoUIView.Xs.text = component.playerInfo.exp + "/10000";
        heroInfoUIView.head.sprite = Resources.Load<Sprite>("item901009");
        heroInfoUIView.ReturnBtn.onClick.AddListener(Retu);
        //UIEventList.Get(heroInfoUIView.head.gameObject).onClick+=ImaEvent;

    }

    //private void ImaEvent(GameObject obj)
    //{
    //    a = !a;
    //    if(a)
    //    {
    //        heroInfoUIView.head.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        heroInfoUIView.head.gameObject.SetActive(false);
    //    }
    //}

    private void Retu()
    {
        this.Close();
        GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.LobbyUI);
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }
    protected override void OnHide()
    {
        base.OnHide();
    }
}
