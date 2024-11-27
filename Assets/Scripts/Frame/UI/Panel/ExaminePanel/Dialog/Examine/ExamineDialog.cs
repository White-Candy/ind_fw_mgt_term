using System.Collections.Generic;
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
    public TMP_InputField theoryTime;
    public TMP_InputField trainingTime;

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
        columns.onValueChanged.AddListener((i) => { CourseSelection(columns.options[columns.value].text); });

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
            if (!InputFieldCheck() || !singlePanel.InputFieldCheck() || !mulitPanel.InputFieldCheck() || !tofPanel.InputFieldCheck()) 
            {
                UIHelper.ShowMessage("内容不可为空", "内容不可为空", new ItemPackage("确定", () => {}));
                return; 
            }
            m_Action.Action(inf:Output());
            Close();
        });

        cancelButton.OnClickAsObservable().Subscribe(_=> { Close(); });

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
        if (columns.options.Count > 0) CourseSelection(columns.options[0].text);

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
        theoryTime.text = inf.TheoryTime;
        trainingTime.text = inf.TrainingTime;

        singlePanel.Init(inf.SingleChoices);
        mulitPanel.Init(inf.MulitChoices);
        tofPanel.Init(inf.TOFChoices);
    }

    /// <summary>
    /// 信息打包
    /// </summary>
    /// <returns></returns>
    public List<ExamineInfo> Output()
    {
        List<ExamineInfo> list = new List<ExamineInfo>();
        ExamineInfo inf = new ExamineInfo();

            inf.id = m_info?.id;
            inf.RegisterTime = m_info?.RegisterTime;
            inf.TrainingScore = trainingScore.text;
            inf.SingleNum = int.Parse(singleNumber.text);
            inf.MulitNum = int.Parse(mulitNumber.text);
            inf.TOFNum = int.Parse(tofNumber.text);
            inf.TheoryTime = theoryTime.text;
            inf.TrainingTime = trainingTime.text;
            inf.SingleChoices = singlePanel.Output();
            inf.MulitChoices = mulitPanel.Output();
            inf.TOFChoices = tofPanel.Output();

        if (columns.value >= 0)
            inf.ColumnsName = columns.options[columns.value].text;

        if (course.value >= 0)
            inf.CourseName = course.options[course.value].text;
        list.Add(inf);
        return list;
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

    public bool InputFieldCheck()
    {
        if (!(ValidateHelper.IsNumberPosInt(trainingScore.text) && ValidateHelper.IsNumberPosInt(singleNumber.text) && ValidateHelper.IsNumberPosInt(mulitNumber.text)
        && ValidateHelper.IsNumberPosInt(tofNumber.text) && ValidateHelper.IsNumberPosInt(theoryTime.text) && ValidateHelper.IsNumberPosInt(trainingTime.text)))
        {
            UIHelper.ShowMessage("格式错误", "格式错误", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
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