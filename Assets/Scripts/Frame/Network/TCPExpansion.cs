
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public static class TCPExp
{
    /// <summary>
    /// 登录请求。
    /// </summary>
    /// <param name="account"></param>
    /// <param name="pwd"></param>
    /// <param name="level"></param>
    public static void LoginReq(string account, string pwd, int level)
    {
        UserInfo inf = new UserInfo();
        inf.userName = account;
        inf.password = pwd;
        inf.level = level;

        string sJson = JsonMapper.ToJson(inf);
        TCP.SendAsync(sJson, EventType.UserLoginEvent);
    }

    /// <summary>
    /// 获取学生信息请求
    /// </summary>
    public static void GetStuInfReq()
    {
        List<UserInfo> inf = new List<UserInfo>();
        
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.GetStuInfoEvent);
    }

    // 添加用户信息
    public static void AddUsersInfo(List<UserInfo> inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddStuInfoEvent);
    }
}