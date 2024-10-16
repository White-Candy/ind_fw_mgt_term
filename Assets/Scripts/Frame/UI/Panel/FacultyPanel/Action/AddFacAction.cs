using System;
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
        FacPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append, params object[] inf)
    {
        base.Action(inf:inf);

        FacultyInfo info = inf[0] as FacultyInfo;

        List<int> id = new List<int>();
        foreach (var item in FacultyPanel.m_faculiesInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();

        TCPHelper.OperateInfo(info, EventType.FacultyEvent, OperateType.ADD);
    }
}