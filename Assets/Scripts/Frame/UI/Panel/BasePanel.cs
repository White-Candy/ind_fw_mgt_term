using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour, IPanel
{
    //Panel Name
    [HideInInspector]
    public string m_NameP;

    // Panel 强引用
    public GameObject m_Content;

    protected bool m_Visible;

    public virtual void Awake()
    {
        m_NameP = this.GetType().ToString();
        m_Visible = m_Content == null ? false : m_Content.activeSelf;
        UITools.AddPanel(m_NameP, this);
    }

    /// <summary>
    /// 面板的显示控制
    /// </summary>
    /// <param name="b"></param>
    public virtual void Active(bool b)
    {
        if (m_Content != null)
        {
            m_Visible = b;
            m_Content.SetActive(b);
        }
    }
}

public interface IPanel { }

public enum PropertyType
{
    PT_None = 0,
    PT_STU_ADDTO, // 学生添加
    PT_STU_SET, // 学生修改
    PT_FAC_ADDTO, // 学院添加
    PT_FAC_SET,  // 学院修改
    PT_MAJ_ADDTO, // 专业添加
    PT_MAJ_SET  // 专业修改
}