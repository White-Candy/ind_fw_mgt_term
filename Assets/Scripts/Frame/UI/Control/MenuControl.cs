using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{   
    public Button Faculty;
    public Button Major;
    public Button Class;
    public Button User;
    public Button Columns;
    public Button Course;
    public Button Examine;
    public Button Score;
    public Button statistics;

    private List<Button> m_btnList = new List<Button>();

    private PanelAction m_currAction = null;

    private void Awake()
    {
        m_btnList = GetComponentsInChildren<Button>().ToList();
        Init();
        foreach (var btn in m_btnList)
        {
            btn.onClick.AddListener(() =>
            {
                ButtonClick(btn);
            });
        }
    }

    /// <summary>
    /// 根据用户等级不同进行响应的初始化
    /// </summary>
    public void Init()
    {
        if (GlobalData.s_currUsrLevel == 2) { ManagerMode(); }
        else if (GlobalData.s_currUsrLevel == 1) { TeacherMode(); }
        else { StudentMode(); }
    }

    public void StudentMode()
    {
        Faculty.gameObject.SetActive(false);
        Major.gameObject.SetActive(false);
        Class.gameObject.SetActive(false);
        User.gameObject.SetActive(false);
        Columns.gameObject.SetActive(false);
        Course.gameObject.SetActive(false);
        Examine.gameObject.SetActive(false);
        statistics.gameObject.SetActive(false);
        Score.gameObject.SetActive(true);
        ButtonClick(Score);
    }

    public void TeacherMode()
    {
        Faculty.gameObject.SetActive(false);
        Major.gameObject.SetActive(false);
        Class.gameObject.SetActive(true);
        User.gameObject.SetActive(true);
        Columns.gameObject.SetActive(false);
        Course.gameObject.SetActive(true);
        Examine.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        statistics.gameObject.SetActive(true);
        ButtonClick(Class);
    }

    public void ManagerMode()
    {
        Faculty.gameObject.SetActive(true);
        Major.gameObject.SetActive(true);
        Class.gameObject.SetActive(true);
        User.gameObject.SetActive(true);
        Columns.gameObject.SetActive(true);
        Course.gameObject.SetActive(true);
        Examine.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        statistics.gameObject.SetActive(true);
        ButtonClick(Faculty);
    }

    public void ButtonClick(Button btn)
    {
        PanelAction action = new PanelAction($"{btn.name}Panel");
        if (m_currAction != null)
        {
            m_currAction.Close();
        }
        m_currAction = action;
        m_currAction?.OnEvent();
    }

    public void Destroy()
    {
        m_currAction.Close();
    }
}
