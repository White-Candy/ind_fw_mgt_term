using System.Collections.Generic;
using UnityEngine;

public class TOFPanel : BasePanel
{
    public TOFItem m_TOFItem;   
    public Transform m_TOFTrans; 

    [HideInInspector]
    public List<TOFChoice> tofChoices = new List<TOFChoice>();

    private List<TOFItem> itemList = new List<TOFItem>();

    public override void Awake()
    {
        base.Awake();
    }
    
    /// <summary>
    /// 添加操作时，初始化的Item应该为空的
    /// </summary>
    /// <param name="number"></param>
    public void Init(int number)
    {
        Clear();
        for(int i = 1; i <= number; ++i)
        {
            TOFItem item = Instantiate(m_TOFItem, m_TOFTrans);
            item.Init(new TOFChoice(), i);
            itemList.Add(item);
        }
    }

    /// <summary>
    /// 修改操作时，初始化Item应该为该课程的题库内容
    /// </summary>
    /// <param name="number"></param>
    public void Init(List<TOFChoice> choices)
    {   
        Clear();
        for (int i = 0; i < choices.Count; ++i)
        {
            TOFItem item = Instantiate(m_TOFItem, m_TOFTrans);
            item.Init(choices[i], i + 1);
            itemList.Add(item);
        }
    }

    public bool InputFieldCheck()
    {
        foreach (var item in itemList)
        {
            if (!item.InputFieldCheck()) return false;
        }
        return true;
    }

    /// <summary>
    /// 把所有的题目打包
    /// </summary>
    /// <returns></returns>
    public List<TOFChoice> Output()
    {
        List<TOFChoice> tofChoices = new List<TOFChoice>();
        foreach (var item in itemList)
        {
            tofChoices.Add(item.Output());
        }
        return tofChoices;
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        foreach (var item in itemList)
        {
            item.Clear();
            item.gameObject.SetActive(false);
            Destroy(item);
        }
        itemList.Clear();
    }    
}