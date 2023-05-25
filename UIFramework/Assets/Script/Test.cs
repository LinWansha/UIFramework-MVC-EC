using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    UnityEngine.Object a = new UnityEngine.Object();
    System.Object b = new UnityEngine.Object();

    public GameObject obj;
    void Start()
    {
        print(a == null);//True
        print(b == null);//False

        StartCoroutine(DestroyObj(obj));
    }

    IEnumerator DestroyObj(GameObject go)
    {

        Debug.Log("StartCoroutine");
        Destroy(go);
        Debug.Log("go:::" + go);
        Debug.Log("go transform::1:::" + go.transform);
        Debug.Log("type::1::" + go.GetType().FullName);
        go = null;
        yield return new WaitForEndOfFrame();
        if (go == null)
        {
            Debug.Log("null===1");
        }
        if (go is GameObject)
        {
            Debug.Log("type::2::" + go.GetType().FullName);
        }
        Destroy(go);
        if (go == null)
        {
            Debug.Log("null===2");
        }
        Debug.Log("go transform::2:::" + go.transform);//这里是77行
        Destroy(go);
        yield break;
    }
}
