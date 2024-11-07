using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class NetWorkLauncher : MonoBehaviour
{
    // public static TCPLauncher instance;

    private DispatcherEvent m_dispatcher = new DispatcherEvent();

    public async void Awake()
    {
        DontDestroyOnLoad(this);

        await FileHelper.DownLoadTextFromServer(Application.streamingAssetsPath + "\\IP.txt", (ip) => 
        {
            GlobalData.IP = $"http://{ip}/";
            // TCP.Connect(ip, 5800);
        });
    }

    public void Update()
    {
        if (HTTP.m_MessQueue.Count > 0)
        {
            MessPackage pkg = HTTP.m_MessQueue.Dequeue();
            //Debug.Log(pkg.ret);
            m_dispatcher.Dispatcher(pkg);
        }
    }

    public void OnDestroy()
    {
        // TCPHelper.Close();
        // await UniTask.WaitUntil(() => { return true == TCPHelper.Close(); });
    }
}
