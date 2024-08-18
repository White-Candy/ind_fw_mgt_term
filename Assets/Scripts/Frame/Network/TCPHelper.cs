
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public static class TCPHelper
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
            level = level
        };

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

    /// <summary>
    /// 添加用户信息
    /// </summary>
    /// <param name="inf"></param>
    public static void AddUsersInfo(List<UserInfo> inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddStuInfoEvent);
    }

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="inf"></param>
    public static void ReviseUserInfo(UserInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.ReviseStuInfoEvent);
    }

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="inf"></param>
    public static void DeleteUserInfo(UserInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.DeleteStuInfoEvent);
    }

    /// <summary>
    /// 获取学院信息
    /// </summary>
    public static void GetFacInfoReq()
    {
        List<FacultyInfo> faculiesList = new List<FacultyInfo>();

        string body = JsonMapper.ToJson(faculiesList);
        TCP.SendAsync(body, EventType.GetFacInfoEvent);
    }

    /// <summary>
    /// 添加学院信息
    /// </summary>
    /// <param name="inf"></param>
    public static void AddFacInfo(FacultyInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddFacInfoEvent);
    }

    /// <summary>
    /// 修改学院信息
    /// </summary>
    /// <param name="inf"></param>
    public static void ReviseFacInfo(FacultyInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.ReviseFacInfoEvent);
    }

    public static void DeleteFacInfo(FacultyInfo inf)
    {
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.DeleteFacInfoEvent); 
    }
}