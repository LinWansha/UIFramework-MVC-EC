using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Lobby
{
    public class LobbyUIMediator : UIMediator
    {
        public LobbyUIView View;
        protected override void OnInit(UIView view)
        {
            base.OnInit(view);
            View=view as LobbyUIView;
        }
        protected override void OnShow(object arg)
        {
            base.OnShow(arg);
            View.knackpackBtn.onClick.AddListener(knackbackHandler);
            View.shopBtn.onClick.AddListener(ShopHandler);
            this.View.Name.onClick.AddListener(changenameClickHandler);

            ///侦听M层组件发出的事件
            GameFramework.Instance.GetModule<MessageModule>().Subcribe<MsgType.PlayerNameChanged>(onNameChanged);//通过泛型类指定消息的类型

            PlayerComponent component = GameFramework.Instance.GetModule<ECSModule>().world.GetComponent<PlayerComponent>();
            this.View.lvTxt.text = "Lv:"+component.playerInfo.lv.ToString();
            this.View.unameTxt.text = component.playerInfo.name;
        }

        private void ShopHandler()
        {
            GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.ShopUI);
        }

        private void onNameChanged(MsgType.PlayerNameChanged arg)
        {
            this.View.unameTxt.text = arg.name;
        }

        private void changenameClickHandler()
        {
   
            GameFramework.Instance.GetModule<ECSModule>().world.GetComponent<PlayerComponent>().ChangeName();
        }

        private void knackbackHandler()
        {
            GameFramework.Instance.GetModule<UIModule>().OpenUI(UIViewID.KnapsackUI);
        }
    }

}
