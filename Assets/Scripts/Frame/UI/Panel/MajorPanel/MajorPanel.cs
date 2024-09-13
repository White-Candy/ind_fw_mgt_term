using System.Collections.Generic;
using LitJson;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MajorPanel : BasePanel
{    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    // public static MajorPanel instance;

    public Button AddTo;
    public Button Refresh;
    public GameObject Search;

    public static List<MajorInfo> m_majorInfo = new List<MajorInfo>();

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
            MajorPropertyDialog dialog = UIHelper.FindPanel<MajorPropertyDialog>();
            dialog.Init(null, PropertyType.PT_MAJ_ADDTO);
            dialog.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            TCPHelper.GetInfoReq<MajorInfo>(EventType.MajorEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }

            MajorInfo inf = new MajorInfo()
            {
                MajorName = m_searchIpt.text
            };
            TCPHelper.OperateInfo(inf, EventType.MajorEvent, OperateType.SEARCH);
        });        
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<MajorInfo>(EventType.MajorEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_majorInfo = JsonMapper.ToObject<List<MajorInfo>>(ret);
        foreach (MajorInfo inf in m_majorInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(MajorInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<MajorItem>();
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
        MajorPropertyDialog dialog = UIHelper.FindPanel<MajorPropertyDialog>();
        dialog.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  专业信息包
/// </summary>
public class MajorInfo : BaseInfo
{
    public string id;
    public string MajorName;
    public string RegisterTime;
    public string FacultyName;
    public string TeacherName;
}