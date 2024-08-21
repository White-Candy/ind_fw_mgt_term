
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class TCPHelper
{
    /// <summary>
    /// ��¼����
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
        TCP.SendAsync(sJson, EventType.UserLoginEvent, OperateType.NONE);
    }

    /// <summary>
    /// ��ȡ��ʼ����Ϣ����
    /// </summary>
    public static void GetInitReq()
    {
        List<string> inf = new List<string>();
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.GetEvent, OperateType.NONE);
    }

    /// <summary>
    /// ��ȡ��Ϣ����
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
    /// ������������
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