
using System.Collections.Generic;
using LitJson;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoursePanel : BasePanel
{    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    // public static CoursePanel instance;

    public Button AddTo;
    public Button Refresh;
    public GameObject Search;
    public static List<CourseInfo> m_courseInfo = new List<CourseInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();
    private Button m_searchBtn;
    private TMP_InputField m_searchIpt;

    public override void Awake()
    {
        base.Awake();

        // instance = this;
        Active(false);
    }
    
    public void Start()
    {
        m_searchBtn = Search.GetComponentInChildren<Button>();
        m_searchIpt = Search.GetComponentInChildren<TMP_InputField>();  

        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            CoursePropertyDialog.instance.Init(null, PropertyType.PT_COR_ADDTO);
            CoursePropertyDialog.instance.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            TCPHelper.GetInfoReq<CourseInfo>(EventType.CourseEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            CourseInfo inf = new CourseInfo()
            {
                CourseName = m_searchIpt.text
            };
            TCPHelper.OperateInfo(inf, EventType.CourseEvent, OperateType.SEARCH);
        });          
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<CourseInfo>(EventType.CourseEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_courseInfo = JsonMapper.ToObject<List<CourseInfo>>(ret);
        foreach (CourseInfo inf in m_courseInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(CourseInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<CourseItem>();
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
        CoursePropertyDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  课程信息包
/// </summary>
public class CourseInfo : BaseInfo
{
    public string id;
    public string CourseName;
    public string Columns;
    public string Working;
    public string RegisterTime;
}