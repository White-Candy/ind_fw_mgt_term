
using System.Collections.Generic;
using LitJson;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MajorPanel : BasePanel
{
    // 学院信息列表
    public List<MajorInfo> m_facultiesInfo = new List<MajorInfo>();
    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    public static MajorPanel instance;

    public Button AddTo;
    public Button Refresh;

    public List<MajorInfo> m_majorInfo = new List<MajorInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();

        instance = this;
        Active(false);
    }
    
    public void Start()
    {
        AddTo.OnClickAsObservable().Subscribe(_ => 
        {
            MajorPropertyDialog.instance.Init(null, PropertyType.PT_MAJ_ADDTO);
            MajorPropertyDialog.instance.Active(true);
        });
    }

    public void Init()
    {
        TCPHelper.GetInfoReq<TCPMajorHelper>();
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
    public void Close()
    {
        MajorPropertyDialog.instance.Close();
        Active(false);

        Clear();
    }
}

/// <summary>
///  专业信息包
/// </summary>
public class MajorInfo
{
    public string id;
    public string MajorName;
    public string RegisterTime;
    public string FacultyName;
    public string TeacherName;
}