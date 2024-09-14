
using Cysharp.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;

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
    public virtual async void OnEvent(MessPackage pkg) { await UniTask.Yield(); }
    public virtual async void GetInfoEvent(MessPackage pkg) { await UniTask.Yield(); }
    public virtual async void AddEvent(MessPackage pkg) { await UniTask.Yield(); }
    public virtual async void ReviseInfoEvent(MessPackage pkg) { await UniTask.Yield(); }
    public virtual async void DeleteInfoEvent(MessPackage pkg) { await UniTask.Yield(); }
    public virtual async void SearchInfoEvent(MessPackage pkg) { await UniTask.Yield(); }
}

public enum OperateType
{
    NONE = 0, GET, ADD, REVISE, DELETE, SEARCH
}

public enum EventType
{
    None = 0,
    UploadEvent,
    DownLoadEvent,
    CheckEvent,
    UserLoginEvent,
    RegisterEvent,
    GetEvent,
    UserEvent,
    MajorEvent,
    FacultyEvent,
    ClassEvent,
    ColumnsEvent,
    CourseEvent,
    ExamineEvent,
    ScoreEvent,
    ResEvent
}