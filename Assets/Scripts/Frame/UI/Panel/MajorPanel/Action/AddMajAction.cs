using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddMajAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        MajorPropertyDialog dialog = UIHelper.FindPanel<MajorPropertyDialog>();
        dialog.Clear();
        dialog.RegisterTime.enabled = false;
        dialog.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        MajorInfo info = inf[0] as MajorInfo;

        List<int> id = new List<int>();
        foreach (var item in MajorPanel.m_majorInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        NetHelper.OperateInfo(info, EventType.MajorEvent, OperateType.ADD);
    }
}