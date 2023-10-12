using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static object m_oLock = new object();
    private static T m_oInstance = null;
    public static T Instance
    {
        get
        {
            lock (m_oLock)
            {
                if (m_oInstance == null)
                {
                    m_oInstance = FindObjectOfType<T>();
                }
                //if (m_oInstance == null)
                //{
                //    new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                //}
                else
                {
                    m_oInstance.Init();
                }

                return m_oInstance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (m_oInstance == null)
        {
            m_oInstance = this as T;
            Init();
        }
    }

    public virtual void Init()
    {

    }
}
