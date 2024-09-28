using System;
using UnityEngine;

public class SetCourseAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        CourseInfo info = inf[0] as CourseInfo;
        CoursePropertyDialog.instance.Loading(info);
        CoursePropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        CourseInfo info = inf[0] as CourseInfo;
        NetHelper.OperateInfo(info, EventType.CourseEvent, OperateType.REVISE);  

        append();
    }
}