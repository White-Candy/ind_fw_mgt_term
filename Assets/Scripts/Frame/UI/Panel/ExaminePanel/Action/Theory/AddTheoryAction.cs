using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddTheoryAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        MajorPropertyDialog.instance.Clear();
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);
    }
}