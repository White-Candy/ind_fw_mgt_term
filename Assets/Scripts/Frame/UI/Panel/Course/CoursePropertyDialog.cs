using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;
using System.Collections.Generic;

public class CoursePropertyDialog : BasePanel
{
    public static CoursePropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField ID;
    public TMP_InputField Course;
    public TMP_Dropdown Columns;
    public TMP_InputField Working;
    public TMP_Dropdown Content;

    // 教学资源下的按钮
    private Button JiaoAn;
    private Button TuPian;
    private Button ShiPin;
    private Button DongHua;
    private Button TuZhi;
    private Button FangAn;
    private Button GuiFan;
    private string RegisterTime;
    private PD_BaseAction m_Action;

    private List<FilePackage> m_filesInfo = new List<FilePackage>();

    public override void Awake()
    {
        base.Awake();

        JiaoAn = UIHelper.GetComponentInScene<Button>("JiaoAn");
        TuPian = UIHelper.GetComponentInScene<Button>("TuPian");
        ShiPin = UIHelper.GetComponentInScene<Button>("ShiPin");
        DongHua = UIHelper.GetComponentInScene<Button>("DongHua");
        TuZhi = UIHelper.GetComponentInScene<Button>("TuZhi");
        FangAn = UIHelper.GetComponentInScene<Button>("FangAn");
        GuiFan = UIHelper.GetComponentInScene<Button>("GuiFan");

        instance = this;
        Active(false);
    }

    public void Start()
    {
        OK.OnClickAsObservable().Subscribe(_=> 
        {
            if (!UIHelper.InputFieldCheck(Course.text) || !UIHelper.InputFieldCheck(Working.text)) return;
            m_Action.Action(() => 
            {
                string colName = Columns.options[Columns.value].text;
                string courseName = Course.text;
                string relativePath = $"{colName}\\{courseName}\\";
                for (int i = 0; i < m_filesInfo.Count; ++i)
                {
                    string fileName = m_filesInfo[i].fileName;
                    m_filesInfo[i].relativePath = relativePath + fileName;
                }

                TCPHelper.UploadFile(m_filesInfo);
                m_filesInfo.Clear();
            }, inf:Output());
            Close();
        });

        Cancel.OnClickAsObservable().Subscribe(_=> 
        {
            Close();
        });

        JiaoAn.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "PDF文件(*.pdf)" + '\0' + "*.pdf\0\0", "PDF", "JiaoAn");
        });

        TuPian.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "PNG文件(*.png)" + '\0' + "*.png\0\0", "PNG", "Picture");
        });

        ShiPin.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "MP4文件(*.mp4)" + '\0' + "*.mp4\0\0", "MP4", "Video");
        });

        DongHua.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "MP4文件(*.mp4)" + '\0' + "*.mp4\0\0", "MP4", "Animation");
        });

        TuZhi.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "PDF文件(*.pdf)" + '\0' + "*.pdf\0\0", "PDF", "TuZhi");
        });

        FangAn.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "PDF文件(*.pdf)" + '\0' + "*.pdf\0\0", "PDF", "FangAn");
        });

        GuiFan.OnClickAsObservable().Subscribe(_ => 
        {
            FileHelper.ResourcesFileLoad(ref m_filesInfo, "PDF文件(*.pdf)" + '\0' + "*.pdf\0\0", "PDF", "GuiFan");
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    /// <param name="type"></param>
    public void Init(CourseInfo info, PropertyType type)
    {
        UIHelper.AddDropDownOptions(ref Columns, GlobalData.columnsList);  

        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);      
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(CourseInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        ID.text = inf.id;
        Course.text = inf.CourseName;
        Columns.value = UIHelper.GetDropDownOptionIndex(Columns, inf.Columns);
        Working.text = inf.Working;
        RegisterTime = inf.RegisterTime;
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public CourseInfo Output()
    {
        CourseInfo inf = new CourseInfo
        {
            id = ID.text,
            CourseName = Course.text,
            Working = Working.text,
            RegisterTime = RegisterTime
        };
        //Debug.Log("Course OutPut Info Register Time: " + inf.RegisterTime);
        if (Tools.checkList(Columns.options, Columns.value)) 
            inf.Columns = Columns.options[Columns.value].text;    

        return inf;
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public override void Close()
    {
        Active(false);
        Clear();
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        ID.text = "";
        Course.text = "";
        Columns.value = 0;
        Working.text = "";
        RegisterTime = Tools.GetCurrLocalTime();
    }
}