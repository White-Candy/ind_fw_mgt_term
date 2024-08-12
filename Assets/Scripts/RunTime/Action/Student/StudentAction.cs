
public class StudentAction : BaseAction
{
    public StudentPanel m_Panel;

    public override void OnEvent(params object[] objs)
    {
        base.OnEvent(objs);

        m_Panel = UITools.FindPanel<StudentPanel>();
        m_Panel.Init();
        m_Panel.Active(true);
    }
}