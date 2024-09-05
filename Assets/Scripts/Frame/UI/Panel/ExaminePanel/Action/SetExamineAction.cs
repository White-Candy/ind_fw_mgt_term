using System;
using UnityEngine;

public class SetExamineAction : PD_BaseAction
{
    private ExamineDialog m_examineDialog;

    public override void Init(params object[] inf)
    {   
        ExamineInfo info = inf[0] as ExamineInfo;
        m_examineDialog = UIHelper.FindPanel<ExamineDialog>();
        m_examineDialog.Loading(info);

        m_examineDialog.columns.enabled = false;
        m_examineDialog.course.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ExamineInfo info = inf[0] as ExamineInfo;

        TCPHelper.OperateInfo(info, EventType.ExamineEvent, OperateType.REVISE);
    }
}