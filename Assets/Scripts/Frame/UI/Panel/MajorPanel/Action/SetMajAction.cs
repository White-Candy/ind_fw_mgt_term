using System;
using UnityEngine;

public class SetMajAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        MajorInfo info = inf[0] as MajorInfo;
        MajorPropertyDialog dialog = UIHelper.FindPanel<MajorPropertyDialog>();
        dialog.Loading(info);
        dialog.RegisterTime.enabled = false;
        dialog.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        MajorInfo info = inf[0] as MajorInfo;
        TCPHelper.OperateInfo(info, EventType.MajorEvent, OperateType.REVISE);  
    }
}