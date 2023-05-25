using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 生成ID的工具类
/// </summary>
public static class IDGenerator
{
    /// <summary>
    /// 持有实体的ID
    /// </summary>
    private static long currentInstanceID;
    public static long CurrentInstanceID() { return currentInstanceID; }//通过这个方法来获取currentInstanceID
    public static void ResetInstanceID()//通过这个方法来重置currentInstanceID
    {
        currentInstanceID = 0;
    }
    /// <summary>
    /// 获取唯一的新的实体ID
    /// </summary>
    /// <returns></returns>
    public static long NewInstanceID()
    {
        return ++currentInstanceID;
    }

}
