using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddExamineAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        ExamineDialog.instance.Clear();
    }

    public override void Action(Action append, params object[] inf)
    {
        base.Action(inf:inf);

        ExamineInfo info = inf[0] as ExamineInfo;

        List<int> id = new List<int>();
        foreach (var item in ExaminePanel.m_ExamineesInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        info.RegisterTime = Tools.GetCurrLocalTime_YMD();

        TCPHelper.OperateInfo(info, EventType.ExamineEvent, OperateType.ADD);
    }
}