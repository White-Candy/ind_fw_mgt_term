
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
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

    public static BasePanel FindPanel(string name)
    {
        BasePanel basePanel;
        PanelList.TryGetValue(name, out basePanel);
        return basePanel != null ? basePanel : null;
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

    /// <summary>
    /// 对Dropdown控件重新添加新的Options
    /// </summary>
    /// <param name="dropDown"></param>
    /// <param name=""></param>
    public static void AddDropDownOptions(TMP_Dropdown dropDown, List<string> options)
    {
        dropDown.ClearOptions();
        dropDown.AddOptions(options);
    }
}