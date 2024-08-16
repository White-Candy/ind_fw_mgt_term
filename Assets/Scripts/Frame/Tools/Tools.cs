
using Cysharp.Threading.Tasks;
using System;
using System.Net.NetworkInformation;
using UnityEngine;

public static class Tools
{
    /// <summary>
    /// �ȴ���
    /// </summary>
    /// <param name="sec"> ���� ex: 1�� => 1.0f </param>
    /// <param name="callback"> CallBack Action </param>
    /// <returns></returns>
    public static async UniTask OnAwait(float sec, Action callback)
    {
        int duration = (int)(sec * 1000);
        await UniTask.Delay(duration);
        callback();
    }

    /// <summary>
    /// ��̬������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    static public T CreateObject<T>(string name) where T : class
    {
        // Debug.Log(name);
        object obj = null;
        try
        {
            obj = CreateObject(name);
        }
        catch { }
        return obj == null ? null : obj as T;
    }

    /// <summary>
    /// ��̬������
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static object CreateObject(string name)
    {
        object obj = null;
        try
        {
            Type type = Type.GetType(name, true);
            // Debug.Log("Type: " + type.Name);
            obj = Activator.CreateInstance(type); //����ָ�����͵�ʵ����
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return obj;
    }


    /// <summary>
    /// ��ȡ
    /// </summary>
    /// <returns></returns>
    public static string GetCurrLocalTime()
    {
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();

        string hour = DateTime.Now.Hour.ToString();
        string min = DateTime.Now.Minute.ToString();
        string sec = DateTime.Now.Second.ToString();

        return $"{year}/{month}/{day} {hour}:{min}:{sec}";
    }


    /// <summary>
    /// ����һ�������
    /// </summary>
    /// <returns></returns>
    public static int SpawnRandom()
    {
       return UnityEngine.Random.Range(0, 10000);
        
    }
}