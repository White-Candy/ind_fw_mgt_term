using LitJson;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StudentPanel : BasePanel
{
    public GameObject itemTemp;
    public Transform TempParent;

    public Button Import;
    public Button Export;
    public Button Revise;
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
        // 导入按钮。
        Import.OnClickAsObservable().Subscribe(async x =>
        {
            string filePath = FileTools.OpenFileDialog();
            // Debug.Log("Import: " + filePath);

            var list = await ExcelTools.Excel2UserInfos(filePath);

            // 请求把新导入的学生信息保存到服务器中
            if (list != null)
            {
                TCPExp.AddUsersInfo(list);
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
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(params object[] objs)
    {
        TCPExp.GetStuInfReq();
    }

    /// <summary>
    /// 展示Panel
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
}
