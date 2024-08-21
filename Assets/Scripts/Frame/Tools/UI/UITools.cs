
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITools
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

    /// <summary>
    /// 获取下拉列表item的索引
    /// </summary>
    /// <param name="dropdwon"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int GetDropDownOptionIndex(TMP_Dropdown dropdwon, string text)
    {
        int idx = dropdwon.options.FindIndex(x => x.text == text);
        return idx  >= 0 ? idx : 0;
    }
}