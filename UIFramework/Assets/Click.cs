using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("item901009");
        GameFramework.Instance.GetModule<UIModule>().CloseUI(UIViewID.LobbyUI);
        GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.HeroInfo);
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
