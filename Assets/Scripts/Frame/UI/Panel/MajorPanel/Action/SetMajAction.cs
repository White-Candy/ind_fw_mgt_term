using UnityEngine;

public class SetMajAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        MajorInfo info = inf[0] as MajorInfo;
        MajorPropertyDialog.instance.Loading(info);
        MajorPropertyDialog.instance.RegisterTime.enabled = false;
        MajorPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        MajorInfo info = inf[0] as MajorInfo;
        TCPHelper.OperateInfo(info, EventType.MajorEvent, OperateType.REVISE);  
    }
}