using LitJson;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StudentPanel : BasePanel
{
    public GameObject itemTemp;
    public Transform TempParent;

    public Button Import;
    public Button Export;
    public Button AddTo;
    public Button Refresh;

    public static StudentPanel instance;

    private List<GameObject> itemList = new List<GameObject>();

    private List<UserInfo> m_UsersInfo = new List<UserInfo>();

    public override void Awake()
    {
        base.Awake();
        instance = this;
        
        Active(false);
    }

    public void Start()
    {
        // 学生信息导入
        Import.OnClickAsObservable().Subscribe(async x =>
        {
            string filePath = FileTools.OpenFileDialog();
            // Debug.Log("Import: " + filePath);

            var list = await ExcelTools.Excel2UserInfos(filePath);

            // 请求把新导入的学生信息保存到服务器中
            if (list != null)
            {
                TCPHelper.OperateInfo(list, EventType.UserEvent, OperateType.ADD);
            }
        });

        // 导出按钮。
        Export.OnClickAsObservable().Subscribe(async x => 
        {
            string savePath = FileTools.SaveFileDialog();
            //Debug.Log("savepath : " + savePath);

            await ExcelTools.CreateExcelFile(savePath);

            await ExcelTools.WriteUserinfo2Excel(m_UsersInfo, savePath);
        });

        // 添加学生信息。
        AddTo.OnClickAsObservable().Subscribe(x => 
        {
            StuPropertyDialog.instance.Init(default, PropertyType.PT_STU_ADDTO);
            StuPropertyDialog.instance.Active(true);
        });

        // 刷新学生信息。
        Refresh.OnClickAsObservable().Subscribe(x => 
        {
            // TCPHelper.GetInfoReq<TCPStuHelper>();
            TCPHelper.GetInfoReq<UserInfo>(EventType.UserEvent);
        });
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(params object[] objs)
    {
        // TCPHelper.GetInfoReq<TCPStuHelper>();
        // TCPHelper.GetInitReq();
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
        m_UsersInfo = JsonMapper.ToObject<List<UserInfo>>(ret);
        foreach (UserInfo inf in m_UsersInfo)
        {
            CloneItem(inf);
        }
    }

    public void CloneItem(UserInfo inf)
    {
        GameObject clone = Instantiate(itemTemp, TempParent);
        var item = clone.GetComponent<SutItem>();
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
    public void Close()
    {
        Active(false);
        Clear();
    }
}
