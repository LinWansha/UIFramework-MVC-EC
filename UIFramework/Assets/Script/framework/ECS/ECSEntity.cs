using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///实体类
/// </summary>
public class ECSEntity
{
    public long InstanceID { get; private set; }
    private Dictionary<Type, ECSComponent> componentMap= new Dictionary<Type, ECSComponent>();//来储存实体上的所有的组件
    public ECSEntity()//实体的构造
    {
        
        InstanceID = IDGenerator.NewInstanceID();//从工具类获取ID号，作为实体的ID
        GameFramework.Instance.GetModule<ECSModule>().AddEntity(this);//通过框架获取模块的方法获取添加实体的方法
    }



    /// <summary>
    /// 添加实体的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="P"></typeparam>
    /// <param name="para"></param>
    /// <returns></returns>
    public T AddComponent<T,P>(P para) where T : ECSComponent//添加组件的方法
    {
        Type type = typeof(T);
        if(!componentMap.TryGetValue(type, out ECSComponent com))
        {
            ECSComponent component = System.Activator.CreateInstance(type) as ECSComponent;
            component.OnAddedEntity(para);
            componentMap.Add(type, component);
            return component as T;
        }
        else
        {
            Debug.LogError("已经添加过组件"+type.ToString());
        }
        return default;
    }
    /// <summary>
    /// 移除组件的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RemoveComponent<T>() where T : ECSComponent//移除组件的方法
    {
        Type type = typeof(T);
        if (componentMap.TryGetValue(type, out ECSComponent com))
        {
            componentMap.Remove(type);
        }
    }

    /// <summary>
    /// 获取组件的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetComponent<T>() where T:ECSComponent
    {
        Type type = typeof(T);
        if(componentMap.TryGetValue(type,out ECSComponent com))
        {
            return com as T;
        }
        return default;
    }




    //    /// <summary>
    //    /// 实体的标识
    //    /// </summary>
    //    public long  InstanceID { get; private set; }

    //    private Dictionary<Type, ECSComponent> componentMap = new Dictionary<Type, ECSComponent>();//所有附加到这个实体上的组件
    //    public ECSEntity()
    //    {
    //        InstanceID =IDGenerator.NewInstanceID();
    //        GameFramework.Instance.GetModule<ECSModule>().AddEntity(this);//把当前的实体类传给添加实体的方法
    //    }


    //    /// <summary>
    //    /// 像实体里添加组件
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <returns></returns>
    //    public T AddComponent<T,P>(P para) where T : ECSComponent
    //    {
    //        Type type = typeof(T);
    //        if(!componentMap.TryGetValue(type,out ECSComponent com))
    //        {
    //            ECSComponent component = System.Activator.CreateInstance(type) as ECSComponent;
    //            component.OnAddeEntity(para);
    //            componentMap.Add(type, component);//以这个类型做键，这个类型的实例做值
    //            return component as T;
    //        }
    //        else
    //        {
    //            Debug.LogError("已经添加过这个组件了" + type.ToString());
    //        }
    //        return default;
    //    }

    ///// <summary>
    ///// 从实体中移除组件的方法
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //    public void RemoveComponent<T>() where T : ECSComponent
    //    {
    //        Type type = typeof(T);
    //        if (componentMap.TryGetValue(type, out ECSComponent com))
    //        {
    //            componentMap.Remove(type);
    //        }
    //    }
}
