using System;
using UnityEngine;

public class SetExamineAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        ExamineInfo info = inf[0] as ExamineInfo;
        ExamineDialog.instance.Loading(info);
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ExamineInfo info = inf[0] as ExamineInfo;

        TCPHelper.OperateInfo(info, EventType.ExamineEvent, OperateType.REVISE);
    }
}