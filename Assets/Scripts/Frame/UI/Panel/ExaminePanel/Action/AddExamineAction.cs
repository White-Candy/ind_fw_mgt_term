using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddExamineAction : PD_BaseAction
{
    private ExamineDialog m_examineDialog;

    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        m_examineDialog = UIHelper.FindPanel<ExamineDialog>();
        m_examineDialog.Clear();

        m_examineDialog.columns.enabled = true;
        m_examineDialog.course.enabled = true;
    }

    public override void Action(Action append, params object[] inf)
    {
        base.Action(inf:inf);

        ExamineInfo info = inf[0] as ExamineInfo;

        List<int> id = new List<int>();
        foreach (var item in ExaminePanel.m_examineesInfo)
            id.Add(int.Parse(item.id));
            
        info.id = Tools.SpawnRandom(id).ToString();
        info.RegisterTime = Tools.GetCurrLocalTime_YMD();

        TCPHelper.OperateInfo(info, EventType.ExamineEvent, OperateType.ADD);
    }
}