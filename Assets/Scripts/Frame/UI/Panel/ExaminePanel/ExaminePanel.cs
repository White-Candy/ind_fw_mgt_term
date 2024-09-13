/*
致给后续的开发者: 
Hey! I'm the original developer. 
    When you see this text, it means you have started to take over what I wrote.
 Please don't complain about the various problems in the code structure. 
 I feel very sorry, because the project is really tight and my ability is limited. 
 I can't think of a good code structure in a short time.
    I wish you good luck in development!
                                                    Author: Sugar
                                                    -2024/09/03
 call me: 287276293@qq.com
*/

using System.Collections.Generic;
using System.Data.Common;
using LitJson;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ExaminePanel : BasePanel
{    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    public Button AddTo;

    public Button Activation;

    public Button Refresh;
   public GameObject Search;
    public static List<ExamineInfo> m_examineesInfo = new List<ExamineInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();

    private ExamineDialog m_exmaineDialog;
    private ActivationDialog m_activationDialog;
    private Button m_searchBtn;
    private TMP_InputField m_searchIpt;

    public override void Awake()
    {
        base.Awake();
    }
    
    public void Start()
    {
        m_searchBtn = Search.GetComponentInChildren<Button>();
        m_searchIpt = Search.GetComponentInChildren<TMP_InputField>();       
        m_exmaineDialog = UIHelper.FindPanel<ExamineDialog>();

        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            m_exmaineDialog.Init(null, PropertyType.PT_EXA_ADDTO); // Dialog窗口打开默认是 理论窗口
            m_exmaineDialog.Active(true);
            m_activationDialog.Active(false);
        });

        m_activationDialog = UIHelper.FindPanel<ActivationDialog>();
        Activation.OnClickAsObservable().Subscribe(_ => 
        {
            m_activationDialog.Init();
            m_activationDialog.Active(true);
            m_exmaineDialog.Active(false);
        });
        
        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            TCPHelper.GetInfoReq<ExamineInfo>(EventType.ExamineEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            ExamineInfo inf = new ExamineInfo()
            {
                CourseName = m_searchIpt.text
            };
            TCPHelper.OperateInfo(inf, EventType.ExamineEvent, OperateType.SEARCH);
        });   

        Active(false);
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<ExamineInfo>(EventType.ExamineEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_examineesInfo = JsonMapper.ToObject<List<ExamineInfo>>(ret);
        foreach (ExamineInfo inf in m_examineesInfo)
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
        m_exmaineDialog.Close();
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
    public string TheoryTime = "5"; // 分钟
    public string TrainingTime = "5"; // 分钟
    public string TrainingScore;
    public int PNum;
    public int SingleNum;
    public int MulitNum;
    public int TOFNum;
    public bool Status = false;
    public List<SingleChoice> SingleChoices = new List<SingleChoice>();
    public List<MulitChoice> MulitChoices = new List<MulitChoice>();
    public List<TOFChoice> TOFChoices = new List<TOFChoice>();

    public ExamineInfo() {}
    public ExamineInfo Clone ()
    {
        ExamineInfo inf = new ExamineInfo();
        inf.id = id;
        inf.ColumnsName = ColumnsName;
        inf.CourseName = CourseName;
        inf.RegisterTime = RegisterTime;
        inf.TrainingScore = TrainingScore;
        inf.PNum = PNum;
        inf.SingleNum = SingleNum;
        inf.MulitNum = MulitNum;
        inf.TOFNum = TOFNum;
        inf.TheoryTime = TheoryTime;
        inf.TrainingTime = TrainingTime;
        inf.Status = Status;
        foreach (var Option in SingleChoices) { inf.SingleChoices.Add(Option.Clone()); }
        foreach (var Option in MulitChoices) { inf.MulitChoices.Add(Option.Clone()); }
        foreach (var Option in TOFChoices) { inf.TOFChoices.Add(Option.Clone()); }
        return inf;
    }    
}

/// <summary>
/// 单选题包
/// </summary>
public class SingleChoice
{
    public string Topic;
    public ItemChoice toA = new ItemChoice();
    public ItemChoice toB = new ItemChoice();
    public ItemChoice toC = new ItemChoice();
    public ItemChoice toD = new ItemChoice();
    public string Answer;
    public string Score = "";

    public SingleChoice Clone()
    {
        SingleChoice single = new SingleChoice();
        single.Topic = Topic;
        single.toA = toA.Clone();
        single.toB = toB.Clone();
        single.toC = toB.Clone();
        single.toD = toB.Clone();
        single.Answer = Answer;
        single.Score = Score;
        return single;
    }    
}

/// <summary>
/// 多选
/// </summary>
public class MulitChoice
{
    public string Topic;
    public List<MulitChoiceItem> Options = new List<MulitChoiceItem>(); // {{"A", "xxxxx", true}, {"B", "xxxxxxx", false}}
    public string Answer;
    public string Score = "";

    public MulitChoice Clone()
    {
        MulitChoice mulit = new MulitChoice();
        mulit.Topic = Topic;
        foreach (var Option in Options) { mulit.Options.Add(Option.Clone()); }
        mulit.Answer = Answer;
        mulit.Score = Score;
        return mulit;
    }
}

/// <summary>
/// 判断题
/// </summary>
public class TOFChoice
{
    public string Topic;
    public ItemChoice toA = new ItemChoice();
    public ItemChoice toB = new ItemChoice();
    public string Answer;
    public string Score = "";

    public TOFChoice Clone()
    {
        TOFChoice tof = new TOFChoice();
        tof.Topic = Topic;
        tof.toA = toA.Clone();
        tof.toB = toB.Clone();
        tof.Answer = Answer;
        tof.Score = Score;
        return tof;
    }
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

    public ItemChoice Clone()
    {
        ItemChoice item = new ItemChoice();
        item.m_content = m_content;
        item.m_isOn = m_isOn;
        return item;
    }
}

/// <summary>
/// 因为服务器端没有办法 序列化 字典类型，所以为了保存多选题的选项，需要自定义一个类
/// </summary>
public class MulitChoiceItem
{
    public string Serial = "A";
    public string Content = "";
    public bool isOn = false;

    public MulitChoiceItem() {}

    public MulitChoiceItem(string serial, string content, bool _isOn) 
    {
        Serial = serial;
        Content = content;
        isOn = _isOn;
    }

    public MulitChoiceItem Clone()
    {
        MulitChoiceItem item = new MulitChoiceItem();
        item.Serial = Serial;
        item.Content = Content;
        item.isOn = isOn;
        return item;
    }
}

public class BaseChoice {}