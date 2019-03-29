/**
 * 询问面板
 * **/

using System.Collections.Generic;
using FairyGUI;
 

namespace ETHotfix
{
    class AskPanelUI : BaseUIForms
    {
        public AskPanelUI()
        {
            this.pakName = "Common";
            this.cmpName = "AskPanel";
            this.CurrentUIType.NeedClearingStack = false;
            this.CurrentUIType.UIForms_ShowMode = UIFormsShowMode.ReverseChange;
            this.CurrentUIType.UIForms_Type = UIFormsType.Window;
        }
        private GComponent gCmp;
        public override void InitUI()
        {
            base.InitUI();
            gCmp = this.GObject.asCom;
            this.gCmp.Center();
         //   this.Window.Show();
            gCmp.GetChild("ok_btn").asButton.onClick.Add(() =>
            {
                Log.Debug("点击了ok");
                //Game.EventSystem.Run(EventIdType.AnswerServerEvent,GameConstant.GAME_YESORNO_YES);
                this.Close();
            });
            gCmp.GetChild("cancle_btn").asButton.onClick.Add(() =>
            {
                Log.Debug("点击了取消");
                //Game.EventSystem.Run(EventIdType.AnswerServerEvent, GameConstant.GAME_YESORNO_NO);
                this.Close();
            });
            gCmp.GetChild("return_btn").asButton.onClick.Add(() => { Log.Debug("点击了返回");this.Close(); });
        }

        public void SetAskContent(string  askContent)
        {
            gCmp.GetChild("text_askContent").asTextField.text = askContent;
        }
    }
}
