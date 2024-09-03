using System;
using UnityEngine;

public class SetTheoryAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {   
        MajorInfo info = inf[0] as MajorInfo;
        MajorPropertyDialog.instance.Loading(info);
        MajorPropertyDialog.instance.RegisterTime.enabled = false;
        MajorPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);
    }
}