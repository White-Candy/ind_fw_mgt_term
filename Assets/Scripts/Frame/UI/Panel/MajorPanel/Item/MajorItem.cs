
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MajorItem : MonoBehaviour
{
    public GameObject Id;
    public GameObject Major;
    public GameObject ReigsterTime;
    public GameObject FacultyName;
    public GameObject TeacherName;
    public Button Revise;
    public Button Delete;

    private MajorInfo m_info = new MajorInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            MessageDialog dialog = DialogHelper.Instance.CreateMessDialog("MessageDialog");
            dialog.Init("专业信息的删除", "是否删除该专业信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            MajorPropertyDialog.instance.Init(m_info, PropertyType.PT_MAJ_SET);
            MajorPropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(MajorInfo info)
    {
        m_info = info;
        
        Id.GetComponentInChildren<TextMeshProUGUI>().text = info.id;
        Major.GetComponentInChildren<TextMeshProUGUI>().text = info.MajorName;
        ReigsterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;
        FacultyName.GetComponentInChildren<TextMeshProUGUI>().text = info.FacultyName;
        TeacherName.GetComponentInChildren<TextMeshProUGUI>().text = info.TeacherName;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    { 
        TCPHelper.OperateInfo(m_info, EventType.MajorEvent, OperateType.DELETE);
    }
}