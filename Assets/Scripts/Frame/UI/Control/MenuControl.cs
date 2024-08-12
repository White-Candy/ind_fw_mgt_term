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

    private void Awake()
    {
        btnList = GetComponentsInChildren<Button>().ToList();
        foreach (var btn in btnList)
        {
            btn.onClick.AddListener(() =>
            {
                BaseAction action = Tools.CreateObject<BaseAction>($"{btn.name}Action");
                action?.OnEvent();
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
