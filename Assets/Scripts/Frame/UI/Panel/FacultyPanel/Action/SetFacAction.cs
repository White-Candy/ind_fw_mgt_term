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

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        FacultyInfo info = inf[0] as FacultyInfo;

        TCPHelper.OperateInfo(info, EventType.FacultyEvent, OperateType.REVISE);
    }
}