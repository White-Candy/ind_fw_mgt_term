using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class FacPropertyDialog : BasePanel
{
    public static FacPropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField FacultyName;
    public TMP_InputField RegisterTime;
    public TMP_InputField TeacherName;

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
    public void Init(FacultyInfo info, PropertyType type)
    {
        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(FacultyInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        FacultyName.text = inf.Name;
        RegisterTime.text = inf.RegisterTime;
        TeacherName.text = inf.TeacherName;
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public FacultyInfo Output()
    {
        FacultyInfo inf = new FacultyInfo
        {
            Name = FacultyName.text,
            RegisterTime = RegisterTime.text,
            TeacherName = TeacherName.text
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
        FacultyName.text = "";
        RegisterTime.text = Tools.GetCurrLocalTime();
        TeacherName.text = "";
    }
}