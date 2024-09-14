using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;
using OpenCover.Framework.Model;

public class ClassPropertyDialog : BasePanel
{
    public static ClassPropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField ID;
    public TMP_InputField Class;
    public TMP_InputField RegisterTime;
    public TMP_Dropdown FacultyName;
    public TMP_Dropdown Major;
    public TMP_Dropdown TeacherName;
    public TMP_InputField Number;

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
            if (!UIHelper.InputFieldCheck(Class.text)) { return; }
            m_Action.Action(inf:Output());
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
    public void Init(ClassInfo info, PropertyType type)
    {
        UIHelper.AddDropDownOptions(ref FacultyName, GlobalData.facultiesList);
        UIHelper.AddDropDownOptions(ref Major, GlobalData.majorList);  
        UIHelper.AddDropDownOptions(ref TeacherName, GlobalData.teachersList);

        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);      
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(ClassInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        ID.text = inf.id;
        Class.text = inf.Class;
        RegisterTime.text = inf.RegisterTime;
        FacultyName.value = UIHelper.GetDropDownOptionIndex(FacultyName, inf.Faculty);
        Major.value = UIHelper.GetDropDownOptionIndex(Major, inf.Major);
        TeacherName.value = UIHelper.GetDropDownOptionIndex(TeacherName, inf.Teacher);
        Number.text = inf.Number.ToString();
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public ClassInfo Output()
    {
        ClassInfo inf = new ClassInfo
        {
            id = ID.text,
            Class = Class.text,
            RegisterTime = RegisterTime.text,
            Number = int.Parse(Number.text == "" ? "0" : Number.text),
        };

        if (Tools.checkList(FacultyName.options, FacultyName.value)) 
            inf.Faculty = FacultyName.options[FacultyName.value].text;

        if (Tools.checkList(Major.options, Major.value)) 
            inf.Major = Major.options[Major.value].text;

        if (Tools.checkList(TeacherName.options, TeacherName.value)) 
            inf.Teacher = TeacherName.options[TeacherName.value].text;
        
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
        Class.text = "";
        RegisterTime.text = Tools.GetCurrLocalTime();
        FacultyName.value = 0;
        Major.value = 0;
        TeacherName.value = 0;
        Number.text = "";
    }
}