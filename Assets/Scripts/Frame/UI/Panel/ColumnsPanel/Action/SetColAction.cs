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

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        ColumnsInfo info = inf[0] as ColumnsInfo;

        TCPHelper.OperateInfo(info, EventType.ColumnsEvent, OperateType.REVISE);
    }
}