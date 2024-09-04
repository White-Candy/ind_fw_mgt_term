
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using TMPro;
using UnityEngine;

public class TrainingPanel : BasePanel
{
    public TMP_Dropdown Columns;
    public TrainingCourseItem m_Item;
    public Transform m_Parent;
    private List<TrainingCourseItem> m_itemList = new List<TrainingCourseItem>();
    public static TrainingPanel inst;

    public override void Awake()
    {
        base.Awake();
        inst = this;
    }

    public void Start()
    {
        Columns.onValueChanged.AddListener((i) => 
        {
            string colName = Columns.options[Columns.value].text;
            CourseSelection(colName);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public async void Init(string courseName)
    {
        await UniTask.WaitUntil(() => { return GlobalData.columnsList.Count == 0; });

        Columns.ClearOptions();
        Columns.AddOptions(GlobalData.columnsList);

        int i = GlobalData.coursesList.FindIndex(x => x.CourseName == courseName);  
        string colName = GlobalData.coursesList[i].Columns;
        SetColumns(colName);  
        CourseSelection(colName);
    }

    /// <summary>
    /// 课程筛选
    /// </summary>
    /// <param name="colName"></param>
    public void CourseSelection(string colName)
    {   
        Save();
        Clear();

        foreach (var inf in ExaminePanel.m_ExamineesInfo)
        {
            if (inf.ColumnsName == colName)
            {
                TrainingCourseItem item = Instantiate(m_Item, m_Parent);
                item.Init(inf);
                m_itemList.Add(item);
            }
        }
    }

    public void SetColumns(string colName)
    {
        int i = Columns.options.FindIndex(x => x.text == colName);
        Columns.value = i;
    }

    public void Save()
    {
        foreach (var item in m_itemList)
        {
            int i = ExaminePanel.m_ExamineesInfo.FindIndex(x => x.CourseName == item.name);
            if (-1 != i)
            {
                int score = 0;
                int.TryParse(item.m_score.text, out score);
                ExaminePanel.m_ExamineesInfo[i].TrainingScore = score;
                ExaminePanel.m_ExamineesInfo[i].Status = item.m_toggle.isOn;
            }
        }
    }

    /// <summary>
    /// Item List清空
    /// </summary>
    public void Clear()
    {
        foreach (var item in m_itemList)
        {
            item.Clear();
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        m_itemList.Clear();
    }
}