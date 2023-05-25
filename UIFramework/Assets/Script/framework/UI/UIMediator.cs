//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 中介这模式
/// </summary>
public abstract class UIMediator
{
    /// <summary>
    /// ui配置表
    /// </summary>
    protected UIConfig uiconfig;
    //和mediator对应点UIView
    protected UIView view;

    /// <summary>
    /// 当UI被关闭的时候，向外发送通知的事件
    /// </summary>
    public event System.Action OnMediatorHide;
    /// <summary>
    /// UI操作的游戏对象
    /// </summary>
    public GameObject Viewobject { get; set; }

    /// <summary>
    /// UI层级
    /// </summary>
    public int SortingOrder { get; set; }

    /// <summary>
    ///  模式    普通   和模式窗口  normal   metal
    /// </summary>
    public UIMode UIMode{ get; set; }
    public virtual void InitMediator(UIView view,UIConfig config)
    {
        
        this.view = view;
        this.uiconfig = config;
        OnInit(view);
    }
    protected virtual void OnInit(UIView view)
    {
    }


    /// <summary>
    /// UI显示出来后调用
    /// </summary>
    /// <param name="go"></param>
    /// <param name="arg"></param>
    public void Show(GameObject go,object arg)
    {
        Viewobject = go;
        OnShow(arg); 
    }
    protected virtual void OnShow(object arg)
    {

    }

    /// <summary>
    /// 当UI被隐藏执行啥
    /// </summary>
    public void Hide()
    { 
        OnHide();
        OnMediatorHide?.Invoke();
        OnMediatorHide = null;
        Viewobject = default;//default是个关键字，代表一个数据类型的默认值，代表null,int a=default;就代表0
    }
    protected virtual void OnHide() { }



    /// <summary>
    /// UI里，如果需要每帧执行某种逻辑的时候，可以重写OnUpdate 
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        OnUpdate(deltaTime);
    }
    protected virtual void OnUpdate(float deltaTime)
    {
    }
    /// <summary>
    /// 关闭自己的方法
    /// </summary>
    protected void Close()
    {
        GameFramework.Instance.GetModule<UIModule>().CloseUI((UIViewID)this.uiconfig.ID);
    }
}
