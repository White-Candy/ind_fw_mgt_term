using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class TCPLauncher : MonoBehaviour
{
    // public static TCPLauncher instance;

    private DispatcherEvent m_dispatcher = new DispatcherEvent();

    public void Awake()
    {
        DontDestroyOnLoad(this);

        TCP.Connect("127.0.0.1", 5800);
    }

    public void Update()
    {
        if (TCP.m_MessQueue.Count > 0)
        {
            MessPackage pkg = TCP.m_MessQueue.Dequeue();
            //Debug.Log(pkg.ret);
            m_dispatcher.Dispatcher(pkg);
        }
    }

    public void OnDestroy()
    {
        TCPHelper.Close();
        //await UniTask.WaitUntil(() => { return true == TCPHelper.Close(); });
    }
}
