using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddClassAction : PD_BaseAction
{
    private ClassPropertyDialog m_ClassProDialog;

    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        
        m_ClassProDialog = UIHelper.FindPanel<ClassPropertyDialog>();
        m_ClassProDialog.Clear();
        m_ClassProDialog.RegisterTime.enabled = false;
        m_ClassProDialog.ID.enabled = false;
        m_ClassProDialog.Number.enabled = false;

        // ClassPropertyDialog.instance.Clear();

        // ClassPropertyDialog.instance.RegisterTime.enabled = false;
        // ClassPropertyDialog.instance.ID.enabled = false;
        // ClassPropertyDialog.instance.Number.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ClassInfo info = inf[0] as ClassInfo;
        List<int> id = new List<int>();
        foreach (var item in ClassPanel.m_classInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        NetHelper.OperateInfo(info, EventType.ClassEvent, OperateType.ADD);
    }
}