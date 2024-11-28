using System;
using UnityEngine;

public class SetFacAction : PD_BaseAction
{
    private FacPropertyDialog m_FacProDialog;
    public override void Init(params object[] inf)
    {   
        FacultyInfo info = inf[0] as FacultyInfo;

        m_FacProDialog = UIHelper.FindPanel<FacPropertyDialog>();
        m_FacProDialog.Clear();

        m_FacProDialog.Loading(info);
        m_FacProDialog.RegisterTime.enabled = false;
        m_FacProDialog.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        FacultyInfo info = inf[0] as FacultyInfo;

        NetHelper.OperateInfo(info, EventType.FacultyEvent, OperateType.REVISE);
    }
}