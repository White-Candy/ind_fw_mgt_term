using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserPanel : BasePanel
{
    public GameObject itemTemp;
    public Transform TempParent;

    public Button Import;
    public Button Export;
    public Button AddTo;
    public Button Refresh;
    public Button batchDelete;
    public GameObject Search;
    // public static UserPanel instance;

    public GameObject delControls;
    public Button deleteOk; // 确认删除
    public Button delCancel; // 取消删除
    public Toggle seleteAll;
    public TMP_Dropdown identityDropdown;
    public Button identityButton;

    private List<GameObject> itemList = new List<GameObject>();

    private List<UserInfo> m_UsersInfo = new List<UserInfo>();
    private Button m_searchBtn;
    private TMP_InputField m_searchIpt;
    private UserPropertyDialog m_UserProDialog;
    private string IdentityButtonText 
    { 
        set 
        {
            if (identityDropdown)
                identityButton.GetComponentInChildren<TextMeshProUGUI>().text = value; 
        } 
    }
    private long m_AllSelectCnt = 0;

    public override void Awake()
    {
        base.Awake();
        // instance = this;
    }

    public void Start()
    {
        m_searchBtn = Search.GetComponentInChildren<Button>();
        m_searchIpt = Search.GetComponentInChildren<TMP_InputField>();
        m_UserProDialog = UIHelper.FindPanel<UserPropertyDialog>();

        // 信息导入
        Import.onClick.AddListener(ImportUsrInfo);

        // 导出按钮。
        Export.onClick.AddListener(ExportUserInfoIntoExcelFile);

        // 添加信息。
        AddTo.onClick.AddListener(HttpReqUserinfoAdd);

        // 刷新学生信息。
        Refresh.onClick.AddListener(() => { NetHelper.GetInfoReq<UserInfo>(EventType.UserEvent); });

        m_searchBtn.onClick.AddListener(UseRearchMethodHttpReqUsrInfo);

        // 批量删除
        batchDelete.onClick.AddListener(BatchDeleteOpation);

        // 确认删除
        deleteOk.onClick.AddListener(ConfirmDelete);

        // 取消删除
        delCancel.onClick.AddListener(DeleteOpation);

        seleteAll.onValueChanged.AddListener(SeleteAllOpation);

        identityDropdown.onValueChanged.AddListener(IdentityScreening);

        identityButton.onClick.AddListener(ShowIdentityDropItemList);
        
#if UNITY_WEBGL
        Import.gameObject.SetActive(false);
        Export.gameObject.SetActive(false);
#endif

        Active(false);    
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void InitAsync()
    {
        // NetHelper.GetInfoReq<TCPStuHelper>();

        NetHelper.GetInitReq();
        NetHelper.GetInfoReq<UserInfo>(EventType.UserEvent);
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
        IdentityButtonText = @"身份";

        foreach (UserInfo inf in m_UsersInfo)
        {
            CloneItem(inf);
        }
        delControls.SetActive(false);
    }

    public void CloneItem(UserInfo inf)
    {
        GameObject clone = Instantiate(itemTemp, TempParent);
        var item = clone.GetComponent<UserItem>();
        item.Init(inf);

        item.delToggle.onValueChanged.AddListener((b) => 
        {
            if (!b)
            {
                m_AllSelectCnt--;
            }
            else
            {
                m_AllSelectCnt++;
                if (m_AllSelectCnt == itemList.Count())
                    seleteAll.isOn = true;
            }
        });
        itemList.Add(clone);
    }

    public void BatchDeletion()
    {
        foreach (var item in itemList)
        {
            UserItem usrItem = item?.GetComponent<UserItem>();
            if (usrItem.delToggle.isOn)
            {
                usrItem.ConfirmDelete();
            }
        }
    }

    private async void ExportUserInfoIntoExcelFile()
    {
        string savePath = FileHelper.SaveFileDialog("Excel文件(*.xlsx)" + '\0' + "*.xlsx\0\0", "选择Excel文件", "XLSX");
        //Debug.Log("savepath : " + savePath);

        await ExcelTools.CreateExcelFile(savePath);

        await ExcelTools.WriteUserinfo2Excel(m_UsersInfo, savePath);
    }

    private async void ImportUsrInfo()
    {
        List<string> filesPath = FileHelper.OpenFileDialog("Excel文件(*.xlsx)" + '\0' + "*.xlsx", "选择Excel文件", "XLSX");
        if (filesPath.Count() == 0) return;
        //if (filesPath.Count > 0)
        //{
        //    DialogHelper helper = new DialogHelper();
        //    MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
        //    dialog.Show ("课程信息的删除", "是否删除该课程信息？", new ItemPackage("确定", null));    
        //    return;
        //}

        var list = await ExcelTools.Excel2UserInfos(filesPath[0]);

        // 请求把新导入的学生信息保存到服务器中
        if (list != null)
        {
            NetHelper.OperateInfo(list, EventType.UserEvent, OperateType.ADD);
        }
    }

    private void HttpReqUserinfoAdd()
    {
        m_UserProDialog.Init(default, PropertyType.PT_USER_ADDTO);
        m_UserProDialog.Active(true);
    }

    private void UseRearchMethodHttpReqUsrInfo()
    {
        if (!UIHelper.InputFieldCheck(m_searchIpt.text)) { return; }
        UserInfo inf = new UserInfo()
        {
            Name = m_searchIpt.text
        };
        NetHelper.OperateInfo(inf, EventType.UserEvent, OperateType.SEARCH);
    }

    private void IdentityScreening(int i)
    {
        string identity = identityDropdown.options[i].text;
        foreach (var item in itemList)
        {
            item.SetActive(false);
            Destroy(item);
        }
        itemList.Clear();

        foreach (UserInfo inf in m_UsersInfo)
        {
            if (inf.Identity == identity)
            { 
                CloneItem(inf);
            }
        }
        delControls.SetActive(false);

        IdentityButtonText = identityDropdown.options[i].text;
    }

    /// <summary>
    /// 点击button按钮 显示 dropdown的listitem
    /// </summary>
    private void ShowIdentityDropItemList()
    {
        identityDropdown.Show();
    }

    private void BatchDeleteOpation()
    {
        seleteAll.isOn = false;
        foreach (var item in itemList)
        {
            UserItem usrItem = item.GetComponent<UserItem>();
            usrItem.delToggle.isOn = false;
            usrItem.Delete.gameObject.SetActive(false);
            usrItem.delToggle.gameObject.SetActive(true);
        }
        delControls.SetActive(true);
    }

    private void DeleteOpation()
    {
        delControls.SetActive(false);
        foreach (var item in itemList)
        {
            UserItem usrItem = item.GetComponent<UserItem>();
            usrItem.Delete.gameObject.SetActive(true);
            usrItem.delToggle.gameObject.SetActive(false);
        }
    }

    private void SeleteAllOpation(bool b)
    {
        foreach (var item in itemList)
        {
            UserItem usrItem = item.GetComponent<UserItem>();
            usrItem.delToggle.isOn = b;
        }
    }

    private void ConfirmDelete()
    {
         DialogHelper helper = new DialogHelper();
        MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
        dialog.Show("用户信息删除", "是否批量删除用户信息？", new ItemPackage("确定", BatchDeletion), new ItemPackage("取消", null));
    }

    public void Clear()
    {
        foreach (GameObject item in itemList)
        {
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        itemList.Clear();
        m_AllSelectCnt = 0;
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
    public string UnitName; // 所属单位 学生所属班级，其他所属学院
    public bool login = false;
    public string hint = "";

    public UserInfo Clone()
    {
        UserInfo inf = new UserInfo()
        {
            userName = userName,
            password = password,
            Name = Name,
            Gender = Gender,
            Age = Age,
            Identity = Identity,
            idCoder = idCoder,
            Contact = Contact,
            UnitName = UnitName,
            login = login,
            hint = hint
        };
        return inf;
    }
}