using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo//玩家的信息类
{
    public int lv=24;//等级
    public int coin=10000;//金币
    public string name = "孙尚香";
    public int exp=3000;//经验
}
/// <summary>
/// Model层的，管理玩家数据的 model层的类
/// </summary>
public class PlayerComponent : ECSComponent
{
    public PlayerInfo playerInfo=new PlayerInfo();
    public override void OnAddedEntity(object para)//添加组件时被调用
    {
        base.OnAddedEntity(para);
        
    }
    public override void OnRemoveEntity()//移除组件时被调用 
    {
        base.OnRemoveEntity();
    }
    /// <summary>
    /// M层之和数据有关，对外提供获取或者操作的接口
    /// </summary>
    /// <param name="str"></param>
    public void ChangeName()
    {
        string str = "孙香香";
        playerInfo.name = str;

        //通知ui
        var msg = new MsgType.PlayerNameChanged();//实例一个结构体类型
        msg.name = this.playerInfo.name;//用结构体的类型点出来属性进行赋值，当作发消息的参数
        GameFramework.Instance.GetModule<MessageModule>().Post<MsgType.PlayerNameChanged>(msg);
        //var msg = new MsgType.PlayerNameChanged();
        //msg.name = playerInfo.name;
        //GameFramework.Instance.GetModule<MessageModule>().Post<MsgType.PlayerNameChanged>(msg);
    }
}

