using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实训考核 课程Item
/// </summary>
public class TrainingCourseItem : MonoBehaviour
{
    public TextMeshProUGUI m_name;
    public TMP_InputField m_score;
    public Toggle m_toggle;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="inf"></param>
    public void Init(ExamineInfo info)
    {
        m_name.text = info.CourseName;
        m_score.text = info.TrainingScore.ToString();
        m_toggle.isOn = info.Status;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        m_name.text = "";
        m_score.text = "";
        m_toggle.isOn = false;
    }
}