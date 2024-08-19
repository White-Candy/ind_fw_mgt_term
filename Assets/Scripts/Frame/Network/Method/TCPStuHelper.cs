
using System.Collections.Generic;
using LitJson;
using OfficeOpenXml.FormulaParsing.Excel.Functions;

public class TCPStuHelper : TCPBaseHelper
{
    /// <summary>
    /// 获取学生信息请求
    /// </summary>
    public override void GetInfReq()
    {
        List<UserInfo> inf = new List<UserInfo>();
        
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.GetStuInfoEvent);
    }

    /// <summary>
    /// 添加用户信息
    /// </summary>
    /// <param name="inf"></param>
    public override void AddInfo(object obj)
    {
        List<UserInfo> inf = obj as List<UserInfo>;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddStuInfoEvent);
    }

    /// <summary>
    /// 修改用户信息
    /// </summary>
    /// <param name="inf"></param>
    public override void ReviseInfo(object obj)
    {
        UserInfo inf = obj as UserInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.ReviseStuInfoEvent);
    }

    /// <summary>
    /// 删除用户信息
    /// </summary>
    /// <param name="inf"></param>
    public override void DeleteInfo(object obj)
    {
        UserInfo inf = obj as UserInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.DeleteStuInfoEvent);
    }
}