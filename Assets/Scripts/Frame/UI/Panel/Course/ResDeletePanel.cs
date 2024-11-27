
using System.Collections.Generic;
using LitJson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResDeletePanel : BasePanel
{
    public ResItem Item;
    public Transform ParentTrans;
    public TMP_Dropdown ColumnsDropdown;
    public TMP_Dropdown CourseDropdown;
    public Button SeletionButton;
    public Button BackButton;

    private List<ResourcesInfo> m_ResourcesInfo = new List<ResourcesInfo>();
    private List<ResItem> m_ItemList = new List<ResItem>();

    public override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        ColumnsDropdown.onValueChanged.AddListener((i) => { CourseSelection(ColumnsDropdown.options[ColumnsDropdown.value].text); });
        BackButton.onClick.AddListener(() => { Active(false); });
        SeletionButton.onClick.AddListener(() => 
        {
            string columns = ColumnsDropdown.options[ColumnsDropdown.value].text;
            string course = CourseDropdown.options[CourseDropdown.value].text;
            SelectionResources(columns, course);
        });
    }

    public override void Init()
    {
        NetHelper.GetInfoReq<ResourcesInfo>(EventType.ResEvent);   
    }

    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_ResourcesInfo = JsonMapper.ToObject<List<ResourcesInfo>>(ret);
        foreach (ResourcesInfo inf in m_ResourcesInfo)
        {
            CloneItem(inf);
        }

        UIHelper.AddDropDownOptions(ref ColumnsDropdown, GlobalData.columnsList);
        if (ColumnsDropdown.options.Count > 0) CourseSelection(ColumnsDropdown.options[0].text);
    }

    public void CloneItem(ResourcesInfo inf)
    {
        ResItem item = Instantiate(Item, ParentTrans).GetComponent<ResItem>();
        item.Init(inf);
        item.gameObject.SetActive(true);
        m_ItemList.Add(item);
    }

    /// <summary>
    /// 课程筛选
    /// </summary>
    /// <param name="colName"></param>
    public void CourseSelection(string colName)
    {
        CourseDropdown.ClearOptions();
        List<string> courseList = new List<string>();
        foreach (var inf in GlobalData.coursesList)
        {
            if (inf.Columns == colName)
                courseList.Add(inf.CourseName);
        }
        CourseDropdown.AddOptions(courseList);
    } 

    /// <summary>
    /// 通过 '栏目'，'课程'字段进行筛选资源Item
    /// </summary>
    public void SelectionResources(string columns, string course)
    {
        Clear();
        foreach (var inf in m_ResourcesInfo)
        {
            string[] split = inf.relaPath.Split("\\");
            if (columns == split[0] && course == split[1]) CloneItem(inf);
        }
    }

    public void Clear()
    {
        foreach (var item in m_ItemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }
        m_ItemList.Clear();
    }
}

/// <summary>
/// 客户端资源更新前会进行检查请求
/// 如果客户端的版本码和服务器的不一致则需要更新
/// </summary>
public class ResourcesInfo : BaseInfo
{
    public string relaPath;
    public string version_code;
    public bool need_updata;


    public ResourcesInfo() { }

    public ResourcesInfo(ResourcesInfo clone)
    {
        relaPath = clone.relaPath;
        version_code = clone.version_code;
        need_updata = clone.need_updata;
    }
}