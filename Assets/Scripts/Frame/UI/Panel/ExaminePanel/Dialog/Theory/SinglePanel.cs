
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
    /// ��Ӳ���ʱ����ʼ����ItemӦ��Ϊ�յ�
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
    /// �޸Ĳ���ʱ����ʼ��ItemӦ��Ϊ�ÿγ̵��������
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
    /// �����е���Ŀ���
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
    /// ���
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