using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
		{
            // FairyUI.Open(typeof(LoginSceneUI_FairyGUI));
            FairyUI.Open(typeof(LoginSceneUI_FairyGUI));
		    
		}
	}
}
