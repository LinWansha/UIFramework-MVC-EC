using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Lobby;
/// <summary>
/// ui模块类
/// </summary>
public enum UIViewID : byte///为什么枚举继承byte，int占四个字节，byte占两个字节
{
    LoginUI=1,//登陆的
    LobbyUI=2,//大厅
    BattleUI=3,//战斗
    LoadingUI=4,//加载
    KnapsackUI=5,//背包
    DialogWindow=6,//弹窗
    HeroInfo=7,//英雄的信息
    ShopUI=8,//商店
}
public enum DialogWindowType : byte
{ 
    SINGLE,
    DOUBLE,
}
public class UIModule : BaseModule
{
    [SerializeField] private Transform uiRoot;
    [SerializeField] private Transform normalUIROOT;
    [SerializeField] private Transform metalUIROOT;
    private Dictionary<UIViewID, GameObject> allUIs;//当前打开的UI的游戏对象

    /// <summary>
    /// 存的是当前已经打开的UImediator
    /// </summary>
    private Dictionary<UIViewID, UIMediator> usingMediator;

    private static Dictionary<UIViewID, Type> MEDIATOR_MAPPING;//type存的是UIMediator
        private static Dictionary<UIViewID, Type> ASSET_MAPPING;//type存的是UIView
    protected internal override void OnModuleInit()//OnModuleInit，模块初始化函数
    {
        base.OnModuleInit();
        allUIs = new Dictionary<UIViewID, GameObject>();

        usingMediator = new Dictionary<UIViewID, UIMediator>();
        MEDIATOR_MAPPING = new Dictionary<UIViewID, Type>();
        ASSET_MAPPING = new Dictionary<UIViewID, Type>();

        //注册项目中的UI
        RegisterUI();

    }



    protected internal void RegisterUI()
    {
        //注册一下登录UI
        MEDIATOR_MAPPING.Add(UIViewID.LoginUI, typeof(LoginUIMediator));

        //注册大厅UI
        MEDIATOR_MAPPING.Add(UIViewID.LobbyUI, typeof(LobbyUIMediator));

        //注册弹窗
        MEDIATOR_MAPPING.Add(UIViewID.DialogWindow,typeof(DialogWindowUIMediator));
        //注册人物信息
        MEDIATOR_MAPPING.Add(UIViewID.HeroInfo,typeof(HeroInfoUIMediator));
        MEDIATOR_MAPPING.Add(UIViewID.KnapsackUI,typeof(KnapsackUIMediator));
        MEDIATOR_MAPPING.Add(UIViewID.ShopUI,typeof(ShopViewMediator));
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
        foreach (KeyValuePair<UIViewID, UIMediator> pair in usingMediator)
        {
            pair.Value.Update(deltaTime);
       }
    }
    protected internal override void OnModuleLateUpdate(float deltaTime)
    {
        base.OnModuleLateUpdate(deltaTime);
    }
    protected internal override void OnModuleFixedUpdate(float deltaTime)
    {
        base.OnModuleFixedUpdate(deltaTime);
    }


    /// <summary>
    /// 打开UI的方法
    /// </summary>
    /// <param name="id"></param>
    public UIMediator OpenUI(UIViewID id,object arg=null)
    {
        UIConfig uiConfig = UIConfig.ByID((int)id);
        if(uiConfig != null)
        {
            #region MyRegion
            //if (this.allUIs.TryGetValue(id, out GameObject uiGo))
            //{
            //    uiGo.SetActive(true);
            //}
            //else
            //{
            //    //1.加载资源,获取资源模块点出加载资源的方法
            //    GameObject prefab = GameFramework.Instance.GetModule<AsstModule>().LoadAsset<GameObject>(uiConfig.Asset);
            //    if (prefab != null)
            //    {
            //        GameObject ui = GameObject.Instantiate(prefab);
            //        ui.transform.SetParent(this.uiRoot.transform, worldPositionStays: false);
            //        allUIs.Add(id, ui);
            //    }

            //}


            #endregion
            //Mediator 的处理 
            usingMediator.TryGetValue(id, out UIMediator mediator);
            if(mediator==null)//没有打开
            {
                //加载资源
                GameObject prefad = GameFramework.Instance.GetModule<AsstModule>().LoadAsset<GameObject>(uiConfig.Asset); 
                if(prefad!=null)
                {
                    GameObject ui = GameObject.Instantiate(prefad);//克隆
                    if(uiConfig.Mode==UIMode.Normal)
                    {
                        ui.transform.SetParent(this.normalUIROOT.transform,false);
                    }
                    else if(uiConfig.Mode==UIMode.Modal)
                    {
                        ui.transform.SetParent(this.metalUIROOT.transform,false);
                    }
                    
                    //ui.transform.SetParent(this.uiRoot.transform,false);
                    allUIs.Add(id,ui);//保存打开的预制体对象
                    UIView uiview = ui.GetComponent<UIView>();//预制体上挂着控制内部ui的脚本，他继承于UIView
                    mediator = GetMediator(id);
                    if(mediator!=null)
                    {
                        mediator.InitMediator(uiview,uiConfig);//中介父类的初始化方法
                        mediator.Show(ui,arg);
                        usingMediator.Add(id,mediator);
                        return mediator;
                    }
                }
                else
                {
                    Debug.LogError("加载不到UI预制体资源"+id.ToString());
                } 
            }
            else
            {
                Debug.LogError("UI已经打开了"+id.ToString());
            }
        }
        else
        {
            Debug.LogError("没有找到UI的配置数据");
        }
        return default;//返回默认值null
        
      
    }
    private UIMediator GetMediator(UIViewID id)
    {
        UIMediator mediator = null;
        if(MEDIATOR_MAPPING.TryGetValue(id,out Type type))
        {
            //通过Type创建Type的实例,相当于new
             mediator = System.Activator.CreateInstance(type) as UIMediator;
        }
        else
        {
            Debug.LogError("这个UI没有被注册");
        }
        return mediator;
    }
    /// <summary>
    /// 关闭UI的方法
    /// </summary>
    /// <param name="id"></param>
    public void CloseUI(UIViewID id)
    {
        usingMediator.TryGetValue(id,out UIMediator mediator);
        if (mediator!=null)//没有打开
        {
            mediator.Hide();
            usingMediator.Remove(id);//从已经打开的mediator列表中移除
            allUIs.TryGetValue(id,out GameObject go);
            if(go!=null)
            {
                allUIs.Remove(id);
                GameObject.DestroyImmediate(go);
            }
        }
        else
        {
            Debug.LogWarning(message: "没有找到关闭的UI"+id.ToString());
        }
    }

    /// <summary>
    /// 打开弹窗方法
    /// </summary>
    /// <param name="btnType">按钮类型，一个按钮还是两个</param>
    /// <param name="title">标题</param>
    /// <param name="content">弹窗内容</param>
    /// <param name="isMack">遮罩是否启动</param>
    /// <param name="submmitCallback">确定回调</param>
    /// <param name="cancelCallback">取消回调</param>
    public void PogUpDialog(DialogWindowType btnType, string title, string content, bool isMack=true,
        Action<object> submmitCallback=null, Action<object> cancelCallback=null)
    {
        DialogWindowUIMediator mediator=this.OpenUI(UIViewID.DialogWindow) as DialogWindowUIMediator;//先把UI打开
        if(mediator!=null)
        {
            mediator.SetDialog(btnType, title,content,isMack,submmitCallback,cancelCallback); 
        }
        else
        {
            Debug.LogError("没打开弹窗");
        }
    }
}
