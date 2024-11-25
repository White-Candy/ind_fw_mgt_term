
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButton : Button
{
    private Image m_UnSelect = null;

    private Image m_Select = null;

    public Image unSelect { get { return m_UnSelect; } set { m_UnSelect = value; } }
    public Image selected;
}