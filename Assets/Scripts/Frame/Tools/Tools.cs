
using Cysharp.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class Tools
{
    /// <summary>
    /// 等待器
    /// </summary>
    /// <param name="sec"> 秒数 ex: 1秒 => 1.0f </param>
    /// <param name="callback"> CallBack Action </param>
    /// <returns></returns>
    public static async UniTask OnAwait(float sec, Action callback = default)
    {
        int duration = (int)(sec * 1000);
        await UniTask.Delay(duration);
        callback();
    }

    /// <summary>
    /// 动态创建类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T CreateObject<T>(string name) where T : class
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
    /// 动态创建类
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
            obj = Activator.CreateInstance(type); //创建指定类型的实例。
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return obj;
    }


    /// <summary>
    /// 获取本地时间
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
    /// 获取本地时间
    /// </summary>
    /// <returns></returns>
    public static string GetCurrLocalTime_YMD()
    {
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = DateTime.Now.Day.ToString();
        
        return $"{year}/{month}/{day}";
    }

    /// <summary>
    /// 生成一个不在list中重复的随机数
    /// </summary>
    /// <returns></returns>
    public static int SpawnRandom(List<int> live_id)
    {
        List<int> random = new List<int>();
        for (int i = 1; i <= 10000; ++i)
        {
            if (live_id.Exists(x => x == i))
            {
                continue;
            }    
            random.Add(i);
        }

        int idx = UnityEngine.Random.Range(0, random.Count);

        return random[idx]; 
    }


    /// <summary>
    /// 检查容器是否 OutRange
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="vct"></param>
    /// <returns></returns>
    public static bool checkList<T>(List<T> vct, int index)
    {
        return vct.Count > index;
    }

    /// <summary>
    /// 数字判断
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool isDigit(string str)
    {
        float i = 0;
        bool result = float.TryParse(str, out i);
        return result;
    }


    public static string ConvertUnicode(string from)
    {
        var bytes = Encoding.Unicode.GetBytes(from);
        return System.Text.Encoding.Unicode.GetString(bytes);
    }

    /// <summary>
    /// Unicode转字符串
    /// </summary>
    /// <param name="source">经过Unicode编码的字符串</param>
    /// <returns>正常字符串</returns>
    internal static string Unicode2String(string source)
    {
        if (string.IsNullOrEmpty(source)) return "无";
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
    }

    /// <summary>
    /// float 保留小数点后几位
    /// </summary>
    /// <param name="fNum"></param>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static float DigitsRetained(float fNum, int bit)
    {
        float b = 1.0f;
        for (int i = 1; i <= bit; ++i)
        {
            b = b * 10.0f;
        }

        float roundedNum = Mathf.Round(fNum * 10f) / b;
        return roundedNum;
    }
}