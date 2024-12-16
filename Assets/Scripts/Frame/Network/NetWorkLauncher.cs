using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        await FileHelper.DownLoadTextFromServer(Application.streamingAssetsPath + "\\IP.txt", (url) => 
        {
            // GlobalData.IP = $"http://{url}/";

            string[] split = url.Split(':');
            if (split.Count() == 2)
            {
                string ip = split[0];
                string port = split[1];
                TCP.Connect(ip, int.Parse(port));
            }
        });
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
        NetHelper.Close();
        // await UniTask.WaitUntil(() => { return true == TCPHelper.Close(); });
    }
}
