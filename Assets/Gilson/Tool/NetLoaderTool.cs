using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using MEC;
using RestSharp;

namespace Gilson.Network
{
    public interface INetLoaderData
    {
        string url { get; set; }
        Coroutine coroutine { get; set; }
        CoroutineHandle coroutineHandle { get; set; }
        Action failed_act { get; set; } //失敗事件
        Action final_act { get; set; }  //完結事件
        Action<float> progress_act { get; set; } //下載進度事件
        UnityWebRequest WebBaseSetting();
        void success_request(UnityWebRequest request);
        void DownloadStop();
    }

    public interface INetLoaderData<T> : INetLoaderData, ISuccessEvent<T> where T : class { }

    public interface ISuccessEvent<T> where T : class
    {
        T OutPutObject { get; set; }
        Action<string, T> SuccessAction { get; set; }
        void LocalFileSuccessAction(byte[] bytes);
    }

    public abstract class NetLoaderData<T> : INetLoaderData<T> where T : class
    {
        public string url { get; set; }
        public Coroutine coroutine { get; set; }
        public CoroutineHandle coroutineHandle { get; set; }
        public Action failed_act { get; set; }
        public Action final_act { get; set; }
        public Action<float> progress_act { get; set; }

        public T OutPutObject { get; set; }
        public Action<string, T> SuccessAction { get; set; }

        public abstract void success_request(UnityWebRequest request);

        public virtual UnityWebRequest WebBaseSetting()
        {
            return UnityWebRequest.Get(url);
        }

        public void DownloadStop()
        {
            if (!IsDownloading) return;
            if (coroutineHandle != null && coroutineHandle.IsRunning)
                Timing.KillCoroutines(coroutineHandle);
        }

        public bool IsDownloading
        {
            get { return coroutine != null; }
        }

        public abstract void LocalFileSuccessAction(byte[] bytes);
    }

    [System.Serializable]
    public class NetLoaderTool<T1, T2> where T1 : INetLoaderData<T2> where T2 : class
    {
        //設定 Static 是為了跨場景後能保留圖片快取。
        readonly static Dictionary<string, T1> cache_dic = new Dictionary<string, T1>();
        readonly int max_cache_count = 50;
        readonly int delete_count = 10;

        public void DownLoader(T1 data)
        {
            if (cache_dic.TryGetValue(data.url, out var cache))
            {
                if (cache.OutPutObject != null)
                {
                    data.SuccessAction(cache.url, cache.OutPutObject);
                    return;
                }
                cache_dic[data.url] = default;
            }
            cache_dic[data.url] = data;
            data.coroutineHandle = Timing.RunCoroutine(DownLoader2(data));
        }

        private IEnumerator<float> DownLoader2(T1 loader_data)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(loader_data.url);
            var send = www.SendWebRequest();
            yield return Timing.WaitUntilDone(send);
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                loader_data.success_request(www);
                loader_data.final_act?.Invoke();
            }

        }

        private IEnumerator<float> DownLoaderIE(T1 loader_data)
        {

            Debug.Log("DownLoaderIE");
            float tempProgress = 0;

            loader_data.url = Uri.EscapeUriString(loader_data.url).Replace("+", "%20");

            if (!loader_data.url.Contains("://"))
            {
                try
                {
                    Debug.Log("LocalFileSuccessAction");
                    var bytes = System.IO.File.ReadAllBytes(loader_data.url);
                    loader_data.LocalFileSuccessAction(bytes);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Download Local File Error \n {ex.ToString()}");
                    loader_data.failed_act();
                }

                loader_data.final_act?.Invoke();
                yield break;
            }

            using UnityWebRequest www = loader_data.WebBaseSetting();
            www.timeout = 20;
            UnityWebRequestAsyncOperation send = www.SendWebRequest();

            if (loader_data.progress_act != null)
            {
                while (!www.isDone && !(www.result == UnityWebRequest.Result.ConnectionError))
                {
                    if (www.downloadProgress >= 0)
                    {
                        tempProgress = Mathf.Lerp(tempProgress, www.downloadProgress, Time.deltaTime);
                        loader_data.progress_act.Invoke(tempProgress);
                    }
                    yield return Timing.WaitForOneFrame;
                    tempProgress = www.downloadProgress;
                    loader_data.progress_act.Invoke(tempProgress);
                }
            }

            yield return Timing.WaitUntilDone(send);

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(String.Format("<color=red>DownloadRequest Error {0}</color> \n{1}", www.error, www.url));
                loader_data.failed_act?.Invoke();
            }
            else
            {
                loader_data.progress_act?.Invoke(1);
            }

            if (cache_dic.Count >= max_cache_count)
            {
                for (int i = 0; i < delete_count; i++)
                {
                    var key = cache_dic.Keys.ElementAt(i);
                    cache_dic[key] = default;
                    cache_dic.Remove(key);
                }
                Resources.UnloadUnusedAssets();
            }

            if (www.result == UnityWebRequest.Result.Success)
                loader_data.success_request(www);

            loader_data.final_act?.Invoke();
        }

        public void DownloadStop(string url)
        {
            cache_dic[url].DownloadStop();
        }

        public void AllDownloadStop()
        {
            foreach (var cache in cache_dic)
                cache.Value.DownloadStop();
        }
    }
}