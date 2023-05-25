using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息模块
/// </summary>
public class MsgType
{
    /// <summary>
    /// 等级改变事件
    /// </summary>
    public struct LvChanged
    {
        public int lvValue;
    }
    /// <summary>
    /// 经验改变事件
    /// </summary>
    public struct ExpChanged
    {
        public int expValue;
    }
    public struct PlayerNameChanged
    {
        public string name;
    }
    /////.........................todo各种事件
}

public class MessageModule : BaseModule
{
    public delegate void MessageEventArgs<T>(T arg) where T : struct;
    /// <summary>
    /// 存放侦听者的集合
    /// </summary>
    public Dictionary<Type, List<Delegate>> msgMap;
    protected internal override void OnModuleFixedUpdate(float deltaTime)
    {
        base.OnModuleFixedUpdate(deltaTime);
    }

    protected internal override void OnModuleInit()
    {
        base.OnModuleInit();
        msgMap = new Dictionary<Type, List<Delegate>>();
    }

    protected internal override void OnModuleLateUpdate(float deltaTime)
    {
        base.OnModuleLateUpdate(deltaTime);
    }

    protected internal override void OnModuleStart()
    {
        base.OnModuleStart();
    }

    protected internal override void OnModuleStop()
    {
        base.OnModuleStop();
    }

    protected internal override void OnModuleUpdate(float deltaTime)
    {
        base.OnModuleUpdate(deltaTime);
    }

    /// <summary>
    ///  注册 订阅  侦听，一个意思
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="Listener"></param>
    public void Subcribe<T>(MessageEventArgs<T> Listener) where T: struct 
    {
        Type t= typeof(T);
        if(msgMap.TryGetValue(t,out var list))//通过传过来的类型，找到管理委托的集合
        {
            
            if(!list.Contains(Listener))//判断集合包含不包含这个委托
            {
                list.Add(Listener);//不包含向集合里添加委托
            }
        }
        else//在字典没有找到存委托的集合
        {
            List<Delegate> lst=new List<Delegate>();//创建一个委托集合
            lst.Add(Listener);//把委托添加到集合
            msgMap.Add(t, lst);//再用字典添加委托集合
            
        }
    }
    /// <summary>
    /// 移除注册、订阅、侦听，一个意思
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="Listener"></param>
    public void UnSubcribe<T>(T msgType, MessageEventArgs<T> Listener) where T : struct
    {
        Type t = msgType.GetType();
        if (msgMap.TryGetValue(t, out var list))
        {

            if (list.Contains(Listener)) 
            {
                list.Remove(Listener);
            }
        }
        else
        {
            Debug.LogWarning("消息ID{t.name}没有注册,无法移除");
        }
    }
    /// <summary>
    /// 移除某个消息的全部订阅者，侦听者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msgType"></param>
    public void UnSubcribe<T>(T msgType) where T : struct//重载移除函数字典里的集合委托
    {
        Type t = msgType.GetType();
        if (msgMap.TryGetValue(t, out var list))
        {
            list.Clear();
            msgMap.Remove(t);
        }
        else
        {
            Debug.LogWarning("消息ID{t.name}没有注册,无法移除");
        }
    }
    /// <summary>
    /// 向订阅者消息派发
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msgType"></param>
    public void Post<T>(T msgType) where T:struct
    {
        Type t = msgType.GetType();
        if (msgMap.TryGetValue(t, out var list))
        {
            foreach(MessageEventArgs<T> item in list)
            {
                item?.Invoke(msgType);
            }
        }
        else
        {
            Debug.LogWarning("消息ID{t.name}没有注册，无法派发");
        }

    }
}
