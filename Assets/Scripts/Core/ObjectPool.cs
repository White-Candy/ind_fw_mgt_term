
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    // 实例方法
    private Func<T> onInstance = () => { return default(T); };

    // 对象存储的队列
    private Queue<T> objQ = new Queue<T>();

    // 最大容量
    private int maxSize = 1000;

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public T Create(string name)
    {
        if (objQ.Count > 0)
        {
            T t = objQ.Dequeue();
            t.gameObject.SetActive(true);
            return t;
        }
        else
        {
            T t = onInstance();
            t.gameObject.SetActive(true);
            System.Random rand = new System.Random();
            t.transform.name =  $"{name}-{rand.Next(10000)}";;
            return t;
        }
    }

    /// <summary>
    /// 初始化方法的注册
    /// </summary>
    /// <param name="func"></param>
    public void RegisterInstance(Func<T> func)
    {
        onInstance = func;
    }

    /// <summary>
    /// 销毁或回收
    /// </summary>
    /// <param name="t"></param>
    public void Destroy(T t)
    {
        if (maxSize <= objQ.Count && t != null)
        {
            t.gameObject.SetActive(false);
            objQ.Enqueue(t);
        }
        else
        {
            GameObject.Destroy(t);
        }
    }
}