
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class TCPHelper
{
    /// <summary>
    /// 登录请求。
    /// </summary>
    /// <param name="account"></param>
    /// <param name="pwd"></param>
    /// <param name="level"></param>
    public static void LoginReq(string account, string pwd, int level)
    {
        UserInfo inf = new UserInfo
        {
            userName = account,
            password = pwd,
        };

        string sJson = JsonMapper.ToJson(inf);
        TCP.SendAsync(sJson, EventType.UserLoginEvent, OperateType.None);
    }

    /// <summary>
    /// 获取信息请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void GetInfoReq<T>(EventType type) where T : BaseInfo
    {
        // TCPBaseHelper helper = new T();
        // helper.GetInfReq();

        List<T> inf = new List<T>();       
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, type, OperateType.GET);
    }

    /// <summary>
    /// 操作数据请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void OperateInfo<T>(T inf, EventType type, OperateType operateType) where T : BaseInfo
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, type, operateType);
    }

    public static void OperateInfo<T>(List<T> inf, EventType type, OperateType operateType) where T : BaseInfo
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, type, operateType);
    }
}