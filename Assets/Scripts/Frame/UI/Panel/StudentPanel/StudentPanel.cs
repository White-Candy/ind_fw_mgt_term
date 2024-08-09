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

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void Start()
    {
        Import.OnClickAsObservable().Subscribe(x =>
        {
            string filePath = FileTools.OpenFileDialog();
            Debug.Log("Import: " + filePath);

            // TODO.. analysis excel file property..
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
        string ret = objs[0] as string;
        List<UserInfo> list = JsonMapper.ToObject<List<UserInfo>>(ret);
        foreach (UserInfo inf in list)
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
            Destroy(item);
        }
        itemList.Clear();
    }
}
