using System;
using UnityEngine;

public class SetColAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        ColumnsInfo info = inf[0] as ColumnsInfo;
        ColPropertyDialog.instance.Loading(info);
        ColPropertyDialog.instance.RegisterTime.enabled = false;
        ColPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ColumnsInfo info = inf[0] as ColumnsInfo;

        NetHelper.OperateInfo(info, EventType.ColumnsEvent, OperateType.REVISE);
    }
}