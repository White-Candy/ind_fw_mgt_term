
using Unity.VisualScripting;

public class MajorAction : BaseAction
{
    public MajorPanel m_Panel;

    public override void OnEvent(params object[] objs)
    {
        base.OnEvent(objs);

        m_Panel = UITools.FindPanel<MajorPanel>();
        m_Panel.Init();
        m_Panel.Active(true);
    }

    public override void Close(params object[] objs)
    {
        base.Close(objs);

        m_Panel.Close();
    }
}