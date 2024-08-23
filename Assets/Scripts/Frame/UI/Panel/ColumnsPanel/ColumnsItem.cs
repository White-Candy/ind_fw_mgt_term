
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColumnsItem : MonoBehaviour
{
    public GameObject Id;
    public GameObject Columns;
    public GameObject ReigsterTime;
    public Button Revise;
    public Button Delete;

    private ColumnsInfo m_info = new ColumnsInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            MessageDialog dialog = DialogHelper.Instance.CreateMessDialog("MessageDialog");
            dialog.Init("栏目信息的删除", "是否删除栏目信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            ColPropertyDialog.instance.Init(m_info, PropertyType.PT_COL_SET);
            ColPropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(ColumnsInfo info)
    {
        m_info = info;
        
        Id.GetComponentInChildren<TextMeshProUGUI>().text = info.id;
        Columns.GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
        ReigsterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        TCPHelper.OperateInfo(m_info, EventType.ColumnsEvent, OperateType.DELETE);
    }
}