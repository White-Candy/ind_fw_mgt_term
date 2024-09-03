
using System;
using System.Collections.Generic;
using UnityEngine;

public class SinglePanel : BasePanel
{
    public SingleItem m_SingleItem;
    public Transform m_SingleTrans;

    [HideInInspector]
    public List<SingleChoice> singleChoices = new List<SingleChoice>();

    private List<SingleItem> itemList = new List<SingleItem>();
    
    public static SinglePanel inst;

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
            SingleItem item = Instantiate(m_SingleItem, m_SingleTrans);
            item.Init(new SingleChoice(), i);
            itemList.Add(item);
        }
    }

    /// <summary>
    /// 修改操作时，初始化Item应该为该课程的题库内容
    /// </summary>
    /// <param name="number"></param>
    public void Init(List<SingleChoice> choices)
    {   
        Clear();
        for (int i = 0; i < choices.Count; ++i)
        {
            SingleItem item = Instantiate(m_SingleItem, m_SingleTrans);
            item.Init(choices[i], i + 1);
            itemList.Add(item);
        }
    }

    /// <summary>
    /// 把所有的题目打包
    /// </summary>
    /// <returns></returns>
    public List<SingleChoice> Output()
    {
        List<SingleChoice> singleChoices = new List<SingleChoice>();
        foreach (var item in itemList)
        {
            singleChoices.Add(item.Output());
        }
        return singleChoices;
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        foreach (var item in itemList)
        {
            item.Clear();
            Destroy(item);
        }
        itemList.Clear();
    }

}