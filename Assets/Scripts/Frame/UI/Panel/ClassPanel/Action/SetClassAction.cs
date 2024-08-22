using UnityEngine;

public class SetClassAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        ClassInfo info = inf[0] as ClassInfo;
        ClassPropertyDialog.instance.Loading(info);

        ClassPropertyDialog.instance.RegisterTime.enabled = false;
        ClassPropertyDialog.instance.ID.enabled = false;
        ClassPropertyDialog.instance.Number.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        ClassInfo info = inf[0] as ClassInfo;
        TCPHelper.OperateInfo(info, EventType.ClassEvent, OperateType.REVISE);  
    }
}