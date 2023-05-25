using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager
{
    private static string directory = Application.streamingAssetsPath+"/config";
    /// <summary> 
    /// 统一调用所有配置表，配置表的管理类
    /// </summary>
    public static void LoadAllConfigsByFile()
    {
        UIConfig.DeserializeByFile(directory);//解析类
        Debug.Log("------------UIConfig解析配置表");


        ItemConfig.DeserializeByFile(directory);
        Debug.Log("------------BagItemConfig解析配置表");

        ShopItemConfig.DeserializeByFile(directory);
        Debug.Log("------------ShopItemConfig解析配置表");
        //所有的配置表在这里统一加载解析

        //Debug.LogError(ShopItemConfig.ByID(1001).OriginalPrice);
    }
}
