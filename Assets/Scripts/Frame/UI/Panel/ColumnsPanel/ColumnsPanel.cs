using System.Collections.Generic;
using LitJson;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ColumnsPanel : BasePanel
{
    // 学院信息列表
    public List<ColumnsInfo> m_facultiesInfo = new List<ColumnsInfo>();
    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    // public static ColumnsPanel instance;

    public Button AddTo;
    public Button Refresh;
    public GameObject Search;
    public static List<ColumnsInfo> m_columnsInfo = new List<ColumnsInfo>();

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
            ColPropertyDialog.instance.Init(null, PropertyType.PT_COL_ADDTO);
            ColPropertyDialog.instance.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            NetHelper.GetInfoReq<ColumnsInfo>(EventType.ColumnsEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            ColumnsInfo inf = new ColumnsInfo()
            {
                Name = m_searchIpt.text
            };
            NetHelper.OperateInfo(inf, EventType.ColumnsEvent, OperateType.SEARCH);
        });           
    }

    public override void Init()
    {
        // NetHelper.GetInfoReq<TCPFacHelper>();
        NetHelper.GetInitReq();
        NetHelper.GetInfoReq<ColumnsInfo>(EventType.ColumnsEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_columnsInfo = JsonMapper.ToObject<List<ColumnsInfo>>(ret);
        foreach (ColumnsInfo inf in m_columnsInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(ColumnsInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<ColumnsItem>();
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
        ColPropertyDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  栏目信息包
/// </summary>
public class ColumnsInfo : BaseInfo
{
    public string id;
    public string Name;
    public string RegisterTime;
}