
using UnityEngine;

public class PanelAction
{
    BasePanel m_Panel;

    public PanelAction() {}

    public PanelAction(string name)
    {
        m_Panel = UITools.FindPanel(name);
    }

    public void OnEvent(params object[] objs)
    {
        m_Panel.Init();
        m_Panel.Active(true);
    }

    public void Close(params object[] objs)
    {
        m_Panel.Close();
    }
}