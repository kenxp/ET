/***
 *	Tittle: "SUIFW" UI框架项目
 *		主题:UI窗体基础类
 *	Description:
 *		功能: 定义所有UI窗体共有方法。
 *		定义四个生命周期
 *		1：Display 显示状态。
 *		2：Hiding  隐藏状态。
 *		3：ReDisney 再显示状态。
 *		4：Freeze 冻结状态。
 *		
 *
 *	Date:   2017
 *	version:    0.1版本
 *	Modify Record:
 *
 */

using System.Threading.Tasks;
using FairyGUI;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public class BaseUIForms
    {
        #region 属性

        /*字段*/

        private FUIType _CurrentUIType = new FUIType();

        /*属性*/
        public FUIType CurrentUIType
        {
            get
            {
                return _CurrentUIType;
            }
            set
            {
                _CurrentUIType = value;
            }
        }

        public string pakName;
        public string cmpName;

        /*对应的FairyGUi的ui组件 */
        public GObject GObject;

        /*Fairygui的窗口。如果是弹出窗体，使用Window展示。否则使用GRoot展示。*/
        public ExWindow Window;

        #endregion

        #region 脚本生命周期

        public void Awake()
        {
            if (string.IsNullOrEmpty(this.pakName) || string.IsNullOrEmpty(this.cmpName))
            {
                Log.Error(this.GetType() + "/Awake() 初始化Ui界面失败，pakName或cmpName为空！");
                return;
            }
            if (!Define.IsAsync) {
                UIPackage.AddPackage("Assets/Res/UI/" + pakName);
                Log.Info($"初始化Ui: {pakName}");
            } else { 
                Log.Info("/ ab包 网络模式！");
                if (UIPackage.GetByName(pakName) == null)
                {
                    string bundleName = this.pakName.StringToAB();
                    Log.Info($"加载Ui bundleName:{bundleName}");
                    ResourcesComponent rc = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                    rc.LoadOneBundle(bundleName);
                    AssetBundle bundle = rc.bundles[bundleName].AssetBundle;
                    //赋值给FairyGUi。
                    UIPackage.AddPackage(bundle);
                }
            }



            this.GObject = UIPackage.CreateObject(pakName, cmpName);
            if (this.GObject == null)
            {
                Log.Error(this.GetType() + "/Awake() 获取不到FairyGui对象！确认项目配置正确后，请检查包名：" + this.pakName + "，组件名:" + this.cmpName);
                return;
            }

            InitUI();

            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window)
            {
                this.Window = new ExWindow();
                this.Window.contentPane = this.GObject.asCom;
                this.Window.doShowAnimationEvent += DoShowAnimationEvent;
                this.Window.doHideAnimationEvent += DoHideAnimationEvent;
                this.Window.onHideEvent += OnHideEvent;
            }
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
        protected void Close()
        {
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window)
            {
                if (this.Window != null)
                    this.Window.Hide();
            }

            UIManagerComponent.Instance.CloseUIForms(this.GetType());
        }

        #region 子类重写方法

        /// <summary>
        /// UI初始化方法。_必须
        /// </summary>
        public virtual void InitUI()
        {
        }

        /// <summary>
        /// 重写窗口关闭时的逻辑。_窗口 弹出窗体。
        /// </summary>
        /// <returns></returns>
        public virtual Task HideEvent()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 重写打开窗口时动画的展示方法 
        /// </summary>
        public virtual void DoShowAnimationEvent()
        {
        }

        /// <summary>
        /// 重写关闭窗口的动画展示方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task HideAnimationEvent()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 窗口关闭动画
        /// </summary>
        private async void DoHideAnimationEvent()
        {
            await HideAnimationEvent();
            if (this.Window != null)
                this.Window.HideImmediately();
        }

        /// <summary>
        /// 关闭窗体事件
        /// </summary>
        private async void OnHideEvent()
        {
            await HideEvent();
        }

        #endregion

        #endregion

        #region 窗体生命周期

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window)
            {
                this.Window.Show();
            }
            else
            {
                GRoot.inst.AddChild(this.GObject);

                this.DoShowAnimationEvent();
            }
        }

        /// <summary>
        /// 隐藏状态(窗口关闭了)
        /// </summary>
        public virtual void Hiding()
        {
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window)
            {
                if (!this.Window.isDisposed)
                {
                    this.Window.doShowAnimationEvent -= DoShowAnimationEvent;
                    this.Window.doHideAnimationEvent -= DoHideAnimationEvent;
                    this.Window.onHideEvent -= OnHideEvent;
                    this.Window.Dispose();
                }

                if (!this.GObject.isDisposed)
                {
                    this.GObject.Dispose();
                }
            }
            else if (this.GObject.parent != null)
            {
                this.OnHideEvent();
                this.DoHideAnimationEvent();
                this.GObject.Dispose();
            }
        }

        /// <summary>
        /// 再显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window && this.Window.parent != null)
            {
                this.Window.visible = true;
            }
            else
            {
                this.GObject.visible = true;
            }
        }

        /// <summary>
        /// 冻结状态（在栈集合中）
        /// </summary>
        public virtual void Freeze()
        {
            //弹出窗体
            if (_CurrentUIType.UIForms_Type == UIFormsType.Window && this.Window.parent != null)
            {
                this.Window.visible = false;
            }
            else
            {
                this.GObject.visible = false;
            }
        }

        #endregion
    } //class_end
}