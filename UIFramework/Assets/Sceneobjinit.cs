using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sceneobjinit : MonoBehaviour
{

    private void Awake()
    {
        GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.LobbyUI);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
