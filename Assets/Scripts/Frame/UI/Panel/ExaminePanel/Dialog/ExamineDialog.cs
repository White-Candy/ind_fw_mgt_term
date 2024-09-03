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
    // public TheoryPanel theoryPanel;
    // public TrainingPanel trainingPanel;

    public ExamineInfo m_info;
    private PD_BaseAction m_Action;
    private PropertyType m_PropertyType;
    private ActionType m_actionType;

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

            m_PropertyType = m_actionType == ActionType.ADD ? PropertyType.PT_THE_ADDTO : PropertyType.PT_THE_SET;
            m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[m_PropertyType]);
            m_Action.Init(m_info);
        });

        Training.OnClickAsObservable().Subscribe(_ =>
        {
            TheoryPanel.inst.Active(false);
            TrainingPanel.inst.Active(true);

            m_PropertyType = m_actionType == ActionType.ADD ? PropertyType.PT_TRA_ADDTO : PropertyType.PT_TRA_SET;
            m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[m_PropertyType]);
            m_Action.Init(m_info);
        });

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
    public void Init(ExamineInfo info, PropertyType propertyType, ActionType actionType)
    {
        // UIHelper.AddDropDownOptions(FacultyName, GlobalData.facultiesList);  
        // UIHelper.AddDropDownOptions(TeacherName, GlobalData.directorsList);

        m_actionType = actionType;
        m_PropertyType = propertyType;
        m_info = info;
        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[m_PropertyType]);
        m_Action.Init(info);
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    public void Loading(MajorInfo inf)
    {
        // Debug.Log($"{inf.Name} || {inf.RegisterTime} || {inf.TeacherName}");
        // ID.text = inf.id;
        // MajorName.text = inf.MajorName;
        // FacultyName.value = UIHelper.GetDropDownOptionIndex(FacultyName, inf.FacultyName);
        // RegisterTime.text = inf.RegisterTime;
        // TeacherName.value = UIHelper.GetDropDownOptionIndex(TeacherName, inf.TeacherName);
    }

    /// <summary>
    /// 将目前界面上的内容信息存储到服务器中。
    /// </summary>
    /// <returns></returns>
    public ExamineInfo Output()
    {
        ExamineInfo inf = new ExamineInfo()
        {           
            Status = TrainingPanel.inst.m_Item.m_Toggle.isOn,
            ClassNum = 0,
            SingleNum = int.Parse(TheoryPanel.inst.SingleNumber.text),
            MulitNum = int.Parse(TheoryPanel.inst.MulitNumber.text),
            TOFNum = int.Parse(TheoryPanel.inst.TOFNumber.text),
            SingleChoices = SinglePanel.inst.Output(),
        };

        if (Tools.checkList(TheoryPanel.inst.Columns.options, TheoryPanel.inst.Columns.value)) 
            inf.ColumnsName = TheoryPanel.inst.Columns.options[TheoryPanel.inst.Columns.value].text;
            
        if (Tools.checkList(TheoryPanel.inst.Course.options, TheoryPanel.inst.Course.value)) 
            inf.CourseName = TheoryPanel.inst.Course.options[TheoryPanel.inst.Course.value].text;

        return new ExamineInfo();
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
        
    }
}

/// <summary>
/// 动作类型 Revise Or Add
/// </summary>
public enum ActionType
{
    NONE, ADD, REVISE
}