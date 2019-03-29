/***
 * 下载中。
 */
namespace ETModel
{
    [Event(EventIdType.Loading)]
    public class LodingEvent:AEvent<string,int>
    {
        public override void Run(string message, int progress)
        {
            if (progress < 0)
            {
                LoadingUI.Instatnce.SetMessage($"出错了：[color=#ff0033]{message}[/color]");
            } else { 
                LoadingUI.Instatnce.SetMessage(message);
                LoadingUI.Instatnce.SetProgress(progress);
            }
        }
    }
}