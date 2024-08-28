using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ColPropertyDialog : BasePanel
{
    public static ColPropertyDialog instance;

    public Button OK;
    public Button Cancel;
    public TMP_InputField ID;
    public TMP_InputField ColumnsName;
    public TMP_InputField RegisterTime;

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
    public void Init(ColumnsInfo info, PropertyType type)
    {
        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[type]);
        m_Action.Init(info);
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(ColumnsInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        ID.text = inf.id;
        ColumnsName.text = inf.Name;
        RegisterTime.text = inf.RegisterTime;
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public ColumnsInfo Output()
    {
        ColumnsInfo inf = new ColumnsInfo
        {
            id = ID.text,
            Name = ColumnsName.text,
            RegisterTime = RegisterTime.text,
        };

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
        ColumnsName.text = "";  
        RegisterTime.text = Tools.GetCurrLocalTime();
    }
}