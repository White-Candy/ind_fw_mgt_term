
using Cysharp.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;

public class BaseEvent
{
    /// <summary>
    /// [limite] network unity socket tcp server message: �¼���ʼǰ����ǰ�ð�����
    /// </summary>
    public virtual void OnPrepare(params object[] args) { }

    /// <summary>
    /// ���ÿ��ģʽ��ͬ���¼�
    /// </summary>
    /// <param name="module_name"></param>
    /// <param name="args"></param>
    public virtual async void OnEvent(params object[] args) { await UniTask.Yield(); }
}

public enum EventType
{
    None = 0,
    UploadEvent,
    DownLoadEvent,
    CheckEvent,
    UserLoginEvent,
    RegisterEvent,
    GetStuInfoEvent,
    AddStuInfoEvent
}