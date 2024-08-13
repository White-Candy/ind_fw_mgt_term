
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public static class TCPExp
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
    /// ��ȡѧ����Ϣ����
    /// </summary>
    public static void GetStuInfReq()
    {
        List<UserInfo> inf = new List<UserInfo>();
        
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.GetStuInfoEvent);
    }

    /// <summary>
    /// ����û���Ϣ
    /// </summary>
    /// <param name="inf"></param>
    public static void AddUsersInfo(List<UserInfo> inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddStuInfoEvent);
    }

    /// <summary>
    /// �޸��û���Ϣ
    /// </summary>
    /// <param name="inf"></param>
    public static void ReviseUserInfo(UserInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.ReviseStuInfoEvent);
    }
}