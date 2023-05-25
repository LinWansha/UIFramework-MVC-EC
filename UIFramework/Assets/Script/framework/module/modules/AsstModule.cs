using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 简单提供资源缓存功能
/// </summary>
public class AsstModule : BaseModule
{
    /// <summary>
    /// 缓存
    /// </summary>
    public Dictionary<string, UnityEngine.Object> assetmap;
     
    protected internal override void OnModuleInit()
    {
        base.OnModuleInit();
        assetmap = new Dictionary<string, UnityEngine.Object>();
        Debug.Log(message: "AsstModule 初始化");
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
    protected internal override void OnModuleLateUpdate(float deltaTime)
    {
        base.OnModuleLateUpdate(deltaTime);
    }
    protected internal override void OnModuleFixedUpdate(float deltaTime)
    {
        base.OnModuleFixedUpdate(deltaTime);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">返回的值</typeparam>
    /// <param name="path">要加载的路径</param>
    /// <returns></returns>
    public T LoadAsset<T>(string path)where T: UnityEngine.Object
    {
        //加载资源看缓存里有没有
        if (this.assetmap.TryGetValue(path, out UnityEngine.Object asset))
        {
            return asset as T;
        }
        else
        {
            //没有被加载过的资源
            T obj=Resources.Load<T>(path);
            if(obj==null)
            {
                Debug.LogWarning(message:"加载的资源，没有加载成功");
                
                return null;
            }
            else
            {
                assetmap.Add(path, obj);
                return obj;
            }
            
        }
        
    }
}
