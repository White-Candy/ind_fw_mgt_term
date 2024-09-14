
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExamineItem : MonoBehaviour
{
    public GameObject ID;
    public GameObject Course;
    public GameObject Register;
    public GameObject Status;
    public GameObject PNum;
    //public Button Revise;
    public Button Delete;

    private ExamineInfo m_info = new ExamineInfo();
    private ExamineDialog m_examineDialog;

    public void Start()
    {
        m_examineDialog = UIHelper.FindPanel<ExamineDialog>();
        // Revise.OnClickAsObservable().Subscribe(_ => 
        // {
        //     m_examineDialog.Init(m_info, PropertyType.PT_EXA_SET); // Dialog窗口打开默认是 理论窗口
        //     m_examineDialog.Active(true);            
        // });

        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("考核信息的删除", "是否删除考核信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });
    }

    public void Init(ExamineInfo inf)
    {
        m_info = inf;

        ID.GetComponentInChildren<TextMeshProUGUI>().text = inf.id;
        Course.GetComponentInChildren<TextMeshProUGUI>().text = inf.CourseName;
        Register.GetComponentInChildren<TextMeshProUGUI>().text = inf.RegisterTime;
        Status.GetComponentInChildren<TextMeshProUGUI>().text = inf.Status ? "开启" : "关闭";
        PNum.GetComponentInChildren<TextMeshProUGUI>().text = inf.PNum.ToString();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    {
        TCPHelper.OperateInfo(m_info, EventType.ExamineEvent, OperateType.DELETE);
    }    
}