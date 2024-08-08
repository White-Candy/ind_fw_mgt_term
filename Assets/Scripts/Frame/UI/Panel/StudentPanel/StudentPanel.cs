using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StudentPanel : BasePanel
{
    public GameObject itemTemp;
    public Transform TempParent;

    public static StudentPanel instance;

    private List<GameObject> itemList = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
        instance = this;
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
        itemList.Add(clone);

        var item = clone.GetComponent<SutItem>();
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
