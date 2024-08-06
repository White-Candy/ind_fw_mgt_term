using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
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