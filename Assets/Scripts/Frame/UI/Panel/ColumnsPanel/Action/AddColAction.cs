using System.Collections.Generic;

public class AddColAction : PD_BaseAction
{
    public override void Init(params object[] inf)
    {
        // Debug.Log("AddFacAction");
        ColPropertyDialog.instance.Clear();
        ColPropertyDialog.instance.RegisterTime.enabled = false;
        ColPropertyDialog.instance.ID.enabled = false;
    }

    public override void Action(params object[] inf)
    {
        base.Action(inf);

        ColumnsInfo info = inf[0] as ColumnsInfo;

        List<int> id = new List<int>();
        foreach (var item in ColumnsPanel.instance.m_columnsInfo)
        {
            id.Add(int.Parse(item.id));
        }
        info.id = Tools.SpawnRandom(id).ToString();

        TCPHelper.OperateInfo(info, EventType.ColumnsEvent, OperateType.ADD);
    }
}