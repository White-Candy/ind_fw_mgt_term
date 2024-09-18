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
        TCPHelper.OperateInfo(m_info, EventType.ResEvent, OperateType.DELETE);
    }
}