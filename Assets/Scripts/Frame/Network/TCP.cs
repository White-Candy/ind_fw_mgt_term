using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using LitJson;
using Unity.VisualScripting;
using UnityEditor;

public static class TCP
{
    public static Socket m_Socket;

    private static IPEndPoint m_Ipend;

    private static int buf_length = 1024000;
    private static byte[] buffer = new byte[buf_length];

    // ���ݰ�����
    public static Queue<MessPackage> m_MessQueue = new Queue<MessPackage>();
    // ǰ�ð�����
    public static Queue<FrontMp> m_FrontQueue = new Queue<FrontMp>();

    public static float percent;

    public static void Connect(string ip, int port)
    {
        m_Ipend = new IPEndPoint(IPAddress.Parse(ip), port);
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        m_Socket.BeginConnect(m_Ipend, ConnectCallbackAsync, null);
    }

    private static void ConnectCallbackAsync(IAsyncResult ar)
    {
        // Socket socket = (Socket)ar.AsyncState;
        if (m_Socket != null)
        {
            // Debug.Log($"Socket Connect: {m_Socket.Connected}");
            m_Socket.EndConnect(ar);

            MessPackage mp = new MessPackage();
            m_Socket.BeginReceive(buffer, 0, buf_length, 0, ReviceAsyncCallback, mp);
        }
    }

    public static void ReviceAsyncCallback(IAsyncResult ar)
    {
        MessPackage mp = (MessPackage)ar.AsyncState;
        int length = m_Socket.EndReceive(ar);
        try
        {
            string mess = Encoding.Unicode.GetString(buffer, 0, length);
            Array.Clear(buffer, 0, buffer.Length);
            //Debug.Log("+++++" + mess); // log message of front package

            if (!mp.get_length)
            {
                JsonData data = JsonMapper.ToObject(mess);
                // ǰ�ð���ȡ���ݰ����ܳ��Ⱥ��¼�����
                mp.length = int.Parse(data["length"].ToString());
                mp.event_type = data["event_type"].ToString();
                mp.get_length = true;
              
                FrontMp fp = new FrontMp();
                fp.event_type = data["event_type"].ToString();
                percent = 0.0f; // ��׼��������װ֮ǰ �����һ����Ϣ���µİٷֱ�
                m_FrontQueue.Enqueue(fp);
            }
            else
            {
                if (mp.length > mp.ret.Count())
                {
                    mp.ret += mess;
                }

                percent = (float)mp.ret.Count() * 1.0f / (float)mp.length * 1.0f * 100.0f;
                // Debug.Log("----------" + percent + " || " + mess);  // Add message package for queue.

                if (percent >= 100.0f)
                {
                    mp.finish = true;

                    MessQueueAdd(mp);
                    mp.Clear();
                }
            }

            m_Socket.BeginReceive(buffer, 0, buf_length, 0, ReviceAsyncCallback, mp);
        }
        catch
        {

        }
    }

    /// <summary>
    /// �첽������Ϣ
    /// </summary>
    /// <param name="mess">����</param>
    /// <param name="event_type">�¼�����</param>
    public static async void SendAsync(string mess, EventType event_type, OperateType operateType)
    {
        SendFrontPackage(mess, event_type, operateType);

        await Tools.OnAwait(0.1f, () =>
        {
            var outputBuffer = Encoding.Unicode.GetBytes(mess);
            m_Socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, SendAsyncCbk, null);
        });
    }

    /// <summary>
    /// ����ǰ�ð�
    /// </summary>
    /// <param name="mess"></param>
    /// <param name="event_type"></param>
    public static void SendFrontPackage(string mess, EventType event_type, OperateType operateType)
    {
        FrontMp mpinfo = new FrontMp()
        {
            ip = NetTools.GetIPForTypeIPV4(),
            length = mess.Count().ToString(),
            event_type = event_type.ToSafeString(),
            operate_type = operateType.ToSafeString()
        };

        string s_info = JsonMapper.ToJson(mpinfo);
        var outputBuffer = Encoding.Unicode.GetBytes(s_info);
        m_Socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, SendAsyncCbk, null);
    }

    /// <summary>
    /// �첽���ͻص�
    /// </summary>
    /// <param name="ar"></param>
    private static void SendAsyncCbk(IAsyncResult ar)
    {
        try
        {
            if (m_Socket != null)
            {
                m_Socket.EndSend(ar);
            }
        }
        catch (SocketException e)
        {
            Debug.Log("socket send fail" + e.ToString());
        }
    }

    /// <summary>
    /// Ϊ��Ϣ���� Clone pkg ���Ҵ��
    /// </summary>
    /// <param name="pkg"></param>
    public static void MessQueueAdd(MessPackage mp)
    {
        MessPackage pkg = new MessPackage(mp);
        m_MessQueue.Enqueue(pkg);
    }
}


/// <summary>
/// ����һ������������Ϣ�� ��Ϣ����
/// </summary>
public class MessPackage
{
    // public Socket socket = default; // ������Ϣ��soket
    public string ip = ""; // ����ip
    public string ret = ""; // �����͵���Ϣ
    public string event_type = ""; // �����Ϣ����ʲô����
    public int length = 0; // ��������ܳ���
    public bool finish = false; // �Ƿ���ȫ�հ�
    public bool get_length = false; // �Ƿ��Ѿ�ͨ��ǰ�ð���ȡ�������ݰ����ܳ���

    public void Clear()
    {
        // socket = default;
        ip = "";
        ret = "";
        event_type = "";
        length = 0;
        finish = false;
        get_length = false;
    }

    public MessPackage() { }

    public MessPackage(MessPackage mp)
    {
        // socket = mp.socket;
        ip = mp.ip;
        ret = mp.ret;
        event_type = mp.event_type;
        length = mp.length;
        finish = mp.finish;
        get_length = mp.get_length;
    }
}

/// <summary>
/// ǰ�ð��ṹ
/// </summary>
public class FrontMp
{
    public string ip;
    public string length;
    public string event_type;
    public string operate_type;
}