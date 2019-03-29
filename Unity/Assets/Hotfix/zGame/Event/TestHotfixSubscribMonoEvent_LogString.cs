/**
 * 发送心跳检测到服务端。
 * 太懒了就不改类名和事件名了。
 *
 */
using ETModel;

namespace ETHotfix
{
    // 分发数值监听
    //[Event(ETModel.EventIdType.TestHotfixSubscribMonoEvent)]
    //public class TestHotfixSubscribMonoEvent_LogString : AEvent<string>
    //{
    //    public override async void Run(string info)
    //    {
    //        if (SessionComponent.Instance == null || SessionComponent.Instance.IsDisposed || SessionComponent.Instance.Session == null)
    //        {
    //            return;
    //        }
    //        long time = TimeHelper.ClientNow() * 10000;
    //        await SessionComponent.Instance.Session.Call(new C2G_HeartTick());
    //        time = TimeHelper.ClientNow() * 10000 - time;
    //      //  UI.Tip(time / 1000 + "ms");
    //    }
    //}
}
