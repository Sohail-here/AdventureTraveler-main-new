using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gilson.UniversalMethods
{
    public class RandomSystem
    {
        public static T[] RandomSorting<T>(T[] array)
        {
            int len = array.Length;
            List<int> list = new List<int>();
            T[] ret = new T[len];
            System.Random rand = new System.Random(GetRandomSeed());
            int i = 0;
            while (list.Count < len)
            {
                int iter = rand.Next(0, len);
                if (!list.Contains(iter))
                {
                    list.Add(iter);
                    ret[i] = array[iter];
                    i++;
                }
            }
            return ret;
        }

        public static T[] RandomSorting<T>(T[] array, int get_count)
        {
            System.Random rand = new System.Random(GetRandomSeed());

            List<int> raw_index_list = new List<int>();
            int raw_count = array.Length;
            T[] random_list = new T[get_count];
            int index, intex_content;

            for (int i = 0; i < raw_count; i++)
            {
                raw_index_list.Add(i);
            }

            for (int j = 0; j < get_count; j++)
            {
                index = rand.Next(0, raw_index_list.Count);
                intex_content = raw_index_list[index];
                raw_index_list.Remove(intex_content);
                random_list[j] = array[intex_content];
            }

            return random_list;
        }

        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }

    public class PathTool
    {
        public static string CachePath(string CacheFolder)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxPlayer:
                    return Directory.GetParent(Application.dataPath).FullName.Replace('\\', '/') + "/" + CacheFolder + "/";
                case RuntimePlatform.WebGLPlayer:
                    return Application.dataPath + "/" + CacheFolder + "/";
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    return Path.Combine(Application.persistentDataPath, CacheFolder) + "/";
                default:
                    return Application.dataPath + "/" + CacheFolder + "/";
            }
        }
    }

    public class Math
    {
        public static float NumChange(float AxNum, float A_MixNum, float A_MaxNum, float B_MixNum, float B_MaxNum)
        {
            float BxNum = B_MixNum + (AxNum - A_MixNum) * (B_MaxNum - B_MixNum) / (A_MaxNum - A_MixNum);
            return BxNum;
        }
        public static float NumChange(float AxNum, float[] A_NumRange, float[] B_NumRange)
        {
            float BxNum = B_NumRange[0] + (AxNum - A_NumRange[0]) * (B_NumRange[1] - B_NumRange[0]) / (A_NumRange[1] - A_NumRange[0]);
            return BxNum;
        }

    }

    public class UGUIEvent
    {
        public class UGUIEventTrigger : EventTrigger
        {
            public static UGUIEventTrigger Get(GameObject gameObject)
            {
                UGUIEventTrigger trigger = gameObject.GetComponent<UGUIEventTrigger>();
                if (null == trigger)
                {
                    trigger = gameObject.AddComponent<UGUIEventTrigger>();
                }
                return trigger;
            }

            public void AddEventListener(EventTriggerType eventTriggerType, UnityAction<BaseEventData> action)
            {
                Entry entry = new Entry();
                entry.eventID = eventTriggerType;
                entry.callback.AddListener(action);
                if (null == triggers)
                {
                    triggers = new List<Entry>();
                }
                triggers.Add(entry);
            }
        }
    }

    public class Move
    {
        private static Vector3 velocity = Vector3.zero;

        public static Vector3 SmothCameraPostion(Vector3 current, Vector3 target, float smoothTime)
        {
            Vector3 smoothPosition = Vector3.SmoothDamp(current, target, ref velocity, smoothTime);
            return smoothPosition;
        }
    }

    public class StringEncrypt
    {
        public static string SelectHtmlStr(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string newstr = str.Replace("<p>", "").Replace("</p>", "\n")
                               .Replace("<br>", "\n").Replace("</br>", "\n")
                               .Replace("<strong>", "<color=#000000ff>").Replace("</strong>", "</color>")
                               .Replace("&nbsp;", " ");
            return newstr;
        }

    }
}