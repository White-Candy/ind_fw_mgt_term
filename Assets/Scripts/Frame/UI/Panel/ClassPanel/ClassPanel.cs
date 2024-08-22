
using System.Collections.Generic;
using LitJson;
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

    public static ClassPanel instance;

    public Button AddTo;
    public Button Refresh;

    public List<ClassInfo> m_classInfo = new List<ClassInfo>();

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
            ClassPropertyDialog.instance.Init(default, PropertyType.PT_CLASS_ADDTO);
            ClassPropertyDialog.instance.Active(true);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => 
        {
            TCPHelper.GetInfoReq<ClassInfo>(EventType.ClassEvent);
        });
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<ClassInfo>(EventType.ClassEvent);
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
        ClassPropertyDialog.instance.Close();
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
    public string Number;
}