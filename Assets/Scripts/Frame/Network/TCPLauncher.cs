using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TCPLauncher : MonoBehaviour
{
    public static TCPLauncher instance;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);

        TCP.Connect("192.168.3.34", 5800);
    }

    public void Update()
    {
        if (TCP.m_FrontQueue.Count > 0)
        {
            FrontMp fp = TCP.m_FrontQueue.Dequeue();
            BaseEvent @event = Tools.CreateObject<BaseEvent>(fp.event_type);
            @event.OnPrepare(fp);
        }

        if (TCP.m_MessQueue.Count > 0)
        {
            MessPackage pkg = TCP.m_MessQueue.Dequeue();
            BaseEvent @event = Tools.CreateObject<BaseEvent>(pkg.event_type);
            @event.OnEvent(pkg);
        }
    }
}
