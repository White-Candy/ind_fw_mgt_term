using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class AddMajAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        MajorPropertyDialog.instance.Clear();
        MajorPropertyDialog.instance.RegisterTime.enabled = false;
        MajorPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        MajorInfo info = inf[0] as MajorInfo;

        List<int> id = new List<int>();
        foreach (var item in MajorPanel.instance.m_majorInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();
        TCPHelper.AddInfo<TCPMajorHelper>(info);
    }
}