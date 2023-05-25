using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动态数据+静态数据
/// </summary>
public class ShopItemInfo
{
    /// <summary>
    /// 配置表数据
    /// </summary>
    public ShopItemConfig Config { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int num { get; set; }//物品的数量
    /// <summary>
    /// 当前售价
    /// </summary>
    public float crtPrice { get; set; }

    /// <summary>
    /// 是否打折?
    /// </summary>
    public bool IsDisCount { get; set; }
}
/// <summary>
/// 商城数据
/// </summary>
public class ShopComponent : ECSComponent
{
    public Dictionary<int,ShopItemInfo> items { get; set; }

    public override void OnAddedEntity(object para)
    {
        base.OnAddedEntity(para);
        items = new Dictionary<int, ShopItemInfo>();
        //模拟一些商城数据
        List<int> list = new List<int>() { 1001, 33, 1002, 200, 1003, 1 };
        for (int i = 0; i < list.Count; i += 2)
        {
            ShopItemInfo info = new ShopItemInfo();
            info.Id = list[i];
            info.num = list[i + 1];
            info.Config = ShopItemConfig.ByID(info.Id);
            info.Name = info.Config.Name;
            info.crtPrice =/*(int) */info.Config.OriginalPrice * (info.Config.DisCount / 10);
            items.Add(info.Id, info);
        }

    }

    public override void OnRemoveEntity()
    {
        base.OnRemoveEntity();
        items.Clear();
        items = null;

    }
}