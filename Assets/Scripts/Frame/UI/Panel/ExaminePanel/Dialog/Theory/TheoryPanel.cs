using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TheoryPanel : BasePanel
{
    public TMP_Dropdown Columns;
    public TMP_Dropdown Course;
    public TMP_InputField SingleNumber;
    public TMP_InputField MulitNumber;
    public TMP_InputField TOFNumber;
    public Button Single;
    public Button Muilt;
    public Button TOF;
    public SinglePanel m_SinglePanel;
    public MulitPanel m_MulitPanel;
    public TOFPanel m_TOFPanel;
    
    private int colVal = 0;
    private int courseVal = 0;

    public static TheoryPanel inst;

    public override void Awake()
    {
        inst = this;

        // 默认是单选题的界面
        m_SinglePanel.Active(true);
        m_MulitPanel.Active(false);
        m_TOFPanel.Active(false);
    }

    public void Start()
    {
        Columns.onValueChanged.AddListener((i) => 
        {
            CourseSelection(Columns.options[Columns.value].text);
        });

        Single.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(true);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(false);
        });

        Muilt.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(true);
            m_TOFPanel.Active(false);
        });

        TOF.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(true);
        });

        SingleNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            bool result = int.TryParse(text, out number);
            if (result)
            {
                SinglePanel.inst.Init(number);
            }
        });

        MulitNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            bool result = int.TryParse(text, out number);
            if (result)
            {
                MulitPanel.inst.Init(number);
            }
        });


        TOFNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            bool result = int.TryParse(text, out number);
            if (result)
            {
                TOFPanel.inst.Init(number);
            }
        });
    }

    /// <summary>
    /// 装填
    /// </summary>
    /// <param name="inf"></param>
    public void Init(ExamineInfo inf)
    {
        Columns.AddOptions(GlobalData.columnsList);
        CourseSelection(Columns.options[Columns.value].text);

        SetDropDown(ref Columns, inf.ColumnsName);
        SetDropDown(ref Course, inf.CourseName);

        colVal = Columns.value;
        courseVal = Course.value;

        m_SinglePanel.Init(inf.SingleChoices);
        m_MulitPanel.Init(inf.MulitChoices);
        m_TOFPanel.Init(inf.TOFChoices);
    }

    /// <summary>
    /// 课程筛选
    /// </summary>
    /// <param name="colName"></param>
    public void CourseSelection(string colName)
    {
        Course.ClearOptions();
        List<string> courseList = new List<string>();
        foreach (var inf in GlobalData.coursesList)
        {
            if (inf.Columns == colName)
                courseList.Add(inf.CourseName);
        }
        Course.AddOptions(courseList);
    }

    /// <summary>
    /// 保存
    /// </summary>
    public void Save()
    {
        string oldColName = Columns.options[colVal].text;
        string oldCourseName = Course.options[courseVal].text;

        int i = ExaminePanel.m_ExamineesInfo.FindIndex(x => x.CourseName == oldCourseName && x.ColumnsName == oldColName);
        if (-1 != i)
        {
            // TODO... Theory Save as Examine
            // ExaminePanel.m_ExamineesInfo[i].SingleNum = intSingleNumber;
        }
    }

    public void SetDropDown(ref TMP_Dropdown dropdown, string text)
    {
        int i = dropdown.options.FindIndex(x => x.text == text);
        if (i != -1)
        {
            dropdown.value = i;
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        Columns.ClearOptions();
        Course.ClearOptions();

        Columns.value = 0;
        Course.value = 0;
        SingleNumber.text = "";
        MulitNumber.text = "";
        TOFNumber.text = "";

        SinglePanel.inst.Clear();
        MulitPanel.inst.Clear();
        TOFPanel.inst.Clear();
    }
}