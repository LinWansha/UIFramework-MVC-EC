using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 框架类,管理游戏中的各个模块
/// </summary>
public class GameFramework 
{
    public static GameFramework Instance { get; private set; }//把框架写成单例
    public static bool Initialized { get; private set; }//所有模块是否都被初始化了 
    private Dictionary<Type,BaseModule> m_modules=new Dictionary<Type,BaseModule>();//这个字典用来储存模块

    public static void Initialize()//把GameFramework单例的初始化方法
    {
        Instance =new GameFramework();
    }

     internal void InitModules()//把模块进行初始化
    {
        
        if (Initialized)
            return;
        Initialized = true;
        foreach(BaseModule module in m_modules.Values)
        {
            module.OnModuleInit(); //模块初始化函数，这个方法继承模块基类
        }
    }

    internal void StartModules()
    {
        foreach (var module in m_modules.Values)//遍历所有模块
        {
            module.OnModuleStart();//模块开始的时候
        }
    }


    /// <summary>
    /// 向框架里添加模块
    /// </summary>
    /// <param name="module"></param>
    public void AddModule(BaseModule module)//参数是模块，因为模块继承于BaseModule，
    {
        Type moduleType=module.GetType();//获取BaseModule类的类型,以获取的类型做键
        
        if (m_modules.ContainsKey(moduleType))//判断是否有这个类型的键
        {
            Debug.LogWarning(message: $"Module添加失败，重复:{moduleType.Name}");//有这个键，提示警告
            return;
        }
        else
        {
            m_modules.Add(moduleType, module);//没有这个类型添加，以类型做键，以模块做值
        }
    }

    internal void FixedUpdate(float deltaTime)
    {
        foreach (var module in m_modules.Values)
        {
            module.OnModuleFixedUpdate(deltaTime);
        }
        
    }

    internal void OnDestroy()
    {
        foreach (var module in m_modules.Values)
        {
            module.OnModuleStop();
        }
    }

    internal void LateUpdate(float deltaTime)
    {
        foreach (var module in m_modules.Values)
        {
            module.OnModuleLateUpdate(deltaTime);
        }
    }

    internal void Update(float deltaTime)
    {
         
        foreach (var module in m_modules.Values)
        {
            module.OnModuleUpdate(deltaTime);
        }
    }

    /// <summary>
    /// 从框架中拿出指定的模块
    /// </summary>
    public T GetModule<T>() where T :BaseModule//获取模块的方法
    {
        if(m_modules.TryGetValue(typeof(T), out BaseModule module))//判断有没有这个模块
        {
            return module as T;
        }
        return default(T);
    }
}
