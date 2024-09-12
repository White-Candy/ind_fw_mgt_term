
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

public class SearchDialog : BasePanel
{
    public Button Search;
    public Button Cancel;
    public TMP_Dropdown Class;
    public TMP_Dropdown Columns;
    public TMP_Dropdown Course;
    public TMP_InputField Year;
    public TMP_InputField Month;
    public TMP_InputField Day;

    public void Start()
    {
        Search.OnClickAsObservable().Subscribe(_ => 
        {
            ScoreInfo scoreInf = new ScoreInfo()
            {
                className = Class.options.Count > 0 ? Class.options[Class.value].text : "",
                columnsName = Columns.options.Count > 0 ? Columns.options[Columns.value].text : "",
                courseName = Course.options.Count > 0 ? Course.options[Course.value].text : "",
                registerTime = $"{Year.text}/{Month.text}/{Day.text}"
            };

            TCPHelper.OperateInfo(scoreInf, EventType.ScoreEvent, OperateType.SEARCH);
            Close();
        });

        Cancel.OnClickAsObservable().Subscribe(_ => { Close(); });

        Columns.onValueChanged.AddListener((i) => 
        {
            UIHelper.AddDropDownOptions(ref Course, CourseSelection());
        });

        Close();
    }

    public override void Init()
    {
        UIHelper.AddDropDownOptions(ref Class, GlobalData.classesList);
        UIHelper.AddDropDownOptions(ref Columns, GlobalData.columnsList);
        UIHelper.AddDropDownOptions(ref Course, CourseSelection());
    }

    public List<string> CourseSelection()
    {
        string colName = Columns.options.Count > 0 ? Columns.options[Columns.value].text : "";
        List<string> courseNameList = new List<string>();
        for (int i = 0; i < GlobalData.coursesList.Count && Columns.options.Count > 0; ++i) 
        { 
            CourseInfo courseInf = GlobalData.coursesList[i];
            if (colName == courseInf.Columns)
                courseNameList.Add(courseInf.CourseName); 
        }
        return courseNameList;
    }

    public override void Close()
    {
        Class.value = 0;
        Columns.value = 0;
        Course.value = 0;

        Year.text = "0";
        Month.text = "0";
        Day.text = "0";

        Active(false);
    }
}