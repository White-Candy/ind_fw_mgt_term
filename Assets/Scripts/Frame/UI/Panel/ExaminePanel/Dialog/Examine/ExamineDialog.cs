using System.Collections.Generic;
using System.Xml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExamineDialog : BasePanel
{
    public TMP_Dropdown columns;
    public TMP_Dropdown course;
    public TMP_InputField trainingScore;
    public TMP_InputField singleNumber;
    public TMP_InputField mulitNumber;
    public TMP_InputField tofNumber;

    public Button single;
    public Button muilt;
    public Button tof;

    public Button okButton;
    public Button cancelButton;

    public SinglePanel singlePanel;
    public MulitPanel mulitPanel;
    public TOFPanel tofPanel;

    private PD_BaseAction m_Action;
    public ExamineInfo m_info = new ExamineInfo();

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        columns.onValueChanged.AddListener((i) => 
        {
            CourseSelection(columns.options[columns.value].text);
        });

        single.OnClickAsObservable().Subscribe(_ =>
        {
            singlePanel.Active(true);
            mulitPanel.Active(false);
            tofPanel.Active(false);
        });

        muilt.OnClickAsObservable().Subscribe(_ =>
        {
            singlePanel.Active(false);
            mulitPanel.Active(true);
            tofPanel.Active(false);
        });

        tof.OnClickAsObservable().Subscribe(_ =>
        {
            singlePanel.Active(false);
            mulitPanel.Active(false);
            tofPanel.Active(true);
        });

        singleNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            int.TryParse(text, out number);
            singlePanel.Init(number);
        });

        mulitNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            int.TryParse(text, out number);
            mulitPanel.Init(number);
        });

        tofNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            int.TryParse(text, out number);
            tofPanel.Init(number);

        });

        okButton.OnClickAsObservable().Subscribe(_=> 
        {
            m_Action.Action(inf:Output());
            Close();
        });

        cancelButton.OnClickAsObservable().Subscribe(_=> 
        {
            Close();
        });        

        // 默认是单选题的界面
        singlePanel.Active(true);
        mulitPanel.Active(false);
        tofPanel.Active(false);

        Active(false);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="inf"></param>
    public void Init(ExamineInfo inf, PropertyType propertyType)
    {
        UIHelper.AddDropDownOptions(ref columns, GlobalData.columnsList);
        CourseSelection(columns.options[0].text);

        m_info = inf;
        m_Action = Tools.CreateObject<PD_BaseAction>(GlobalData.m_Enum2Type[propertyType]);
        m_Action.Init(m_info);        
    }

    /// <summary>
    /// 信息装填
    /// </summary>
    /// <param name="inf"></param>
    public void Loading(ExamineInfo inf)
    {
        UIHelper.SetDropDown(ref columns, inf.ColumnsName);
        UIHelper.SetDropDown(ref course, inf.CourseName);

        trainingScore.text = inf.TrainingScore.ToString();
        singleNumber.text = inf.SingleNum.ToString();
        mulitNumber.text = inf.MulitNum.ToString();
        tofNumber.text = inf.TOFNum.ToString();

        singlePanel.Init(inf.SingleChoices);
        mulitPanel.Init(inf.MulitChoices);
        tofPanel.Init(inf.TOFChoices);
    }

    /// <summary>
    /// 信息打包
    /// </summary>
    /// <returns></returns>
    public ExamineInfo Output()
    {
        ExamineInfo inf = new ExamineInfo()
        {
            TrainingScore = int.Parse(trainingScore.text),
            SingleNum = int.Parse(singleNumber.text),
            MulitNum = int.Parse(mulitNumber.text),
            TOFNum = int.Parse(tofNumber.text),
            SingleChoices = singlePanel.Output(),
            MulitChoices = mulitPanel.Output(),
            TOFChoices = tofPanel.Output()
        };

        if (columns.value >= 0)
            inf.ColumnsName = columns.options[columns.value].text;

        if (course.value >= 0)
            inf.CourseName = course.options[course.value].text;
        
        return inf;
    }
    
    /// <summary>
    /// 课程筛选
    /// </summary>
    /// <param name="colName"></param>
    public void CourseSelection(string colName)
    {
        course.ClearOptions();
        List<string> courseList = new List<string>();
        foreach (var inf in GlobalData.coursesList)
        {
            if (inf.Columns == colName)
                courseList.Add(inf.CourseName);
        }
        course.AddOptions(courseList);
    } 

    /// <summary>
    /// 关闭
    /// </summary>
    public override void Close()
    {
        Clear();
        Active(false);
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        columns.value = 0;
        course.value = 0;
        trainingScore.text = "";
        singleNumber.text = "";
        mulitNumber.text = "";
        tofNumber.text = "";

        singlePanel.Clear();
        mulitPanel.Clear();
        tofPanel.Clear();
    }
}