
using System;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TheoryPanel : BasePanel
{
    public TMP_Dropdown Columns;
    public TMP_Dropdown Course;
    public TMP_InputField SingleNumber;
    public TMP_InputField MulitNumber;
    public TMP_InputField TOFNumber;
    public Button Single;
    public Button Muilt;
    public Button TOF;
    public SinglePanel m_SinglePanel;
    public MulitPanel m_MulitPanel;
    public TOFPanel m_TOFPanel;
    
    public static TheoryPanel inst;

    [HideInInspector]
    public List<MulitChoice> mulitChoices = new List<MulitChoice>();

    [HideInInspector]
    public List<TOFChoice> tofChoices = new List<TOFChoice>();

    public override void Awake()
    {
        inst = this;

        // 默认是单选题的界面
        m_SinglePanel.Active(true);
        m_MulitPanel.Active(false);
        m_TOFPanel.Active(false);
    }

    public void Start()
    {
        Single.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(true);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(false);
        });

        Muilt.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(true);
            m_TOFPanel.Active(false);
        });

        TOF.OnClickAsObservable().Subscribe(_ =>
        {
            m_SinglePanel.Active(false);
            m_MulitPanel.Active(false);
            m_TOFPanel.Active(true);
        });

        SingleNumber.onValueChanged.AddListener((text) => 
        {
            int number = 0;
            bool result = int.TryParse(text, out number);
            if (result)
            {
                SinglePanel.inst.Init(number);
            }
        });

        MulitNumber.onValueChanged.AddListener((text) => 
        {
            // TODO..
            
            // int number = 0;
            // bool result = int.TryParse(text, out number);
            // if (result)
            // {
            //     SinglePanel.inst.Init(number);
            // }
        });


        TOFNumber.onValueChanged.AddListener((text) => 
        {
            // TODO..

            // int number = 0;
            // bool result = int.TryParse(text, out number);
            // if (result)
            // {
            //     SinglePanel.inst.Init(number);
            // }
        });
    }



    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        Columns.value = 0;
        Course.value = 0;
        SingleNumber.text = "";
        MulitNumber.text = "";
        TOFNumber.text = "";
    }
}