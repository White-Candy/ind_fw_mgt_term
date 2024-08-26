using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

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
    private string RegisterTime;
    private PD_BaseAction m_Action;

    public override void Awake()
    {
        base.Awake();

        instance = this;
        Active(false);
    }

    public void Start()
    {
        OK.OnClickAsObservable().Subscribe(_=> 
        {
            m_Action.Action(Output());
            Close();
        });

        Cancel.OnClickAsObservable().Subscribe(_=> 
        {
            Close();
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="info"></param>
    /// <param name="type"></param>
    public void Init(CourseInfo info, PropertyType type)
    {
        UITools.AddDropDownOptions(Columns, GlobalData.columnsList);  

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
        Columns.value = UITools.GetDropDownOptionIndex(Columns, inf.Columns);
        Working.text = inf.Working;
        Content.value = UITools.GetDropDownOptionIndex(Content, inf.Content);
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
            Content = Content.options[Content.value].text,
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
        Content.value = 0;
        RegisterTime = Tools.GetCurrLocalTime();
    }
}