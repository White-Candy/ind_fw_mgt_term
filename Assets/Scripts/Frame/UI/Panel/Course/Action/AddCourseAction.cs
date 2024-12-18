using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddCourseAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        CoursePropertyDialog.instance.Clear();
        CoursePropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(Action append = default, params object[] inf)
    {
        base.Action(inf:inf);

        CourseInfo info = inf[0] as CourseInfo;

        List<int> id = new List<int>();
        foreach (var item in CoursePanel.m_courseInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        NetHelper.OperateInfo(info, EventType.CourseEvent, OperateType.ADD);

        append();
    }
}