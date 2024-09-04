using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;
using System;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;

public class ExamineDialog : BasePanel
{
    public static ExamineDialog instance;

    public Button OK;
    public Button Cancel;
    public Button Theory;
    public Button Training;
    public ExamineInfo m_info = new ExamineInfo();
    private PD_BaseAction m_Action;
    private PropertyType m_PropertyType;

    public override void Awake()
    {
        base.Awake();

        instance = this;
        Active(false);

        // 默认显示理论界面
        TheoryPanel.inst.Active(true);
        TrainingPanel.inst.Active(false);
    }

    public void Start()
    {
        Theory.OnClickAsObservable().Subscribe(_ =>
        {
            TheoryPanel.inst.Active(true);
            TrainingPanel.inst.Active(false);
        });

        Training.OnClickAsObservable().Subscribe(_ =>
        {
            TheoryPanel.inst.Active(false);
            TrainingPanel.inst.Active(true);
        });

        OK.OnClickAsObservable().Subscribe(_=> 
        {
            TrainingPanel.inst.Save();
            m_Action.Action(inf:ExaminePanel.m_ExamineesInfo);
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
    public void Init(ExamineInfo info, PropertyType propertyType)
    {
        // UIHelper.AddDropDownOptions(FacultyName, GlobalData.facultiesList);  
        // UIHelper.AddDropDownOptions(TeacherName, GlobalData.directorsList);

        m_PropertyType = propertyType;
        m_info = info == null ? m_info : info;
        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[m_PropertyType]);
        m_Action.Init(info);
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(ExamineInfo inf)
    {
        TheoryPanel.inst.Init(inf);
        TrainingPanel.inst.Init(inf.CourseName);
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
        TheoryPanel.inst.Clear();
        TrainingPanel.inst.Clear();
    }
}