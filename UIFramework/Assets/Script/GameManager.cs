using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 游戏的启动类
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool activing=false;
    private void Awake()
    {
        if(GameFramework.Instance!=null)//判断框架单例类是否为空
        {
            Destroy(gameObject);
            return;
        }
        activing=true;
        DontDestroyOnLoad(gameObject);
        GameFramework.Initialize();
        StartupModules();
        ///初始化框架中管理的各个模块（调用各个模块OnModuleInit）
        GameFramework.Instance.InitModules();
    }
    private void Start()
    {
        ///开始 框架中管理的各个模块（调用各个模块OnModuleInit）
        GameFramework.Instance.StartModules();

       
        ///加载所有的配置表
        ConfigManager.LoadAllConfigsByFile();

        //附加人物信息组件
        GameFramework.Instance.GetModule<ECSModule>().world.AddComponent<PlayerComponent,PlayerInfo>(new PlayerInfo());

        //附加背包数据组件
        GameFramework.Instance.GetModule<ECSModule>().world.AddComponent<KnapsackComponent,ItemInfo>(new ItemInfo());
        GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.LoginUI);
        //测试弹窗的API
        //GameFramework.Instance.GetModule<UIModule>().PogUpDialog(DialogWindowType.SINGLE, "问题", "你想干啥", true,
        //    (x) => { Debug.Log("确定出售，开始出售");},//在弹窗里点击确定之后执行的代码，委托
        //    (x) => { Debug.Log("取消了，啥也不干"); });//在弹窗里点击取消按钮,委托


        //this.Invoke("closeUI",3);//3秒调用


    }
    //public void closeUI()
    //{
    //    GameFramework.Instance.GetModule<UIModule>().CloseUI(UIViewID.LoginUI);
    //}
    public void StartupModules()
    {

        foreach (Transform t in this.transform)//遍历自己的子对象
        {
            BaseModule module = t.GetComponent<BaseModule>();
            GameFramework.Instance.AddModule(module);
        }
        //GameFramework.Instance.AddModule(new UIModule());
        //GameFramework.Instance.AddModule(new AsstModule());
        ///项目中的各种模块进行创建
        //GameFramework.Instance.GetModule<UIModule>();//拿到ui模块
    }
    private void Update()
    {
        GameFramework.Instance.Update(Time.deltaTime);
    }
    private void LateUpdate()
    {
        GameFramework.Instance.LateUpdate(Time.deltaTime);
    }
    private void FixedUpdate()
    {
        GameFramework.Instance.FixedUpdate(Time.deltaTime);
    }
    private void OnDestroy()
    {
        if(activing)
        {
            GameFramework.Instance.OnDestroy();
        }
        
    }
}


