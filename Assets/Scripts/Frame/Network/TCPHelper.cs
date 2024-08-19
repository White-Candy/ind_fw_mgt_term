
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public static class TCPHelper
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
            level = level
        };

        string sJson = JsonMapper.ToJson(inf);
        TCP.SendAsync(sJson, EventType.UserLoginEvent);
    }

    /// <summary>
    /// ��ȡ��Ϣ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void GetInfoReq<T>() where T : TCPBaseHelper, new()
    {
        TCPBaseHelper helper = new T();
        helper.GetInfReq();
    }

    /// <summary>
    /// �����Ϣ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void AddInfo<T>(object obj) where T : TCPBaseHelper, new() 
    {
        TCPBaseHelper helper = new T();
        helper.AddInfo(obj);
    }

    /// <summary>
    /// �޸���Ϣ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void ReviseInfo<T>(object obj) where T : TCPBaseHelper, new() 
    {
        TCPBaseHelper helper = new T();
        helper.ReviseInfo(obj);
    }

    /// <summary>
    /// ɾ����Ϣ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void DeleteInfo<T>(object obj) where T : TCPBaseHelper, new() 
    {
        TCPBaseHelper helper = new T();
        helper.DeleteInfo(obj);
    }
}