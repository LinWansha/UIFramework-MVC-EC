using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 模块的抽象基类，为各个模块，提供一些声明周期函数
/// </summary>
public abstract class BaseModule : MonoBehaviour
{
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnDestroy()
    {
        
    }


    /// <summary>
    /// 生命周期函数
    /// </summary>
    protected internal virtual void OnModuleInit() { }//模块初始化的时候
    protected internal virtual void OnModuleStart() { }//模块开始的时候
    protected internal virtual void OnModuleStop() { }//模块停止的时候
    protected internal virtual void OnModuleUpdate(float deltaTime) { }//模块每一帧执行的时候
    protected internal virtual void OnModuleLateUpdate(float deltaTime) { }
    protected internal virtual void OnModuleFixedUpdate(float deltaTime) { }

}
