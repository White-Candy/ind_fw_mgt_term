using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddTrainingAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        MajorPropertyDialog.instance.Clear();
        MajorPropertyDialog.instance.RegisterTime.enabled = false;
        MajorPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);
    }
}