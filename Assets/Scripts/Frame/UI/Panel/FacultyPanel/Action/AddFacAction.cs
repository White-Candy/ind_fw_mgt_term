using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddFacAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        FacPropertyDialog.instance.Clear();
        FacPropertyDialog.instance.RegisterTime.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        FacultyInfo info = inf[0] as FacultyInfo;
        info.id = Tools.SpawnRandom().ToString();
        TCPHelper.AddFacInfo(info);
    }
}