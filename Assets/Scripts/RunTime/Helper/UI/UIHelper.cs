
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIHelper
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
        // Debug.Log("FindPanel: " + name);
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
        return idx  >= 0 ? idx : -1;
    }

    /// <summary>
    /// 对Dropdown控件重新添加新的Options
    /// </summary>
    /// <param name="dropDown"></param>
    /// <param name=""></param>
    public static void AddDropDownOptions(ref TMP_Dropdown dropDown, List<string> options)
    {
        dropDown.ClearOptions();
        dropDown.AddOptions(options);
    }

    /// <summary>
    /// 场景中的UI控件获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T GetComponentInScene<T> (string name)
    {
        return GameObject.Find(name).GetComponentInChildren<T>();
    }

    /// <summary>
    /// 设置dropdown
    /// </summary>
    /// <param name="dropdown"></param>
    /// <param name="text"></param>
    public static void SetDropDown(ref TMP_Dropdown dropdown, string text)
    {
        int i = dropdown.options.FindIndex(x => x.text == text);
        if (i != -1)
        {
            dropdown.value = i;
        }
    }

    public static void ShowMessage(string title, string hint, params ItemPackage[] packages)
    {
        // new ItemPackage("确定", ConfirmDelete), new ItemPackage("取消", null)
        DialogHelper helper = new DialogHelper();
        MessageDialog dialog = helper.CreateMessDialog("MessageDialog");
        dialog.Show(title, hint, packages); 
    }

    /// <summary>
    /// InputField控件内容检查
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool InputFieldCheck(string text)
    {
        return IllegalCharacterCheck(text) && NullOrEmptyCheck(text);
    }

    /// <summary>
    /// 非法字符检查
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IllegalCharacterCheck(string text)
    {
        if (text.Contains("|") || text.Contains("@") || text.Contains("#"))
        {
            ShowMessage("输入非法字符", "不可使用非法字符'|','@','#'。", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
    }

    public static bool NullOrEmptyCheck(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            ShowMessage("字符为空", "字符不可为空!", new ItemPackage("确定", () => {}));
            return false;
        }
        return true;
    }
}