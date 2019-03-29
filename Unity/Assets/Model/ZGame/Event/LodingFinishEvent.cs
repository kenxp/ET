/***
 * 下载结束。
 */
namespace ETModel
{
    [Event(EventIdType.LoadingFinish)]
    public class LodingFinishEvent:AEvent
    {
        public override async void Run()
        {
            LoadingUI.Instatnce?.SetProgress(100);
            LoadingUI.Instatnce?.SetMessage("更新完成。");
            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);
            LoadingUI.Instatnce?.CloseLoadingUI();
        }
    }
}