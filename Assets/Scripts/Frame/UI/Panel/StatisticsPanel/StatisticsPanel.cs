using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StatisticsPanel : BasePanel
{
    public GameObject m_itemTemp;
    public Transform m_tempParent;

    public Button searchButton;
    public Button sumButton;
    public TextMeshProUGUI sumText;


    private StatisticsSearch m_StatisticsSearch = new StatisticsSearch();

    public static List<UsrTimeInfo> m_UsrTimeInfo = new List<UsrTimeInfo>();
    private List<GameObject> m_ItemList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();

        // instance = this;
        Active(false);
    }


    void Start()
    {
        m_StatisticsSearch = UIHelper.FindPanel<StatisticsSearch>();
        searchButton.onClick.AddListener(() => 
        {
            m_StatisticsSearch.InitAsync();
            m_StatisticsSearch.Active(true);
        });

        sumButton.onClick.AddListener(() => 
        {
            long sum = 0;
            foreach (var inf in m_UsrTimeInfo)
                sum += inf.min;
            sumText.text = $"{sum}����";
        });
    }

    public override async void InitAsync()
    {
        base.InitAsync();

        await Client.m_Server.HttpRequest(GlobalData.IP + "GetUsrTimeList", "", (ret) =>
        {
            Debug.Log("Post Return: " + ret);
            Show(ret);
        }, UnityWebRequest.kHttpVerbGET);

    }

    void Update()
    {
        
    }

    /// <summary>
    /// Item��clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(UsrTimeInfo inf)
    {
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<StatisticsItem>();
        item.Init(inf);
        m_ItemList.Add(clone);
    }

    /// <summary>
    /// ��ʾ���ݵ��������
    /// </summary>
    /// <param name="objs"></param>
    public void Show(string retStr)
    {
        Clear();

        string ret = retStr;
        m_UsrTimeInfo = JsonMapper.ToObject<List<UsrTimeInfo>>(ret);
        foreach (UsrTimeInfo inf in m_UsrTimeInfo)
        {
            CloneItem(inf);
        }

        sumText.text = $"{0}����";
    }

    public void Clear()
    {
        foreach (var item in m_ItemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        m_ItemList.Clear();
    }

    /// <summary>
    /// �ر�
    /// </summary>
    public override void Close()
    {
        Active(false);
        Clear();
    }
}


/// <summary>
/// ʱ��ͳ��
/// </summary>
public class UsrTimeInfo : BaseInfo
{
    public string usrName = ""; // �û���
    public string moduleName = ""; // ģ������
    public int min = 0; // ʹ��ʱ��(����)
}