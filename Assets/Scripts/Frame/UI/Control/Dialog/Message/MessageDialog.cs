using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    // 按钮对象池
    private ObjectPool<MessageDialogItem> m_Pool = new ObjectPool<MessageDialogItem>();

    //Item列表
    private List<MessageDialogItem> m_listItem = new List<MessageDialogItem>();

    // item 模板
    public GameObject m_Temp;

    // 依赖父物体
    public Transform m_Parent;

    // 关闭按钮
    public Button m_Close;
    public TextMeshProUGUI m_Title;
    public TextMeshProUGUI m_Hint;

    public void Awake()
    {
        m_Pool.RegisterInstance(ItemInstance);
    }

    public void Start()
    {
        m_Close?.OnClickAsObservable().Subscribe(_ => 
        {
            Close();
        });
    }


    /// <summary>
    /// Dialog窗口初始化
    /// </summary>
    public void Init(string title, string hint, params ItemPackage[] packages)
    {
        m_Title.text = title;
        m_Hint.text = hint;
        foreach (var pkg in packages)
        {
            string name = pkg.Name;
            Action action = pkg.Clicked;

            Action onClicked = () => 
            {
                action?.Invoke();
                Close();
            };

            MessageDialogItem item = m_Pool.Create(name);
            item.Init(name, onClicked);
        }
        // Debug.Log($"This is Dialog Panel Init, And params count is: {packages.Count()}");
    }

    /// <summary>
    /// DialogItem的初始化
    /// </summary>
    /// <returns></returns>
    public MessageDialogItem ItemInstance()
    {
        MessageDialogItem item = Instantiate(m_Temp, m_Parent).GetComponent<MessageDialogItem>();
        // item.gameObject.SetActive(false);
        return item;
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        gameObject.SetActive(false);
        foreach (var item in m_listItem)
        {
            m_Pool.Destroy(item);
        }
    }
}

/// <summary>
/// Item包
/// </summary>
public class ItemPackage
{
    public string Name;
    public Action Clicked;

    public ItemPackage(string name = "item", Action action = null)
    {
        Name = name;
        Clicked = action;
    }
}