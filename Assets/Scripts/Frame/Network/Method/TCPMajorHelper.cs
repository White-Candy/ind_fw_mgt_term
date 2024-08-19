
using System.Collections.Generic;
using LitJson;

public class TCPMajorHelper : TCPBaseHelper
{
    /// <summary>
    /// 获取专业信息
    /// </summary>
    public override void GetInfReq()
    {
        base.GetInfReq();
        
        List<MajorInfo> MajorList = new List<MajorInfo>();

        string body = JsonMapper.ToJson(MajorList);
        TCP.SendAsync(body, EventType.GetMajorInfoEvent);
    }

    /// <summary>
    /// 添加专业信息
    /// </summary>
    /// <param name="inf"></param>
    public override void AddInfo(object obj)
    {
        MajorInfo inf = obj as MajorInfo;
        string body = JsonMapper.ToJson(inf);
        TCP.SendAsync(body, EventType.AddMajorInfoEvent);
    }
}