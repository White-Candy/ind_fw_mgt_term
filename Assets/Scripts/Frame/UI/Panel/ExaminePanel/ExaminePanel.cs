/*
致给后续的开发者: 
Hey! I'm the original developer. 
    When you see this text, it means you have started to take over what I wrote.
 Please don't complain about the various problems in the code structure. 
 I feel very sorry, because the project is really tight and my ability is limited. 
 I can't think of a good code structure in a short time.
*/
using System;
using System.Collections.Generic;
using LitJson;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExaminePanel : BasePanel
{    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    public Button AddTo;

    public static List<ExamineInfo> m_ExamineesInfo = new List<ExamineInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();

        Active(false);
    }
    
    public void Start()
    {
        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            ExamineDialog.instance.Init(null, PropertyType.PT_EXA_ADDTO); // Dialog窗口打开默认是 理论窗口
            ExamineDialog.instance.Active(true);
        });
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        // TCPHelper.GetInfoReq<ExamineInfo>(EventType.ExamineEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_ExamineesInfo = JsonMapper.ToObject<List<ExamineInfo>>(ret);
        foreach (ExamineInfo inf in m_ExamineesInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(ExamineInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<ExamineItem>();
        item.Init(inf);
        m_itemList.Add(clone);
    }

    public void Clear()
    {
        foreach (var item in m_itemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        m_itemList.Clear();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public override void Close()
    {
        ExamineDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  考核信息包
/// </summary>
public class ExamineInfo : BaseInfo
{
    public string id;
    public string ColumnsName;
    public string CourseName;
    public string RegisterTime;
    public int TrainingScore;
    public int ClassNum;
    public int SingleNum;
    public int MulitNum;
    public int TOFNum;
    public bool Status;
    public List<SingleChoice> SingleChoices = new List<SingleChoice>();
    public List<MulitChoice> MulitChoices = new List<MulitChoice>();
    public List<TOFChoice> TOFChoices = new List<TOFChoice>();
}

/// <summary>
/// 单选题包
/// </summary>
public class SingleChoice : BaseChoice
{
    public string Topic;
    public ItemChoice toA = new ItemChoice();
    public ItemChoice toB = new ItemChoice();
    public ItemChoice toC = new ItemChoice();
    public ItemChoice toD = new ItemChoice();
    public string Answer;
    public int Score = 0;
}

/// <summary>
/// 多选
/// </summary>
public class MulitChoice : BaseChoice
{
    public string Topic;
    public Dictionary<string, ItemChoice> Options = new Dictionary<string, ItemChoice>(); // {{"A", {"xxxxx", true}}, {"B", {"xxxxxxx", false}}}
    public string Answer;
    public int Score;
}

/// <summary>
/// 判断题
/// </summary>
public class TOFChoice : BaseChoice
{
    public string Topic;
    public ItemChoice toA = new ItemChoice();
    public ItemChoice toB = new ItemChoice();
    public string Answer;
    public int Score;
}

/// <summary>
/// 理论模式中 一个选项的信息
/// </summary>
public class ItemChoice
{
    public string m_content = "";
    public bool m_isOn = false;

    public ItemChoice() {}

    public ItemChoice(string content, bool ison)
    {
        m_content = content;
        m_isOn = ison;
    }
}

public class BaseChoice {}