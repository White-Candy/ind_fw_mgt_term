using System;
using System.Runtime.CompilerServices;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialogItem : MonoBehaviour
{
    private Button m_item;

    private Action onClick;

    public void Awake()
    {
        m_item = this.gameObject.GetComponent<Button>();
    }

    public void Init(string name, Action clicked)
    {
        m_item.GetComponentInChildren<TextMeshProUGUI>().text = name;

        onClick = clicked;
        m_item.OnClickAsObservable().Subscribe(_ => 
        {
            // Debug.Log($"item clicked: {this.transform.name}");
            onClick();
        });
    }
}