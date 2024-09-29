using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class FacPropertyDialog : BasePanel
{
    // public static FacPropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField ID;
    public TMP_InputField FacultyName;
    public TMP_InputField RegisterTime;
    public TMP_Dropdown TeacherName;

    private PD_BaseAction m_Action;

    public override void Awake()
    {
        base.Awake();

        // instance = this;
        Active(false);
    }

    public void Start()
    {
        OK.OnClickAsObservable().Subscribe(_=> 
        {
            if (!UIHelper.InputFieldCheck(FacultyName.text)) { return; }
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
    public void Init(FacultyInfo info, PropertyType type)
    {
        UIHelper.AddDropDownOptions(ref TeacherName, GlobalData.deanList);

        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(FacultyInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        ID.text = inf.id;
        FacultyName.text = inf.Name;
        RegisterTime.text = inf.RegisterTime;
        TeacherName.value = UIHelper.GetDropDownOptionIndex(TeacherName, inf.TeacherName);
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public FacultyInfo Output()
    {
        FacultyInfo inf = new FacultyInfo
        {
            id = ID.text,
            Name = FacultyName.text,
            RegisterTime = RegisterTime.text,
        };

        if (Tools.checkList(TeacherName.options, TeacherName.value)) 
            inf.TeacherName = TeacherName.options[TeacherName.value].text;

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
        FacultyName.text = "";
        RegisterTime.text = Tools.GetCurrLocalTime();
        TeacherName.value = 0;
    }
}