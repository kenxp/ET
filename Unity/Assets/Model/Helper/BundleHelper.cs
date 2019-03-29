using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
	public static class BundleHelper
	{
		public static async ETTask DownloadBundle()
		{
			if (Define.IsAsync)
			{
				try
				{
                    Game.EventSystem.Run(EventIdType.LoadingBegin);
                    using (BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.AddComponent<BundleDownloaderComponent>())
					{
						await bundleDownloaderComponent.StartAsync();
						
						await bundleDownloaderComponent.DownloadAsync();
					}
					
					Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle("StreamingAssets");
					ResourcesComponent.AssetBundleManifestObject = (AssetBundleManifest)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("StreamingAssets", "AssetBundleManifest");

                    Game.EventSystem.Run(EventIdType.LoadingFinish);
                    await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);
                }
				catch (Exception e)
				{
					Log.Error(e);
                    Game.EventSystem.Run(EventIdType.Loading, e.Message, -1);
                    while (true)
                    {
                        await Game.Scene.GetComponent<TimerComponent>().WaitAsync(3000);
                    }
                }

			}
		}

		public static string GetBundleMD5(VersionConfig streamingVersionConfig, string bundleName)
		{
			string path = Path.Combine(PathHelper.AppHotfixResPath, bundleName);
			if (File.Exists(path))
			{
				return MD5Helper.FileMD5(path);
			}
			
			if (false && streamingVersionConfig!=null && streamingVersionConfig.FileInfoDict.ContainsKey(bundleName))
			{
				return streamingVersionConfig.FileInfoDict[bundleName].MD5;	
			}

			return "";
		}
	}
}
