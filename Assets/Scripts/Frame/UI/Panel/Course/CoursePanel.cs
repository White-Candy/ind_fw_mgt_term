
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
    public Button ResDelete;
    public Button Refresh;
    public GameObject Search;
    public static List<CourseInfo> m_courseInfo = new List<CourseInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();
    private Button m_searchBtn;
    private TMP_InputField m_searchIpt;
    
    private ResDeletePanel m_ResDeletePanel;
    private CoursePropertyDialog m_CourseDialog;

    public override void Awake()
    {
        base.Awake();

        // instance = this;

#if UNITY_WEBGL
        ResDelete.gameObject.SetActive(false);
#endif
    }
    
    public void Start()
    {
        m_searchBtn = Search.GetComponentInChildren<Button>();
        m_searchIpt = Search.GetComponentInChildren<TMP_InputField>();  

        m_ResDeletePanel = UIHelper.FindPanel<ResDeletePanel>();
        m_ResDeletePanel.Active(false);

        m_CourseDialog = UIHelper.FindPanel<CoursePropertyDialog>();
        m_CourseDialog.Active(false);

        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            m_CourseDialog.Init(null, PropertyType.PT_COR_ADDTO);
            m_CourseDialog.Active(true);
            m_ResDeletePanel.Active(false);
        });

        ResDelete.OnClickAsObservable().Subscribe(_ => 
        {
            m_ResDeletePanel.Init();
            m_ResDeletePanel.Active(true);
            m_CourseDialog.Active(false);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            NetHelper.GetInfoReq<CourseInfo>(EventType.CourseEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            CourseInfo inf = new CourseInfo()
            {
                CourseName = m_searchIpt.text
            };
            NetHelper.OperateInfo(inf, EventType.CourseEvent, OperateType.SEARCH);
        });

        Active(false);          
    }

    public override void Init()
    {
        NetHelper.GetInitReq();
        NetHelper.GetInfoReq<CourseInfo>(EventType.CourseEvent);
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