using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 实体管理的职能，这个模块用来管理实体
/// </summary>
public class ECSModule:BaseModule
{
    public ECSEntity world;//世界的实体类
    private Dictionary<long, ECSEntity> entities;//储存所有实体的字典
     protected internal override void OnModuleInit()
    {
        base.OnModuleInit();
        entities = new Dictionary<long, ECSEntity>();


        world = new ECSEntity();
       //PlayerComponent com= world.AddComponent<PlayerComponent, PlayerInfo>(new PlayerInfo());
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
    /// 添加实体的方法
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(ECSEntity entity)//通过传过来的实体判断是否存在
    {
        this.entities.TryGetValue(entity.InstanceID,out ECSEntity e);
        if(e==null)
        {
            this.entities.Add(entity.InstanceID,entity);
        }
        else
        {
            Debug.LogError("实体重复"+entity.InstanceID);
        }
    }


    /// <summary>
    /// 查询实体的方法
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ECSEntity FindEntity(long id)//通过ID号查找实体
    {
        
        if (this.entities.TryGetValue(id, out ECSEntity e))
        {
            return e;
        }
        return default;
    }



    ///// <summary>
    ///// 添加实体
    ///// </summary>
    ///// <param name="entity"></param>
    //public void AddEntity(ECSEntity entity)//接收实体的参数
    //{
    //    this.entities.TryGetValue(entity.InstanceID,out ECSEntity e);//用实体的类点出来它自身的id，做键进行
    //    if(e == null)
    //    {
    //        this.entities.Add(entity.InstanceID,entity);
    //    }
    //    else
    //    {
    //        Debug.LogError("实体重复"+entity.InstanceID);
    //    }
    //}

    ///// <summary>
    ///// 查找实体
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //public ECSEntity FindEntity(long id)
    //{
    //    this.entities.TryGetValue(id, out ECSEntity e);
    //    if (e != null)
    //    {
    //        return e;
    //    }
    //    return default;
    //}
}
