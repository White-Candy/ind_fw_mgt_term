using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddClassAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        ClassPropertyDialog.instance.Clear();

        ClassPropertyDialog.instance.RegisterTime.enabled = false;
        ClassPropertyDialog.instance.ID.enabled = false;
        ClassPropertyDialog.instance.Number.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        ClassInfo info = inf[0] as ClassInfo;
        List<int> id = new List<int>();
        foreach (var item in ClassPanel.m_classInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        TCPHelper.OperateInfo(info, EventType.ClassEvent, OperateType.ADD);
    }
}