using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

public class MajorPropertyDialog : BasePanel
{
    public static MajorPropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField ID;
    public TMP_InputField MajorName;
    public TMP_InputField RegisterTime;
    public TMP_Dropdown FacultyName;
    public TMP_Dropdown TeacherName;

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
    public void Init(MajorInfo info, PropertyType type)
    {
        FacultyName.AddOptions(GlobalData.facultiesList);
        TeacherName.AddOptions(GlobalData.teachersList);

        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);      
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(MajorInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        ID.text = inf.id;
        MajorName.text = inf.MajorName;
        FacultyName.value = UITools.GetDropDownOptionIndex(FacultyName, inf.FacultyName);
        RegisterTime.text = inf.RegisterTime;
        TeacherName.value = UITools.GetDropDownOptionIndex(TeacherName, inf.TeacherName);
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public MajorInfo Output()
    {
        MajorInfo inf = new MajorInfo
        {
            id = ID.text,
            MajorName = MajorName.text,
            FacultyName = FacultyName.options[FacultyName.value].text,
            RegisterTime = RegisterTime.text,
            TeacherName = TeacherName.options[TeacherName.value].text
        };
        
        return inf;
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
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
        MajorName.text = "";
        FacultyName.value = 0;
        RegisterTime.text = Tools.GetCurrLocalTime();
        TeacherName.value = 0;
    }
}