using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UIEventList : EventTrigger
{
    public event Action<GameObject> onClick;//鼠标点击
    public event Action<GameObject> onEnter;//鼠标进入
    public event Action<GameObject> onExit;//鼠标离开
    public event Action<GameObject> onDestroy;//鼠标离开
    public event Action<GameObject> onEndDrag;//鼠标离开
    public event Action<GameObject> onDraging;//鼠标离开
    public static UIEventList Get(GameObject go)
    {
        UIEventList ui=go.GetComponent<UIEventList>();
        if(ui==null)//判断是否为空
        {
            ui=go.AddComponent<UIEventList>();//为空添加组件
        }
        return ui;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        onClick?.Invoke(this.gameObject);
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onEnter?.Invoke(this.gameObject);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onExit?.Invoke(this.gameObject);
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        //onDestroy?.Invoke(this.gameObject,eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
    }
}
