
using Cysharp.Threading.Tasks;
using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class NetHelper
{
    /// <summary>
    /// ��¼����
    /// </summary>
    /// <param name="account"></param>
    /// <param name="pwd"></param>
    public static void LoginReq(string account, string pwd)
    {
        UserInfo inf = new UserInfo
        {
            userName = account,
            password = pwd,
        };

        string sJson = JsonMapper.ToJson(inf);
        HTTP.SendAsyncPost(sJson, EventType.UserLoginEvent, OperateType.NONE);
    }

    /// <summary>
    /// ��ȡ��ʼ����Ϣ����
    /// </summary>
    public static void GetInitReq()
    {
        List<string> inf = new List<string>();
        string body = JsonMapper.ToJson(inf);
        HTTP.SendAsyncPost(body, EventType.GetEvent, OperateType.NONE);
    }

    /// <summary>
    /// ��ȡ��Ϣ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void GetInfoReq<T>(EventType type) where T : BaseInfo
    {
        List<T> inf = new List<T>();       
        string body = JsonMapper.ToJson(inf);
        HTTP.SendAsyncPost(body, type, OperateType.GET);
    }

    /// <summary>
    /// ������������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void OperateInfo<T>(T inf, EventType type, OperateType operateType) where T : BaseInfo
    {
        string body = JsonMapper.ToJson(inf);
        HTTP.SendAsyncPost(body, type, operateType);
    }

    public static void OperateInfo<T>(List<T> inf, EventType type, OperateType operateType) where T : BaseInfo
    {
        string body = JsonMapper.ToJson(inf);
        HTTP.SendAsyncPost(body, type, operateType);
    }

    /// <summary>
    /// ��Դ�ϴ���������������
    /// </summary>
    /// <param name="list"></param>
    public static async void UploadFile(FilePackage fileInfo)
    {
        string bodymessage = await JsonHelper.AsyncToJson(fileInfo);
        HTTP.SendAsyncPost(bodymessage, EventType.UploadEvent, OperateType.NONE);
    }

    public static void UploadFile(List<FilePackage> fileList)
    {
        foreach (var inf in fileList)
        {
            UploadFile(inf);        
        }
    }



    /// <summary>
    /// �ر�����
    /// </summary>
    public static bool Close()
    {
        // TCP.Close();
        return true;
    }
}