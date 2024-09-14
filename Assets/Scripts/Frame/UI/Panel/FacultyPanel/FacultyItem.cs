
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FacultyItem : MonoBehaviour
{
    public GameObject Id;
    public GameObject Faculty;
    public GameObject ReigsterTime;
    public GameObject TeacherName;
    public Button Revise;
    public Button Delete;

    private FacultyInfo m_info = new FacultyInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("学院信息的删除", "是否删除学院信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            FacPropertyDialog.instance.Init(m_info, PropertyType.PT_FAC_SET);
            FacPropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(FacultyInfo info)
    {
        m_info = info;
        
        Id.GetComponentInChildren<TextMeshProUGUI>().text = info.id;
        Faculty.GetComponentInChildren<TextMeshProUGUI>().text = info.Name;
        ReigsterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        TCPHelper.OperateInfo(m_info, EventType.FacultyEvent, OperateType.DELETE);
    }
}