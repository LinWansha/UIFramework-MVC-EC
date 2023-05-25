using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class DialogWindowUIMediator : UIMediator
{
    private DialogWindowUIView uiview;
    private Action<object> subAction;
    private Action<object> cancelAction;
    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        this.uiview = view as DialogWindowUIView;
        
    }
    protected override void OnShow(object arg)
    {
        base.OnShow(arg);
        uiview.submmitBtn.onClick.AddListener(submmitClickhandler);
        uiview.cancelBtn.onClick.AddListener(cancelClickhandler);
        uiview.singleBtn.onClick.AddListener(submmitClickhandler);
        this.uiview.closeBtn.onClick.AddListener(cancelClickhandler);
    }
    protected override void OnHide()
    {
        base.OnHide();
        uiview.submmitBtn.onClick.RemoveListener(submmitClickhandler);//隐藏ui移除事件
        uiview.cancelBtn.onClick.RemoveListener(cancelClickhandler);
        uiview.singleBtn.onClick.RemoveListener(submmitClickhandler);
        this.uiview.closeBtn.onClick.RemoveListener(cancelClickhandler);
    }

    private void cancelClickhandler()//事件显示信息
    {
        cancelAction?.Invoke(null);
        this.Close();
    }

    private void submmitClickhandler()//事件显示信息
    {
        subAction?.Invoke(null);//判断 ?相当于if判断
        this.Close();
    }

    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
    }
    

    public void SetDialog(DialogWindowType btnType, string title, string content, bool isMack, Action<object> submmitCallback, Action<object> cancelCallback)
    {
        switch(btnType)
        {
            case DialogWindowType.SINGLE:
                this.uiview.singleBtn.gameObject.SetActive(true);        
                this.uiview.submmitBtn.gameObject.SetActive(false);        
                this.uiview.cancelBtn.gameObject.SetActive(false);        
                break;
            case DialogWindowType.DOUBLE:
                this.uiview.singleBtn.gameObject.SetActive(false);
                this.uiview.submmitBtn.gameObject.SetActive(true);
                this.uiview.cancelBtn.gameObject.SetActive(true);
                break;
        }
        this.uiview.titleTxt.text = title; 
        this.uiview.contentTxt.text = content;
        this.uiview.uiMask.gameObject.SetActive(isMack);
        subAction = submmitCallback;
        cancelAction=cancelCallback;
    }
}
