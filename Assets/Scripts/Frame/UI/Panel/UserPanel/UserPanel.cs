using LitJson;
using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : BasePanel
{
    public GameObject itemTemp;
    public Transform TempParent;

    public Button Import;
    public Button Export;
    public Button AddTo;
    public Button Refresh;
    public GameObject Search;
    // public static UserPanel instance;

    private List<GameObject> itemList = new List<GameObject>();

    private List<UserInfo> m_UsersInfo = new List<UserInfo>();
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

        // 信息导入
        Import.OnClickAsObservable().Subscribe(async x =>
        {
            List<string> filesPath = FileHelper.OpenFileDialog("Excel文件(*.xlsx)" + '\0' + "*.xlsx", "选择Excel文件", "XLSX");
            if (filesPath.Count > 0)
            {
                DialogHelper helper = new DialogHelper();
                MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
                dialog.Show ("课程信息的删除", "是否删除该课程信息？", new ItemPackage("确定", null));    
                return;
            }
            
            var list = await ExcelTools.Excel2UserInfos(filesPath[0]);

            // 请求把新导入的学生信息保存到服务器中
            if (list != null)
            {
                TCPHelper.OperateInfo(list, EventType.UserEvent, OperateType.ADD);
            }              
        });

        // 导出按钮。
        Export.OnClickAsObservable().Subscribe(async x => 
        {
            string savePath = FileHelper.SaveFileDialog("Excel文件(*.xlsx)" + '\0' + "*.xlsx\0\0", "选择Excel文件", "XLSX");
            //Debug.Log("savepath : " + savePath);

            await ExcelTools.CreateExcelFile(savePath);

            await ExcelTools.WriteUserinfo2Excel(m_UsersInfo, savePath);
        });

        // 添加信息。
        AddTo.OnClickAsObservable().Subscribe(x => 
        {
            UserPropertyDialog.instance.Init(default, PropertyType.PT_USER_ADDTO);
            UserPropertyDialog.instance.Active(true);
        });

        // 刷新学生信息。
        Refresh.OnClickAsObservable().Subscribe(x => 
        {
            // TCPHelper.GetInfoReq<TCPStuHelper>();
            TCPHelper.GetInfoReq<UserInfo>(EventType.UserEvent);
        });

        m_searchBtn.OnClickAsObservable().Subscribe(_ => 
        {
            if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
            UserInfo inf = new UserInfo()
            {
                Name = m_searchIpt.text
            };
            TCPHelper.OperateInfo(inf, EventType.UserEvent, OperateType.SEARCH);
        });         
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void Init()
    {
        // TCPHelper.GetInfoReq<TCPStuHelper>();

        TCPHelper.GetInitReq();
        TCPHelper.GetInfoReq<UserInfo>(EventType.UserEvent);
    }

    /// <summary>
    /// 显示数据到主面板上
    /// </summary>
    /// <param name="objs"></param>
    public void Show(params object[] objs)
    {
        Clear();

        string ret = objs[0] as string;
        //Debug.Log("show: " + ret);
        m_UsersInfo = JsonMapper.ToObject<List<UserInfo>>(ret);
        foreach (UserInfo inf in m_UsersInfo)
        {
            CloneItem(inf);
        }
    }

    public void CloneItem(UserInfo inf)
    {
        GameObject clone = Instantiate(itemTemp, TempParent);
        var item = clone.GetComponent<UserItem>();
        item.Init(inf);
        itemList.Add(clone);
    }

    public void Clear()
    {
        foreach (GameObject item in itemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        itemList.Clear();
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
/// 用户信息包
/// </summary>
public class UserInfo : BaseInfo
{
    public string userName;
    public string password;
    public string Name;
    public string Gender;
    public string Age;
    public string Identity;
    public string idCoder;
    public string Contact;
    public string className;
    public bool login = false;
    public string hint = "";
}