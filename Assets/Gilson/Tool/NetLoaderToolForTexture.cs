using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Gilson.Network
{
    public sealed class NetLoaderDataForTexture2D : NetLoaderData<Texture2D>
    {
        public bool useminmap = false;
        public bool nonReadable = true;

        public override UnityWebRequest WebBaseSetting()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url, nonReadable);//true 為 刪除原始資料節省內存
            return www;
        }

        public override void success_request(UnityWebRequest request)
        {
            OutPutObject = DownloadHandlerTexture.GetContent(request);
            SuccessAction(url, OutPutObject);
        }

        public override void LocalFileSuccessAction(byte[] bytes)
        {
            Texture2D returningTex = new Texture2D(2, 2); //must start with a placeholder Texture object
            returningTex.LoadImage(bytes);
            SuccessAction(url, returningTex);
        }
    }

    public sealed class NetLoaderToolForTexture : NetLoaderTool<NetLoaderDataForTexture2D, Texture2D>
    {
        static NetLoaderToolForTexture instance;
        public static NetLoaderToolForTexture Instance => instance ?? new NetLoaderToolForTexture();
        //(instance = FindObjectOfType<NetLoaderToolForTexture>()) ??
        //new GameObject("NetLoaderToolForTexture").AddComponent<NetLoaderToolForTexture>();

        private void OnDestroy()
        {
            instance = null;
            AllDownloadStop();
        }
    }
}