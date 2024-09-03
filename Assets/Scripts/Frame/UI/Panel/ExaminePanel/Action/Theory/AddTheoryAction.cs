using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddTheoryAction : PD_BaseAction
{
    private ExamineInfo m_inf = new ExamineInfo();

    public override void Init(params object[] inf)
    {
        m_inf = inf[0] as ExamineInfo;
        TheoryPanel.inst.Clear();
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ExamineInfo info = inf[0] as ExamineInfo;

        List<int> id = new List<int>();
        foreach (var item in MajorPanel.m_majorInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        TCPHelper.OperateInfo(info, EventType.MajorEvent, OperateType.ADD);        
    }
}