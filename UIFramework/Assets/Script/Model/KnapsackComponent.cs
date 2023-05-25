using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动态数据+静态数据
/// </summary>
public class ItemInfo
{
    /// <summary>
    /// 配置表数据
    /// </summary>
    public ItemConfig Config { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int num { get; set; }//物品的数量
}
public class KnapsackComponent : ECSComponent
{
    /// <summary>
    /// M层所管理的背包里放置的物品的数据
    /// </summary>
    public Dictionary<int, ItemInfo> items { get; set; }
    /// <summary>
    /// 当前的物品格子的数量
    /// </summary>
    public int boxCount { get; set; }
    public override void OnAddedEntity(object para)
    {
        base.OnAddedEntity(para);
        items = new Dictionary<int, ItemInfo>();

        //模拟一些背包数据
        List<int> list = new List<int>() { 1001, 33,1002 ,200, 1003 , 1 };
        for(int i = 0; i < list.Count; i+=2)
        {
            ItemInfo info = new ItemInfo();
            info.Id=list[i];
            info.num=list[i+1];
            info.Config = ItemConfig.ByID(info.Id);
            info.Name = info.Config.Name;
            items.Add(info.Id,info);
        }


        boxCount = 50;
    }

    public override void OnRemoveEntity()
    {
        base.OnRemoveEntity();
        items.Clear();
        items = null;
        boxCount = 0;
    }
}
