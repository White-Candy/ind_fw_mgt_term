
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ResDeletePanel : BasePanel
{
    public ResItem Item;
    public Transform ParentTrans;

    private List<ResourcesInfo> m_ResourcesInfo = new List<ResourcesInfo>();

    private List<ResItem> m_ItemList = new List<ResItem>();

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {

    }

    public override void Init()
    {
        TCPHelper.GetInfoReq<ResourcesInfo>(EventType.ResEvent);   
    }

    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_ResourcesInfo = JsonMapper.ToObject<List<ResourcesInfo>>(ret);
        foreach (ResourcesInfo inf in m_ResourcesInfo)
        {
            CloneItem(inf);
        }
    }

    public void CloneItem(ResourcesInfo inf)
    {
        ResItem item = Instantiate(Item, ParentTrans).GetComponent<ResItem>();
        item.Init(inf);
        item.gameObject.SetActive(true);
        m_ItemList.Add(item);
    }

    public void Clear()
    {
        foreach (var item in m_ItemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }
        m_ItemList.Clear();
    }
}

/// <summary>
/// 客户端资源更新前会进行检查请求
/// 如果客户端的版本码和服务器的不一致则需要更新
/// </summary>
public class ResourcesInfo : BaseInfo
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