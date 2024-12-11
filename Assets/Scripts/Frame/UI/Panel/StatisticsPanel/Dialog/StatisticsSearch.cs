using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

public class StatisticsSearch : BasePanel
{
    public TMP_InputField Name;
    public TMP_Dropdown Course;

    public Button searchButton;
    public Button cancelButton;

    public void Start()
    {
        searchButton.onClick.AddListener(() =>
        {
            FieldFilter();
        });

        cancelButton.onClick.AddListener(() => { Close(); });

        Close();
    }

    public override void InitAsync()
    {
        List<string> localCourseList = new List<string>(CourseSelection());
        localCourseList.Insert(0, "нч");
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

    public async void FieldFilter()
    {
        if (Course.value == 0 && Name.text == "") return;
        // if (Course.value > 0)

        await Client.m_Server.HttpRequest(GlobalData.IP + "GetUsrTimeList", "", (ret) =>
        {
            List<UsrTimeInfo> usrTimeInfoList = JsonMapper.ToObject<List<UsrTimeInfo>>(ret);

            Predicate<UsrTimeInfo> match;
            if (Name.text == "") { match = (x) => x.moduleName == Course.options[Course.value].text; }
            else if (Course.value == 0) { match = (x) => x.usrName == Name.text; }
            else { match = (x) => (x.usrName == Name.text && x.moduleName == Course.options[Course.value].text); }

            List<UsrTimeInfo> usrTimeList = usrTimeInfoList.FindAll(match);
            string usrStr = JsonMapper.ToJson(usrTimeList);

            StatisticsPanel panel = UIHelper.FindPanel<StatisticsPanel>();
            panel.Show(usrStr);
            Close();
        }, UnityWebRequest.kHttpVerbGET);
    }

    public override void Close()
    {
        Name.text = "";
        Course.value = 0;

        Active(false);
    }
}