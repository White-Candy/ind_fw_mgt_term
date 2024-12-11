
using UnityEngine;

public class PanelAction
{
    BasePanel m_Panel;

    public PanelAction() {}

    public PanelAction(string name)
    {
        m_Panel = UIHelper.FindPanel(name);
    }

    public void OnEvent(params object[] objs)
    {
        m_Panel.InitAsync();
        m_Panel.Active(true);
    }

    public void Close(params object[] objs)
    {
        m_Panel.Close();
    }
}