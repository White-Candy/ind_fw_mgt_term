
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;

public class SearchDialog : BasePanel
{
    public Button Search;
    public Button Cancel;
    public TMP_InputField Name;
    public TMP_Dropdown Class;
    public TMP_Dropdown Course;
    public TMP_InputField Year;
    public TMP_InputField Month;
    public TMP_InputField Day;

    public void Start()
    {
        Search.OnClickAsObservable().Subscribe(_ => 
        {
            //if (!InputFieldCheck()) return;

            string usrName = "", _className = "", _courseName = "", _registerTime = "";
            if (Name.text.Count() > 0) { usrName = Name.text; }
            if (Class.value > 0) { _className = Class.options[Class.value].text; }
            if (Course.value > 0) { _courseName = Course.options[Course.value].text; }
            if (Year.text.Count() > 0 && Year.text.Count() > 0 && Year.text.Count() > 0) { _registerTime = $"{Year.text}/{Month.text}/{Day.text}"; }
            ScoreInfo scoreInf = new ScoreInfo()
            {
                Name = usrName, className = _className,
                courseName = _courseName, registerTime = _registerTime
            };

            TCPHelper.OperateInfo(scoreInf, EventType.ScoreEvent, OperateType.SEARCH);
            Close();
        });

        Cancel.OnClickAsObservable().Subscribe(_ => { Close(); });

        Close();
    }

    public bool InputFieldCheck()
    {
        if (!UIHelper.InputFieldCheck(Name.text) || !UIHelper.InputFieldCheck(Year.text) || !UIHelper.InputFieldCheck(Month.text)
            || !UIHelper.InputFieldCheck(Day.text)) return false;
        return true;
    }

    public override void Init()
    {
        List<string> localClassList = new List<string>(GlobalData.classesList);
        List<string> localCourseList = new List<string>(CourseSelection());
        localClassList.Insert(0, "无");
        localCourseList.Insert(0, "无");

        UIHelper.AddDropDownOptions(ref Class, localClassList);
        UIHelper.AddDropDownOptions(ref Course, localCourseList);
    }

    public List<string> CourseSelection()
    {
        List<string> courseNameList = new List<string>();
        for (int i = 0; i < GlobalData.coursesList.Count; ++i) 
        { 
            CourseInfo courseInf = GlobalData.coursesList[i];
            courseNameList.Add(courseInf.CourseName);
        }
        return courseNameList;
    }

    public override void Close()
    {
        Name.text = "";
        Class.value = 0;
        Course.value = 0;

        Year.text = "";
        Month.text = "";
        Day.text = "";
        Active(false);
    }
}