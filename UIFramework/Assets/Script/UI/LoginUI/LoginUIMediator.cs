using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 中介者模式   UI内部的逻辑，写到Mediator里
/// 持有登录按钮等ui里的组成部分
/// </summary>
public class LoginUIMediator : UIMediator
{
    string yh = "1";
    string mm = "1";
    private LoginUIView loginView;
    protected override void OnInit(UIView view)
    {
        base.OnInit(view);
        Debug.Log("我是登录UI的Oninit");
        loginView=view as LoginUIView;
    }
    private void OnLogin() 
    {
        Debug.Log("开始登录");
        

        if (loginView.YhInp.text == yh && loginView.PawInp.text == mm)
        {
            //GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.LobbyUI);
            // GameFramework.Instance.GetModule<UIModule>().CloseUI(UIViewID.LoginUI);
            SceneManager.LoadScene("mainCity");
            this.Close();
        }
        else
        {
            loginView.t.gameObject.SetActive(true);
            GameFramework.Instance.GetModule<UIModule>().PogUpDialog(DialogWindowType.SINGLE, "问题", "用户或密码错误", true,
           (x) => { Debug.Log("确定出售，开始出售"); },//在弹窗里点击确定之后执行的代码，委托
            (x) => { Debug.Log("取消了，啥也不干"); });//在弹窗里点击取消按钮,委托
        }
    }
    protected override void OnShow(object arg)
    {
        base.OnShow(arg);
        loginView.loginBtn.onClick.AddListener(OnLogin);
        //loginView.infotxt.text =arg.ToString();
    }
    protected override void OnHide()
    {
        base.OnHide();
        Debug.LogError("我是登录UI被关闭了");
    }
    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        //Debug.LogError("我是登录UI的OnUpdate");
    }

}
