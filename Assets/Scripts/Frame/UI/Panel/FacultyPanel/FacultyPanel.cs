
using System.Collections.Generic;
using LitJson;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FacultyPanel : BasePanel
{
    // 学院信息列表
    public List<FacultyInfo> m_facultiesInfo = new List<FacultyInfo>();
    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    // public static FacultyPanel instance;

    public Button AddTo;
    public Button Refresh;

    public static List<FacultyInfo> m_faculiesInfo = new List<FacultyInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();

        // instance = this;
        Active(false);
    }
    
    public void Start()
    {
        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            FacPropertyDialog.instance.Init(null, PropertyType.PT_FAC_ADDTO);
            FacPropertyDialog.instance.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            TCPHelper.GetInfoReq<FacultyInfo>(EventType.FacultyEvent);
        });
    }

    public override void Init()
    {
        // TCPHelper.GetInfoReq<TCPFacHelper>();
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<FacultyInfo>(EventType.FacultyEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_faculiesInfo = JsonMapper.ToObject<List<FacultyInfo>>(ret);
        foreach (FacultyInfo inf in m_faculiesInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(FacultyInfo inf)
    {
        // Debug.Log($"Clone Item: {inf.Name} || {inf.TeacherName} || {inf.RegisterTime}");
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<FacultyItem>();
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
        FacPropertyDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  学院信息包
/// </summary>
public class FacultyInfo : BaseInfo
{
    public string id;
    public string Name;
    public string RegisterTime;
    public string TeacherName;
}