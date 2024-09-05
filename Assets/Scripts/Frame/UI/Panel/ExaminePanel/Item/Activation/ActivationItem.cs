using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实训考核 课程Item
/// </summary>
public class ActivationItem : MonoBehaviour
{
    public TextMeshProUGUI courseName;
    public TextMeshProUGUI registerTime;
    public Toggle toggle;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="inf"></param>
    public void Init(ExamineInfo info)
    {
        courseName.text = info.CourseName;
        registerTime.text = info.RegisterTime.ToString();
        toggle.isOn = info.Status;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        courseName.text = "";
        registerTime.text = "";
        toggle.isOn = false;
    }
}