using System;
using UnityEngine;

public class SetFacAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        FacultyInfo info = inf[0] as FacultyInfo;
        FacPropertyDialog.instance.Loading(info);
        FacPropertyDialog.instance.RegisterTime.enabled = false;
        FacPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        FacultyInfo info = inf[0] as FacultyInfo;

        NetHelper.OperateInfo(info, EventType.FacultyEvent, OperateType.REVISE);
    }
}