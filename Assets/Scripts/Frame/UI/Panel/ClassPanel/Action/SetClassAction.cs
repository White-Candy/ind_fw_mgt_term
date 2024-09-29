using System;
using UnityEngine;

public class SetClassAction : PD_BaseAction
{
    private ClassPropertyDialog m_ClassProDialog;
    
    public override void Init(params object[] inf)
    {   
        ClassInfo info = inf[0] as ClassInfo;

        m_ClassProDialog = UIHelper.FindPanel<ClassPropertyDialog>();
        m_ClassProDialog.Loading(info);
        m_ClassProDialog.RegisterTime.enabled = false;
        m_ClassProDialog.ID.enabled = false;
        m_ClassProDialog.Number.enabled = false;

        // ClassPropertyDialog.instance.Loading(info);
        // ClassPropertyDialog.instance.RegisterTime.enabled = false;
        // ClassPropertyDialog.instance.ID.enabled = false;
        // ClassPropertyDialog.instance.Number.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        ClassInfo info = inf[0] as ClassInfo;
        NetHelper.OperateInfo(info, EventType.ClassEvent, OperateType.REVISE);  
    }
}