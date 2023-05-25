using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
public enum UIMode
{
    Normal=0,//普通ui
    Modal=1,
}
public class UIConfig
{
    public static int Count;
    private static List<UIConfig> datas;
    private static Dictionary<int, int> indexMap;
    public static void DeserializeByFile(string directory)
    {
        string path = $"{directory}/UIConfig.json";
        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))//System.IO，以流的方式去操作文件，以只读的方式去打开文件
        {   //为文件提供 Stream，既支持同步读写操作，也支持异步读写操作
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fs))//System.IO.StreamReader读写器，专门从流读取数据用的，fs是一个文件流
            {
                datas=new List<UIConfig>();
                indexMap=new Dictionary<int, int>();
                string json=reader.ReadToEnd();//流式的读取，读出所有数据
                JArray array=JArray.Parse(json);//最外层是中括号用JArray.Parse
                //JObject obj = JObject.Parse(json);//最外层是大括号用JObject.Parse
                //Count = array.Count;
                for (int i=0;i<array.Count;i++)
                {
                    JObject dataobject=array[i] as JObject;//带表一个对象，也相当于字典
                    //Debug.LogWarning(dataobject["Asset"]);//属性名相当于键，警告打印出相应的数据
                    UIConfig data=(UIConfig)dataobject.ToObject(typeof(UIConfig));
                    datas.Add(data);
                    indexMap.Add(data.ID, i);
                }
            }
        }
    }
    public static UIConfig ByID(int id)
    {
        if(id<=0)
        {
            return null;
        }
        if(!indexMap.TryGetValue(id,out int intdex))//通过枚举值，来获取下标
        {
            throw new System.Exception("UIConfig找不到ID:{id}");
        }
        return ByIndex(intdex);
    }
    public static UIConfig ByIndex(int intdex)
    {
        return datas[intdex];//通过下标来获取解析出来的数据
    }
    public System.Int32 ID { get; set; }
    public string Description;
    public string Asset { get; set; }
    public UIMode Mode { get; set; }
}
