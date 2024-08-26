using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CourseItem : MonoBehaviour
{
    public GameObject Id;
    public GameObject Course;
    public GameObject RegisterTime;
    public GameObject Columns;
    public Button Revise;
    public Button Delete;

    private CourseInfo m_info = new CourseInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Init("课程信息的删除", "是否删除该课程信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            CoursePropertyDialog.instance.Init(m_info, PropertyType.PT_COR_SET);
            CoursePropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(CourseInfo info)
    {
        m_info = info;
        
        Id.GetComponentInChildren<TextMeshProUGUI>().text = info.id;
        Course.GetComponentInChildren<TextMeshProUGUI>().text = info.CourseName;
        Columns.GetComponentInChildren<TextMeshProUGUI>().text = info.Columns;
        RegisterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    { 
        TCPHelper.OperateInfo(m_info, EventType.CourseEvent, OperateType.DELETE);
    }
}