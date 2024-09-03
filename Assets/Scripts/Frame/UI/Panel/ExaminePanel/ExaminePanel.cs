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
            ExamineDialog.instance.Init(null, PropertyType.PT_THE_ADDTO, ActionType.ADD); // Dialog���ڴ�Ĭ���� ���۴���
            ExamineDialog.instance.Active(true);
        });
    }

    public override void Init()
    {
        // TCPHelper.GetInitReq();
        // TCPHelper.GetInfoReq<ExamineInfo>(EventType.ExamineEvent);
    }

    /// <summary>
    /// ��ʾ���ݵ��������
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
    /// Item��clone
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
    /// �ر�
    /// </summary>
    public override void Close()
    {
        ExamineDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  ������Ϣ��
/// </summary>
public class ExamineInfo : BaseInfo
{
    public string id;
    public string ColumnsName;
    public string CourseName;
    public string RegisterTime;
    public bool Status;
    public int ClassNum;
    public int SingleNum;
    public int MulitNum;
    public int TOFNum;
    public List<SingleChoice> SingleChoices;
    public List<MulitChoice> MulitChoices;
    public List<TOFChoice> TOFChoices;
}

/// <summary>
/// ��ѡ���
/// </summary>
public class SingleChoice : BaseChoice
{
    public string Topic;
    public string toA;
    public string toB;
    public string toC;
    public string toD;
    public string Answer;
    public int Score = 0;
}

/// <summary>
/// ��ѡ
/// </summary>
public class MulitChoice : BaseChoice
{
    public string Topic;
    public Dictionary<string, string> Options; // {{"A", "xxxxx"}, {"B", "xxxxxxx"}}
    public string Answer;
    public int Score;
}

/// <summary>
/// �ж���
/// </summary>
public class TOFChoice : BaseChoice
{
    public string Topic;
    public string toA;
    public string toB;
    public string Answer;
    public int Score;
}

public class BaseChoice {}