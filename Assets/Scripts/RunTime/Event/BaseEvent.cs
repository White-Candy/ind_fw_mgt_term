
using Cysharp.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;

public class BaseEvent
{
    /// <summary>
    /// [limite] network unity socket tcp server message: 事件开始前处理前置包内容
    /// </summary>
    public virtual void OnPrepare(params object[] args) { }

    /// <summary>
    /// 点击每个模式不同的事件
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