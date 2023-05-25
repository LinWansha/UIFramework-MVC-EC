using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemConfig
{

    public System.Int32 ID { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// 物品类型，和背包的Type共用
    /// </summary>
    public ItemType Type { get; set; }
    /// <summary>
    /// 折扣力度
    /// </summary>
    public int DisCount { get; set; }
    /// <summary>
    /// 原价
    /// </summary>
    public int OriginalPrice { get; set; }





    public static int Count;
    private static List<ShopItemConfig> datas;
    private static Dictionary<int, int> indexMap;
    public static void DeserializeByFile(string directory)
    {
        string path = $"{directory}/shopItemConfig.json";
        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))//System.IO，以流的方式去操作文件，以只读的方式去打开文件
        {   //为文件提供 Stream，既支持同步读写操作，也支持异步读写操作
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))//System.IO.StreamReader读写器，专门从流读取数据用的，fs是一个文件流
            {
                datas = new List<ShopItemConfig>();
                indexMap = new Dictionary<int, int>();
                string json = reader.ReadToEnd();//流式的读取，读出所有数据
                JArray array = JArray.Parse(json);//最外层是中括号用JArray.Parse
                //JObject obj = JObject.Parse(json);//最外层是大括号用JObject.Parse
                //Count = array.Count;
                for (int i = 0; i < array.Count; i++)
                {
                    JObject dataobject = array[i] as JObject;//带表一个对象，也相当于字典
                    //Debug.LogWarning(dataobject["Asset"]);//属性名相当于键，警告打印出相应的数据
                    ShopItemConfig data = (ShopItemConfig)dataobject.ToObject(typeof(ShopItemConfig));
                    datas.Add(data);
                    indexMap.Add(data.ID, i);
                }
            }
        }
    }
    public static ShopItemConfig ByID(int id)
    {
        if (id <= 0)
        {
            return null;
        }
        if (!indexMap.TryGetValue(id, out int intdex))//通过枚举值，来获取下标
        {
            throw new System.Exception("UIConfig找不到ID:{id}");
        }
        return ByIndex(intdex);
    }
    private static ShopItemConfig ByIndex(int intdex)
    {
        return datas[intdex];//通过下标来获取解析出来的数据
    }
}
