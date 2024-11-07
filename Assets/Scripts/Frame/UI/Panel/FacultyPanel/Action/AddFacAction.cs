using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddFacAction : PD_BaseAction
{
    private FacPropertyDialog m_FacProDialog;
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        m_FacProDialog = UIHelper.FindPanel<FacPropertyDialog>();
        m_FacProDialog.Clear();
        m_FacProDialog.RegisterTime.enabled = false;
        m_FacProDialog.ID.enabled = false;
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

        NetHelper.OperateInfo(info, EventType.FacultyEvent, OperateType.ADD);
    }
}