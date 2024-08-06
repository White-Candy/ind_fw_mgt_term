using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using LitJson;

public static class TCP
{
    public static Socket m_Socket;

    private static IPEndPoint m_Ipend;

    private static int buf_length = 1024000;
    private static byte[] buffer = new byte[buf_length];

    // 内容包队列
    public static Queue<MessPackage> m_MessQueue = new Queue<MessPackage>();
    // 前置包队列
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
                // 前置包获取内容包的总长度和事件类型
                mp.length = int.Parse(data["length"].ToString());
                mp.event_type = data["event_type"].ToString();
                mp.get_length = true;

                FrontMp fp = new FrontMp();
                fp.event_type = data["event_type"].ToString();
                percent = 0.0f; // 在准备队列填装之前 清空上一次消息留下的百分比
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
                    m_MessQueue.Enqueue(mp);
                    mp.Clear();
                }
            }

            m_Socket.BeginReceive(buffer, 0, buf_length, 0, ReviceAsyncCallback, mp);
        }
        catch
        {

        }
    }
}


/// <summary>
/// 这是一个接受完整信息的 信息包类
/// </summary>
public class MessPackage
{
    // public Socket socket = default; // 发送信息的soket
    public string ip = ""; // 他的ip
    public string ret = ""; // 他发送的信息
    public string event_type = ""; // 这个信息属于什么类型
    public int length = 0; // 这个包的总长度
    public bool finish = false; // 是否完全收包
    public bool get_length = false; // 是否已经通过前置包获取到了内容包的总长度

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
/// 前置包结构
/// </summary>
public class FrontMp
{
    public string ip;
    public string length;
    public string event_type;
}