
using System.Collections.Generic;
using UnityEngine;

public class MulitPanel : BasePanel
{
    public MulitItem m_MulitItem; 
    public Transform m_MulitTrans;       

    [HideInInspector]
    public List<MulitChoice> mulitChoices = new List<MulitChoice>();

    private List<MulitItem> itemList = new List<MulitItem>();
    
    public static MulitPanel inst;

    public override void Awake()
    {
        base.Awake();
        inst = this;
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
            MulitItem item = Instantiate(m_MulitItem, m_MulitTrans);
            item.Init(i);
            itemList.Add(item);
        }
    }

    /// <summary>
    /// 修改操作时，初始化Item应该为该课程的题库内容
    /// </summary>
    /// <param name="number"></param>
    public void Init(List<MulitChoice> choices)
    {   
        Clear();
        for (int i = 0; i < choices.Count; ++i)
        {
            MulitItem item = Instantiate(m_MulitItem, m_MulitTrans);
            item.Init(choices[i], i + 1);
            itemList.Add(item);
        }
    }

    /// <summary>
    /// 把所有的题目打包
    /// </summary>
    /// <returns></returns>
    public List<MulitChoice> Output()
    {
        List<MulitChoice> mulitChoices = new List<MulitChoice>();
        foreach (var item in itemList)
        {
            mulitChoices.Add(item.Output());
        }
        return mulitChoices;
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