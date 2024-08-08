
using System.Collections.Generic;
using UnityEngine;

public static class UITools
{
    public static Dictionary<string, BasePanel> PanelList = new Dictionary<string, BasePanel>();

    public static T FindPanel<T>() where T : class, IPanel
    {
        foreach (var panel in PanelList.Values)
        {
            if (panel is T)
            {
                //Debug.Log("SpawnPanel: " + typeof(T).Name);
                return panel as T;
            }
        }
        return null;
    }

    public static void AddPanel(string panelName, BasePanel panel)
    {
        //Debug.Log($"AddPanel:{panelName}");
        PanelList.Add(panelName, panel);
    }
}