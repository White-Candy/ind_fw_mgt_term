using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ClassItem : MonoBehaviour
{
    public GameObject Id;
    public GameObject Class;
    public GameObject Major;
    public GameObject ReigsterTime;
    public GameObject Faculty;
    public GameObject Teacher;
    public GameObject Number;
    public Button Revise;
    public Button Delete;

    private ClassInfo m_info = new ClassInfo();

    public void Start()
    {
        Delete.OnClickAsObservable().Subscribe(_ => 
        {
            DialogHelper helper = new DialogHelper();
            MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
            dialog.Show("班级信息的删除", "是否删除该班级信息？", new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null));    
        });

        Revise.OnClickAsObservable().Subscribe(_ => 
        {
            ClassPropertyDialog classProDialog = UIHelper.FindPanel<ClassPropertyDialog>();
            classProDialog.Init(m_info, PropertyType.PT_CLASS_SET);
            classProDialog.Active(true);

            // ClassPropertyDialog.instance.Init(m_info, PropertyType.PT_CLASS_SET);
            // ClassPropertyDialog.instance.Active(true);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    public void Init(ClassInfo info)
    {
        m_info = info;
        
        Id.GetComponentInChildren<TextMeshProUGUI>().text = info.id;
        Class.GetComponentInChildren<TextMeshProUGUI>().text = info.Class;
        Major.GetComponentInChildren<TextMeshProUGUI>().text = info.Major;
        ReigsterTime.GetComponentInChildren<TextMeshProUGUI>().text = info.RegisterTime;
        Faculty.GetComponentInChildren<TextMeshProUGUI>().text = info.Faculty;
        Teacher.GetComponentInChildren<TextMeshProUGUI>().text = info.Teacher;
        Number.GetComponentInChildren<TextMeshProUGUI>().text = info.Number.ToString();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认删除
    /// </summary>
    public void ConfirmDelete()
    { 
        // TODO...CHECK
        NetHelper.OperateInfo(m_info, EventType.ClassEvent, OperateType.DELETE);
    }
}