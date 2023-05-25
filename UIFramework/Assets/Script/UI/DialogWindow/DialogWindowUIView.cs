using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂在弹窗预制体上
/// </summary>
public class DialogWindowUIView : UIView
{
    public Button submmitBtn; 
    public Button cancelBtn;
    public Button singleBtn; 
    public Text titleTxt;
    public Text contentTxt;
    public Transform uiMask;
    public Button closeBtn;
}
