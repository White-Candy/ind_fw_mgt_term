using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    private List<Button> btnList = new List<Button>();

    private BaseAction m_currAction = new BaseAction();

    private void Awake()
    {
        btnList = GetComponentsInChildren<Button>().ToList();
        foreach (var btn in btnList)
        {
            btn.onClick.AddListener(() =>
            {
                BaseAction action = Tools.CreateObject<BaseAction>($"{btn.name}Action");
                if (m_currAction != null)
                {
                    m_currAction.Close();
                }
                m_currAction = action;
                m_currAction?.OnEvent();
            });

        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
