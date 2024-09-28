using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResItem : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public Button Delete;
    
    private ResourcesInfo m_info = new ResourcesInfo();

    public void Start()
    {
        Delete.onClick.AddListener(DeleteResourceReq);
    }

    public void Init(ResourcesInfo inf)
    {   
        m_info = new ResourcesInfo(inf);
        Name.text = m_info.relaPath;
    }

    public void DeleteResourceReq()
    {
        UIHelper.ShowMessage("资源删除", "是否删除该资源文件?", new ItemPackage("确定", () => 
        {
            NetHelper.OperateInfo(m_info, EventType.ResEvent, OperateType.DELETE);
        }), new ItemPackage("取消", null));
    }
}