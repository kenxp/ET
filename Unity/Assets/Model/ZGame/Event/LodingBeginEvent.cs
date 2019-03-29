/***
 * 开始下载。
 */

using UnityEngine;

namespace ETModel
{
    [Event(EventIdType.LoadingBegin)]
    public class LodingBeginEvent:AEvent
    {
        public override void Run()
        {

            Game.Scene.GameObject.AddComponent<LoadingUI>();

        }
    }
}