
using System.Collections.Generic;
using LitJson;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClassPanel : BasePanel
{
    // 学院信息列表
    public List<ClassInfo> m_facultiesInfo = new List<ClassInfo>();
    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    // public static ClassPanel instance;

    public Button AddTo;
    public Button Refresh;
    public GameObject Search;
    public static List<ClassInfo> m_classInfo = new List<ClassInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();
    private Button m_searchBtn;
    private TMP_InputField m_searchIpt;
    private ClassPropertyDialog m_ClassProDialog;
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
        m_ClassProDialog = UIHelper.FindPanel<ClassPropertyDialog>();

        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            m_ClassProDialog.Init(default, PropertyType.PT_CLASS_ADDTO);
            m_ClassProDialog.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            NetHelper.GetInfoReq<ClassInfo>(EventType.ClassEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            ClassInfo inf = new ClassInfo()
            {
                Class = m_searchIpt.text
            };
            NetHelper.OperateInfo(inf, EventType.ClassEvent, OperateType.SEARCH);
        });            
    }

    public override void Init()
    {
        NetHelper.GetInitReq();
        NetHelper.GetInfoReq<ClassInfo>(EventType.ClassEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_classInfo = JsonMapper.ToObject<List<ClassInfo>>(ret);
        foreach (ClassInfo inf in m_classInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(ClassInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<ClassItem>();
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
        m_ClassProDialog.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  班级信息包
/// </summary>
public class ClassInfo : BaseInfo
{
    public string id;
    public string Class;
    public string RegisterTime;
    public string Faculty;
    public string Major;
    public string Teacher;
    public int Number;
}