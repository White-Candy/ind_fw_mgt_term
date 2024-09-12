
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivationDialog : BasePanel
{
    public TMP_Dropdown columns;
    public ActivationItem m_item;
    public Transform m_Parent;
    public Button okButton;
    public Button cancelButton;

    private List<ExamineInfo> m_list = new List<ExamineInfo>();
    private List<ActivationItem> m_itemList = new List<ActivationItem>();

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        columns.onValueChanged.AddListener((i) => 
        {
            string colName = columns.options[columns.value].text;
            CourseSelection(colName);
        });

        okButton.onClick.AddListener(() => 
        {
            Save();
            TCPHelper.OperateInfo(m_list, EventType.ExamineEvent, OperateType.REVISE);
            Close();
        });

        cancelButton.onClick.AddListener(() => 
        {
            Close();
        });

        Active(false);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public async override void Init()
    {
        m_list = ExaminePanel.m_examineesInfo;
        await UniTask.WaitUntil(() => { return GlobalData.columnsList.Count != 0; });

        UIHelper.AddDropDownOptions(ref columns, GlobalData.columnsList);

        columns.value = 0;
        string colName = columns.options[0].text;
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
        Debug.Log($"target Columns Name: {colName} | mlist Length: {m_list.Count}.");
        foreach (var inf in m_list)
        {
            if (inf.ColumnsName == colName)
            {
                ActivationItem item = Instantiate(m_item, m_Parent);
                item.Init(inf);
                m_itemList.Add(item);
            }
        }
    }

    public void Save()
    {
        foreach (var item in m_itemList)
        {
            int i = m_list.FindIndex(x => x.CourseName == item.courseName.text && x.RegisterTime == item.registerTime.text);
            if (-1 != i)
            {
                m_list[i].Status = item.toggle.isOn;
            }
        }
    }

    public override void Close()
    {
        Clear();
        m_list.Clear();
        Active(false);
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
        //m_list.Clear();
    }
}