/**
 * UI加载页面
 */

using FairyGUI;
using UnityEngine;

namespace ETModel
{
    public class LoadingUI :MonoBehaviour
    {    
        public static LoadingUI Instatnce;
        private GObject _gObject;
        private GTextField _gTextField;
        private GProgressBar _gProgressBar;

        void Start()
        {
            Instatnce = this;
            UIPackage.AddPackage("UI/Loading");
            _gObject = UIPackage.CreateObject("Loading", "UILoading");
            
            this._gProgressBar = this._gObject.asCom.GetChild("progress").asProgress;
            _gProgressBar.value = 0;
            this._gTextField = _gProgressBar.GetChild("tips").asTextField;
            _gTextField.text = "正在检查版本...";

            GRoot.inst.SetContentScaleFactor(640, 1136, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            GRoot.inst.AddChild(this._gObject);
        }

        public void CloseLoadingUI()
        {
            _gObject.Dispose();
            Destroy(this);
        }

        public void SetProgress(int value)
        {
            this._gProgressBar.value = value;
        }

        public void SetMessage(string Messagee)
        {
            this._gTextField.text = Messagee;
        }

    }//class_end
}