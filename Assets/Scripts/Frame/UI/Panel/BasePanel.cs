using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour, IPanel
{
    //Panel Name
    [HideInInspector]
    public string m_NameP;

    // Panel ǿ����
    public GameObject m_Content;

    protected bool m_Visible;

    public virtual void Awake()
    {
        m_NameP = this.GetType().ToString();
        m_Visible = m_Content == null ? false : m_Content.activeSelf;
        UITools.AddPanel(m_NameP, this);
    }

    /// <summary>
    /// ������ʾ����
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
    PT_STU_ADDTO, // ѧ�����
    PT_STU_SET, // ѧ���޸�
    PT_FAC_ADDTO, // ѧԺ���
    PT_FAC_SET,  // ѧԺ�޸�
    PT_MAJ_ADDTO, // רҵ���
    PT_MAJ_SET  // רҵ�޸�
}