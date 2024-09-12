using System.Collections.Generic;
using LitJson;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : BasePanel
{    
    public GameObject m_itemTemp;

    public Transform m_tempParent;

    public Button Search;
    public Button Export;
    public Button Refresh;

    public static List<ScoreInfo> m_scoresInfo = new List<ScoreInfo>();

    private List<GameObject> m_itemList = new List<GameObject>();

    private SearchDialog searchDialog = new SearchDialog();

    public override void Awake()
    {
        base.Awake();

        // instance = this;
        Active(false);
    }
    
    public void Start()
    {
        searchDialog = UIHelper.FindPanel<SearchDialog>();
        Search.OnClickAsObservable().Subscribe(_ => 
        {
            searchDialog.Init();
            searchDialog.Active(true); 
        });

        Export.OnClickAsObservable().Subscribe(async _ => 
        {
            string savePath = FileHelper.SaveFileDialog("Excel文件(*.xlsx)" + '\0' + "*.xlsx\0\0", "选择Excel文件", "XLSX");
            //Debug.Log("savepath : " + savePath);

            await ExcelTools.CreateExcelFile(savePath);

            await ExcelTools.WriteScoresInf2Excel(m_scoresInfo, savePath);
        });

        Refresh.OnClickAsObservable().Subscribe(_ => { Init(); });
    }

    public override void Init()
    {
        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<ScoreInfo>(EventType.ScoreEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        m_scoresInfo = JsonMapper.ToObject<List<ScoreInfo>>(ret);
        foreach (ScoreInfo inf in m_scoresInfo)
        {
            CloneItem(inf);
        }
    }

    /// <summary>
    /// Item的clone
    /// </summary>
    /// <param name="inf"></param>
    public void CloneItem(ScoreInfo inf)
    {
        GameObject clone = Instantiate(m_itemTemp, m_tempParent);
        var item = clone.GetComponent<ScoreItem>();
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
        Active(false);
        Clear();
    }
}

/// <summary>
/// 成绩管理信息
/// </summary>
public class ScoreInfo : BaseInfo
{
    public string className;
    public string columnsName;
    public string courseName;
    public string registerTime; // 该次考试的注册时间
    public string userName;
    public string Name;
    public string theoryScore;
    public string trainingScore;
    public bool theoryFinished; //本次理论考试是否完成
    public bool trainingFinished; //本次实训考试是否完成

    public ScoreInfo Clone()
    {
        ScoreInfo inf = new ScoreInfo()
        {
            className = className,
            columnsName = columnsName,
            courseName = courseName,
            registerTime = registerTime,
            userName = userName,
            Name = Name,
            theoryScore = theoryScore,
            trainingScore = trainingScore,
            theoryFinished = theoryFinished,
            trainingFinished = trainingFinished,
        };
        return inf;
    }
}