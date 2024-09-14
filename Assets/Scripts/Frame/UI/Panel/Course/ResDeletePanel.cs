
using UnityEngine.UI;

public class ResDeletePanel : BasePanel
{
    public Button Delete;
    public Button ResPath;

    public override void Awake()
    {
        Active(false);
    }

    public void Start()
    {

    }

    public void Show(params object[] objs)
    {

    }
}

/// <summary>
/// 客户端资源更新前会进行检查请求
/// 如果客户端的版本码和服务器的不一致则需要更新
/// </summary>
public class ResourcesInfo
{
    public string relaPath;
    public string version_code;
    public bool need_updata;


    public ResourcesInfo() { }

    public ResourcesInfo(ResourcesInfo clone)
    {
        relaPath = clone.relaPath;
        version_code = clone.version_code;
        need_updata = clone.need_updata;
    }
}