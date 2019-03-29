﻿using System;
using FairyGUI;

namespace ETHotfix
{
    public static class FairyUI
    {
        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static BaseUIForms Open(Type type)
        {
            return UIManagerComponent.Instance.ShowUIForms(type);
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        /// <param name="type"></param>
        public static void Close(Type type)
        {
            UIManagerComponent.Instance.CloseUIForms(type);
        }

        /// <summary>
        /// 清空ui
        /// </summary>
        public static void ClearUI()
        {
            Game.Scene.RemoveComponent<UIManagerComponent>();
            Game.Scene.AddComponent<UIManagerComponent>();
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="content"></param>
        private static GComponent tip;

        public static void Tip(string content)
        {
            if (tip == null)
            {
#if UNITY_EDITOR
                UIPackage.AddPackage("UI/Inventory");
#endif
                tip = UIPackage.CreateObject("Inventory", "tipCmp").asCom;
            }

            // SoundComponent.Instance?.PlayClip(SoundName.window_tips);
            tip.GetChild("contentText").asTextField.text = content;
            GRoot.inst.ShowPopup(tip);
            tip.Center();
        }
    } //class_end
}