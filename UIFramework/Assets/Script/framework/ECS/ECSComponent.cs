using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏中所有组件类的基类,抽象的
/// </summary>
public abstract class ECSComponent
{
    /// <summary>
    /// 组件的唯一ID
    /// </summary>
    public long ID { get; private set; }


    /// <summary>
    /// 组件所在实体的ID
    /// </summary>
    public long EntityID { get; set; }


   
    /// <summary>
    /// 组件所在的实体的对象
    /// </summary>
    public ECSEntity Entity//实体的引用，组件在那个实体上 
    {
        get
        {
            return GameFramework.Instance.GetModule<ECSModule>().FindEntity(this.EntityID);//通过ID查找组件在那个实体上 
        }
    }

    public ECSComponent()//组件的构造
    {
        this.ID = IDGenerator.NewInstanceID();//组件的ID也是通过工具类获取唯一的ID
    }

    public virtual void OnAddedEntity(object para)
    {

    }
    public virtual void OnRemoveEntity()
    {

    }




    ///// <summary>
    ///// 组件的唯一ID
    ///// </summary>
    //public long ID { get; private set; }

    ///// <summary>
    ///// 组件所在实体的ID
    ///// </summary>
    //public long EntityID { get;set; }

    ///// <summary>
    ///// 组件所在的实体的对象
    ///// </summary>
    //public ECSEntity Entity
    //{
    //    get
    //    {
    //       return GameFramework.Instance.GetModule<ECSModule>().FindEntity(this.EntityID);//找到组件所在的实体
    //    }
    //}
    //public ECSComponent()
    //{
    //    this.ID = IDGenerator.NewInstanceID();
    //}



    //public virtual void OnAddeEntity(object para)
    //{

    //}
    //public virtual void OnRemoveFromEntity()
    //{

    //}
}
