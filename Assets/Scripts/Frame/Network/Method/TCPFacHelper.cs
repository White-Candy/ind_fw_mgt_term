
using System.Collections.Generic;
using LitJson;

public class TCPFacHelper : TCPBaseHelper
{
    /// <summary>
    /// 获取学院信息
    /// </summary>
    public override void GetInfReq()
    {
        base.GetInfReq();
        
        List<FacultyInfo> faculiesList = new List<FacultyInfo>();

        string body = JsonMapper.ToJson(faculiesList);
        TCP.SendAsync(body, EventType.GetFacInfoEvent);
    }

    /// <summary>
    /// 添加学院信息
    /// </summary>
    /// <param name="inf"></param>
    public override void AddInfo(object obj)
    {
        FacultyInfo inf = obj as FacultyInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddFacInfoEvent);
    }

    /// <summary>
    /// 修改学院信息
    /// </summary>
    /// <param name="inf"></param>
    public override void ReviseInfo(object obj)
    {
        FacultyInfo inf = obj as FacultyInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.ReviseFacInfoEvent);
    }

    /// <summary>
    /// 删除学院信息
    /// </summary>
    /// <param name="inf"></param>
    public override void DeleteInfo(object obj)
    {
        FacultyInfo inf = obj as FacultyInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.DeleteFacInfoEvent); 
    }
}